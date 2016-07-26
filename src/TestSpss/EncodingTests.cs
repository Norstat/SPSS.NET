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
        public void CreateAPolishFile()
        {
            SpssThinWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            WriteLabelsFile(@"SAVs\generated-pl.sav", "Polish_Poland.1250", Encoding.GetEncoding(1250), () => labels1250);
        }

        [Fact]
        public void CreateADanishFile()
        {
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            WriteLabelsFile(@"SAVs\generated-da.sav", "Danish", Encoding.GetEncoding(1252), () => labels1252);
        }

        [Fact]
        public void CreateAUtf8File()
        {
            // just testing...
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_UTF8);
            foreach (var locale in new[] { /*"Norwegian", "Danish", "Greek", "Turkish", "Hebrew", "Arabic", "Vietnamese", "Russian", */ "Latvian" })
            WriteLabelsFile(@"SAVs\generated-utf8.sav", locale, Encoding.UTF8, () => labels1252.Union(labels1250));
        }

        [Fact]
        public void ReadDanishFile()
        {
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_CODEPAGE);
            SpssSafeWrapper.spssSetLocale(0, "Danish");
            int handle = -1;
            string label;
            try
            {
                SpssSafeWrapper.spssOpenRead(@"SAVs\da.sav", out handle);
                SpssSafeWrapper.spssGetVarLabel(handle, "q1", out label);
                Assert.Equal("the quick brown fox...", label);

                SpssSafeWrapper.spssGetVarLabel(handle, "q2", out label);
                Assert.Equal("æøå (DA characters)", label);
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
