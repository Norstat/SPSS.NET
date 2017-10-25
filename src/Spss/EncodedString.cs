using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Spss
{
    /// <summary>
    /// Helper for encoding a string and using an <see cref="IntPtr"/> to pass it to SPSS
    /// while also safely handling the underlying IntPtr.
    /// The methods in the spss io library can return either local mode or utf-8. The 
    /// c# functions do not support that, as far as I can tell, so we have to manage
    /// the encoding and bytes ourselves using an IntPtr. If we just use a C# string,
    /// then at best - it would work in local or utf-8 mode, but not both.
    /// </summary>
    /// <remarks>
    /// See http://blog.benoitblanchon.fr/safehandle/
    /// </remarks>
    public class EncodedString : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region Instance

        /// <summary>
        /// Create a buffer for SETting string to sav
        /// Always wrap this in a using statement!
        /// </summary>
        public EncodedString(string value, Encoding encoding) : base(true)
        {
            var encodedBytes = Encoding.Convert(Encoding.Unicode, encoding, Encoding.Unicode.GetBytes(value));

            // create encoded byte array with null terminater (hence +1)
            var ptrData = new byte[encodedBytes.Length + 1];
            encodedBytes.CopyTo(ptrData, 0);
            ptrData[ptrData.Length - 1] = 0; // null terminate the string

            // create the intptr
            this.SetHandle(Marshal.AllocHGlobal(ptrData.Length));

            // and copy data
            Marshal.Copy(ptrData, 0, this.handle, ptrData.Length);
        }

        /// <summary>
        /// Create a string buffer for GETting string from sav.
        /// Always wrap this in a using statement!
        /// </summary>
        /// <param name="byteLength"></param>
        public EncodedString(int byteLength) : base(true)
        {
            this.SetHandle(Marshal.AllocHGlobal(byteLength));
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                Marshal.FreeHGlobal(this.handle);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Decode into string. Expects a null terminated char array
        /// </summary>
        /// <param name="encoding"></param>
        public string ToString(Encoding encoding)
        {
            return Decode(this.handle, encoding);
        }

        /// <summary>
        /// Decode into string
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="length">the number of bytes</param>
        public string ToString(Encoding encoding, int length)
        {
            return Decode(this.handle, encoding, length);
        }

        public override string ToString()
        {
            return this.ToString(Encoding.Default);
        }

        #endregion

        #region Static

        /// <summary>
        /// Decode an IntPtr to a string when you don't know the length. This
        /// expects a null terminated array
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="encoding"></param>
        public static string Decode(IntPtr ptr, Encoding encoding)
        {
            byte b;
            var bytes = new List<byte>();
            var offset = 0;
            while ((b = Marshal.ReadByte(ptr, offset++)) != 0 && offset < SpssThinWrapper.SPSS_MAX_VERYLONGSTRING)
            {
                bytes.Add(b);
            }
            return encoding.GetString(bytes.ToArray()).Trim();
        }

        /// <summary>
        /// Convert char *** to string[]
        /// </summary>
        /// <remarks>
        /// This is based on https://limbioliong.wordpress.com/2011/08/14/returning-an-array-of-strings-from-c-to-c-part-1/
        /// I'm uncertain whether to call FreeHGlobal/FreeCoTaskMem or not. Seems to work in some cases, but not in others. 
        /// spssFreeVarNValueLabels Should free the memory, right?
        /// </remarks>
        public static string[] DecodeArray(IntPtr ptr, Encoding encoding, int arraySize)
        {
            if (ptr == IntPtr.Zero || arraySize == 0)
            {
                // Marshal.Copy throw if ptr is Zero!?
                return new string[0];
            }

            var result = new string[arraySize];
            var ptrArray = new IntPtr[arraySize];
            Marshal.Copy(ptr, ptrArray, 0, arraySize);
            for (var i = 0; i < arraySize; i++)
            {
                var p = ptrArray[i];
                result[i] = Decode(p, encoding);
            }
            return result;
        }

        /// <summary>
        /// Decode an IntPtr to a string when you know the byte length
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="encoding"></param>
        /// <param name="length">number of bytes</param>
        public static string Decode(IntPtr ptr, Encoding encoding, int length)
        {
            if (length == 0) return null;

            var bytes = new byte[length];
            Marshal.Copy(ptr, bytes, 0, length);
            return encoding.GetString(bytes).Trim();

            // NB: this doesn't work. We need the raw bytes to handle encoding, as above
            //Marshal.PtrToStringAnsi(ptr); // auto -> uni or ansi
        }

        public static implicit operator IntPtr(EncodedString str)
        {
            return str.handle;
        }

        #endregion
    }
}
