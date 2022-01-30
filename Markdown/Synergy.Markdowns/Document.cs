using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Markdowns
{
    public static partial class Markdown
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string NL = Environment.NewLine;
        // ReSharper disable once InconsistentNaming
        private static readonly string NL2 = Markdown.NL + Markdown.NL;
        
        public interface IElement{}
        
        public class Document
        {
            private readonly List<IElement> elements = new();
            
            public Document Append(IElement element)
            {
                this.elements.Add(element);
                return this;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                StringBuilder markdown = new StringBuilder();
                foreach (var element in this.elements)
                {
                    markdown.AppendLine(element.ToString());
                }
                
                return markdown.ToString();
            }
        }
    }
}