using Synergy.Contracts;

namespace Synergy.Catalogue.Markdowns
{
    public partial class Markdown
    {
        public class Code : IElement
        {
            private readonly string text;

            public Code(string text)
                => this.text = text.OrFail(nameof(text))
                                   .Trim();

            public Code Line(string line)
            {
                return new Code(this.text + NL + line);
            }
            
            public override string ToString() => $"```{NL}{this.text}{NL}```{NL}";
        }
    }
}