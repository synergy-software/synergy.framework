using ApprovalTests;

namespace Synergy.Catalogue.Approval
{
    public class MarkdownTextWriter : ApprovalTextWriter
    {
        /// <inheritdoc />
        public MarkdownTextWriter(string data) : base(data, "md")
        {
        }
    }
}