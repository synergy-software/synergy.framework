using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Markdowns
{
    public static partial class Markdown
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string NL = Environment.NewLine;
        
        public interface IElement{}
        
        public class Document : IEnumerable<IElement>
        {
            private readonly List<IElement> elements = new();
            
            public Document Append(IElement element)
            {
                this.elements.Add(element);
                return this;
            }
            
            public Document Append(IEnumerable<IElement> newElements)
            {
                this.elements.AddRange(newElements);
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

            /// <inheritdoc />
            public IEnumerator<IElement> GetEnumerator() 
                => this.elements.GetEnumerator();

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator() 
                => this.GetEnumerator();
        }
    }
}