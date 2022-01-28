using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Catalogue.Markdowns
{
    public static partial class Markdown
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string NL = Environment.NewLine;
        // ReSharper disable once InconsistentNaming
        private static readonly string NL2 = NL + NL;
        
        public interface IElement{}
        
        public class Document
        {
            private readonly List<IElement> elements = new();
            
            public void Append(IElement element)
            {
                this.elements.Add(element);
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