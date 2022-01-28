using Synergy.Contracts;

namespace Synergy.Catalogue.Markdowns
{
    public partial class Markdown
    {
        public class Paragraph : IElement
        {
            private readonly string text;

            public Paragraph(string text)
                => this.text = text.OrFail(nameof(text))
                                   .Trim();

            public override string ToString() => $"{this.text}{Markdown.NL}";
        }
    }
}