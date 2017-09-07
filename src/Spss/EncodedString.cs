using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Spss
{
    /// <summary>
    /// Helper for encoding a string and using an <see cref="IntPtr"/> to pass it to SPSS.
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

        public static implicit operator IntPtr(EncodedString str)
        {
            return str.handle;
        }

        public string ToString(Encoding encoding, int length)
        {
            if (length == 0) return null;

            var bytes = new byte[length];
            Marshal.Copy(this.handle, bytes, 0, length);
            // in case the array is null terminated (not all methods return byte length)
            var zeroIndex = Array.FindIndex(bytes, b => b == 0); 
            return zeroIndex >= 0 ? encoding.GetString(bytes, 0, zeroIndex).Trim() : encoding.GetString(bytes);

            // NB: this doesn't work. We need the raw bytes to handle encoding, as above
            //Marshal.PtrToStringAnsi(ptr); // auto -> uni or ansi
        }

        public override string ToString()
        {
            return this.ToString(Encoding.Default, SpssThinWrapper.SPSS_MAX_LONGSTRING);
        }
    }
}
