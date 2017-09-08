using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spss.Testing
{
    public class EncodingTests
    {
        private string[] labels1250 = new[]
        {
            "Czy planują Państwo w najbliższym czasie wykorzystywanie w procesach produkcyjnych innych, nowych modyfikatów, które nie zostały jeszcze wymienione?",
            "Proszę podać symbole lub nazwy modyfikatów, które planują Państwo wykorzystywać w najbliższej przyszłości. NIE ODCZYTYWAĆ ANKIETER: Maksymalnie 5 odpowiedziUWAGA: Respondenci podając symbol modyfikatu mogą podać kod zaczynający się od „E” np. E-1410 czyli kod dodatku do żywności lub kod zaczynający się od „LU” np. LU-1410 czyli kod handlowy. To nie ma znaczenia, który podadzą, ponieważ oznaczenie cyfrowe jest zawsze takie samo.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        };

        private string[] labels1252 = new[]
        {
            "æ ø å",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        };

        [Fact]
        public void CreatePolishFile()
        {
            SpssThinWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            WriteLabelsFile(@"SAVs\generated-pl.sav", "Polish_Poland.1250", Encoding.GetEncoding(1250), () => labels1250);
        }

        [Fact]
        public void CreateDanishFile()
        {
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            WriteLabelsFile(@"SAVs\generated-da.sav", "Danish", Encoding.GetEncoding(1252), () => labels1252);
        }

        [Fact]
        public void CreateUtf8File()
        {
            // just testing...
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_UTF8);
            WriteLabelsFile(@"SAVs\generated-utf8.sav", null, Encoding.UTF8, () => labels1252.Union(labels1250));
        }

        [Fact]
        public void ReadDanishFile()
        {
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            SpssSafeWrapper.spssSetLocale(0, "Polish");
            int handle = -1;
            string label;
            try
            {
                var enc = Encoding.GetEncoding(1252);
                SpssSafeWrapper.spssOpenRead(@"SAVs\da.sav", out handle);
                SpssSafeWrapper.spssGetVarLabel(handle, "q1", out label, enc);
                Assert.Equal("the quick brown fox...", label);

                SpssSafeWrapper.spssGetVarLabel(handle, "q2", out label, enc);
                Assert.Equal("æøå (DA characters)", label);
            }
            finally
            {
                SpssSafeWrapper.spssCloseRead(handle);
            }
        }

        [Fact]
        public void ReadPolishFileAsUtf8()
        {
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_UTF8);
            int handle = -1;
            string label;
            try
            {
                SpssSafeWrapper.spssOpenRead(@"SAVs\pl.sav", out handle);
                SpssSafeWrapper.spssGetVarLabel(handle, "Q1", out label, Encoding.UTF8);
                Assert.Equal(labels1250[0], label);

                SpssSafeWrapper.spssGetVarLabel(handle, "Q2", out label, Encoding.UTF8);
                // SPSS stores fewer characters. Not checked why (could be a couple of reasons)
                Assert.Equal(labels1250[1].Substring(0, label.Length), label);
                double[] values;
                string[] labels;
                SpssSafeWrapper.spssGetVarNValueLabels(handle, "Q2", out values, out labels);
                Assert.Equal("Proszę", labels[0]);
                Assert.Equal("podać", labels[1]);
            }
            finally
            {
                SpssSafeWrapper.spssCloseRead(handle);
            }
        }


        /// <summary>
        /// Writes a set of labels to a file. Does not bother with var-names or data, just the labels.
        /// </summary>
        private static void WriteLabelsFile(string filename, string savLocale, Encoding encoding, Func<IEnumerable<string>> getLabels)
        {
            var handle = 0;
            if (File.Exists(filename)) File.Delete(filename);

            try
            {
                if (savLocale != null)
                {
                    var result = SpssSafeWrapper.spssSetLocale(0, savLocale);
                    Debug.WriteLine(savLocale + " => " + result);
                }
                SpssSafeWrapper.spssOpenWrite(filename, out handle);
                var index = 1;
                foreach (var lbl in getLabels())
                {
                    var name = "Q" + index++;
                    SpssSafeWrapper.spssSetVarName(handle, name, 0);
                    SpssSafeWrapper.spssSetVarLabel(handle, name, lbl, encoding);
                }
                SpssThinWrapper.spssCommitHeaderImpl(handle);
            }
            finally
            {
                SpssThinWrapper.spssCloseWriteImpl(handle);
            }
        }
    }
}
