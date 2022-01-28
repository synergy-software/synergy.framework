using Synergy.Contracts;

namespace Synergy.Catalogue.Markdowns
{
    public partial class Markdown
    {
        public class Header1 : IElement
        {
            private readonly string header;

            public Header1(string header)
                => this.header = header.OrFail(nameof(header))
                                       .Trim();

            public override string ToString() => $"# {this.header}{Markdown.NL}";
        }
    }
}