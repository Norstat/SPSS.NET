using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
    public class EncodedString : SafeHandle // invalid values?
    {
        private EncodedString(IntPtr invalidHandleValue) : base(invalidHandleValue, true)
        {
        }

        public string Value { get; private set; }

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

        public override bool IsInvalid { get; } = false; // all handle values are valid?

        /// <summary>
        /// Encode using custom encoding
        /// </summary>
        public static EncodedString Encode(string str, Encoding encoding)
        {
            // this code is inspired by these solutions
            // http://stackoverflow.com/questions/13289171/marshalasunmanagedtype-lpstr-how-does-this-convert-utf-8-strings-to-char
            // http://stackoverflow.com/questions/17562295/if-i-allocate-some-memory-with-allochglobal-do-i-have-to-free-it-with-freehglob
            // 
            // SPSS needs a single-byte null terminated char array, similar to UnmanagedType.LPStr.
            // All .Net strings are utf-16, so we can't pass on a string, because it needs to be correctly encoded.
            // We have to use an IntPtr and an array with the encoded bytes.

            // Because .Net strings are utf-16 we convert from Unicode to target encoding
            var encodedBytes = Encoding.Convert(Encoding.Unicode, encoding, Encoding.Unicode.GetBytes(str));

            // copy the encoded bytes to a null terminated array
            var ptrData = new byte[encodedBytes.Length + 1];
            encodedBytes.CopyTo(ptrData, 0);
            ptrData[ptrData.Length - 1] = 0;

            // create an IntPtr and copy the bytes
            var ptr = Marshal.AllocHGlobal(ptrData.Length);
            Marshal.Copy(ptrData, 0, ptr, ptrData.Length);

            return new EncodedString(ptr);
        }

        public static EncodedString Decode(int length = SpssThinWrapper.SPSS_MAX_VERYLONGSTRING)
        {
            var ptr = Marshal.AllocHGlobal(length);
            return new EncodedString(ptr);
        }

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
            while ((b = Marshal.ReadByte(ptr, offset++)) != 0)
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
        /// I'm uncertain whether to call FreeCoTaskMem or not. Seems to work in some cases, but not in others. 
        /// spssFreeVarNValueLabels Should free the memory, right? Leaving them commented for now.
        /// </remarks>
        public static unsafe string[] DecodeArray(IntPtr ptr, Encoding encoding, int len)
        {
            var result = new string[len];
            var ptrArray = new IntPtr[len];
            Marshal.Copy(ptr, ptrArray, 0, len);
            for (var i = 0; i < len; i++)
            {
                var p = ptrArray[i];
                result[i] = Decode(p, encoding);
                //Marshal.FreeCoTaskMem(p);
            }
            //Marshal.FreeCoTaskMem(ptr);
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
    }
}
