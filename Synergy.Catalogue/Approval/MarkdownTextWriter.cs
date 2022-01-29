using ApprovalTests;
using Synergy.Catalogue.Markdowns;

namespace Synergy.Catalogue.Approval
{
    public class MarkdownTextWriter : ApprovalTextWriter
    {
        /// <inheritdoc />
        public MarkdownTextWriter(string data) : base(data, "md")
        {
        }
        
        public MarkdownTextWriter(Markdown.Document data) : base(data.ToString(), "md")
        {
        }
    }
}