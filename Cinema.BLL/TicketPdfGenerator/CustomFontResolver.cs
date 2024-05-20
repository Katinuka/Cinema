using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.BLL.TicketPdfGenerator
{
    public class CustomFontResolver : IFontResolver
    {
        private readonly Dictionary<string, byte[]> _fontData = new Dictionary<string, byte[]>();

        public CustomFontResolver()
        {
            AddFont("Verdana", "../Fonts/Verdana.ttf");
            AddFont("VerdanaBold", "../Fonts/Verdana.ttf");
        }

        private void AddFont(string name, string path)
        {
            var fontBytes = File.ReadAllBytes(path);
            _fontData[name] = fontBytes;
        }

        public byte[] GetFont(string faceName)
        {
            if (_fontData.TryGetValue(faceName, out var font))
            {
                return font;
            }
            throw new ArgumentException($"Font {faceName} not found.");
        }

        public string DefaultFontName => "Verdana";

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Verdana", StringComparison.OrdinalIgnoreCase))
            {
                if (isBold)
                {
                    return new FontResolverInfo("VerdanaBold");
                }
                return new FontResolverInfo("Verdana");
            }
            return new FontResolverInfo("Verdana");
        }
    }
}
