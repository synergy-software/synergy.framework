using Synergy.Contracts;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
        public class Code : IElement
        {
            private readonly string? language;
            private readonly string text;

            public Code(string text, string? language = "csharp")
            {
                this.language = language?.Trim();
                this.text = text.OrFail(nameof(text))
                                .Trim();
            }

            public Code Line(string line)
            {
                return new Code(this.text + Markdown.NL + line);
            }

            public override string ToString() => $"``` {this.language ?? ""}{Markdown.NL}" +
                                                 $"{this.text}{Markdown.NL}" +
                                                 $"```{Markdown.NL}";
        }
    }
}