using Synergy.Contracts;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
        public abstract class Header : IElement
        {
            private readonly int level;
            private readonly string header;

            protected Header(int level, string header)
            {
                this.level = level;
                this.header = header.OrFail(nameof(header))
                                    .Trim();
            }

            public override string ToString() => $"{new string('#', this.level)} {this.header}{NL}";
        }

        public class Header1 : Header
        {
            public Header1(string header) : base(1, header)
            {
            }
        }

        public class Header2 : Header
        {
            public Header2(string header) : base(2, header)
            {
            }
        }
        
        public class Header3 : Header
        {
            public Header3(string header) : base(3, header)
            {
            }
        }
    }
}