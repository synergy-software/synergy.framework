using Synergy.Contracts;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
        public class Paragraph : IElement
        {
            private readonly string text;

            public Paragraph(string text)
                => this.text = text.OrFail(nameof(text))
                                   .Trim();

            public Paragraph Line(string line)
            {
                return new Paragraph(this.text + NL + line);
            }
            
            public override string ToString() => $"{this.text}{NL}";
        }
    }
}