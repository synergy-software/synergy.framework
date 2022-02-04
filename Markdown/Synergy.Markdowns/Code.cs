using Synergy.Catalogue;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
        public class Code : IElement
        {
            private readonly string? language;
            private readonly string? text;

            public Code(string? text = null, string? language = "csharp")
            {
                this.language = language?.TrimToNull();
                this.text = text;
            }

            public Code Line(string line)
            {
                if (this.text == null)
                    return new Code(line, this.language);
                return new Code(this.text + Markdown.NL + line, this.language);
            }

            public override string ToString() => $"``` {this.language ?? ""}{Markdown.NL}" +
                                                 $"{this.text}{Markdown.NL}" +
                                                 $"```{Markdown.NL}";
        }
    }
}