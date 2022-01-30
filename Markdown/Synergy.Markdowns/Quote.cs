using System;
using System.Collections.Generic;
using Synergy.Contracts;

namespace Synergy.Markdowns
{
    public partial class Markdown
    {
        public class Quote : IElement
        {
            private readonly List<string> lines;

            public Quote(string text)
            {
                this.lines = new List<string>()
                {
                    text.OrFail(nameof(text))
                        .Trim()
                };
            }

            public Quote Line(string text)
            {
                this.lines.Add(text.OrFail(nameof(text))
                                   .Trim());
                return this;
            }
            
            public override string ToString() => $"> {String.Join(NL + "> ", this.lines)}{NL}";
        }
    }
}