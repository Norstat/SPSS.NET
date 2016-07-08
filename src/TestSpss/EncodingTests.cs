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
        // working savLocale formats: Polish_Poland.1250, Polish (case sensitive?)
        // todo: make a lookup table so DPT can convert 'windows-1250' to 'Polish'

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
            SpssSafeWrapper.spssSetInterfaceEncodingImpl(InterfaceEncoding.SPSS_ENCODING_UTF8);
            WriteLabelsFile(@"SAVs\generated-utf8.sav", "Danish", Encoding.UTF8, () => labels1252.Union(labels1250));
        }

        /// <summary>
        /// Writes a set of labels to a file. Does not bother with var-names or data, just the labels.
        /// </summary>
        private static void WriteLabelsFile(string filename, string savLocale, Encoding encoding, Func<IEnumerable<string>> getLabels)
        {
            var handle = 0;
            if (File.Exists(filename)) File.Delete(filename);

            var wrapper = new SpssWrapper(encoding);
            try
            {
                if (savLocale != null)
                {
                    Debug.WriteLine(wrapper.SpssSetLocale(0, savLocale));
                }
                wrapper.spssOpenWrite(filename, out handle);
                var index = 1;
                foreach (var lbl in getLabels())
                {
                    var name = "Q" + index++;
                    wrapper.spssSetVarName(handle, name, 0);
                    wrapper.spssSetVarLabel(handle, name, lbl);
                }
                wrapper.spssCommitHeader(handle);
            }
            finally
            {
                wrapper.spssCloseWrite(handle);
            }
        }
    }
}
