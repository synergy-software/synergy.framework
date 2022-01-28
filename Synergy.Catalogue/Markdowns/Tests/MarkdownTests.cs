using ApprovalTests;
using Synergy.Catalogue.Approval;
using Xunit;

namespace Synergy.Catalogue.Markdowns
{
    public class MarkdownTests
    {
        [Fact]
        public void CreateMarkdownDocument()
        {
            var markdown = new Markdown.Document();
            markdown.Append(new Markdown.Header1("header - level 1"));
            markdown.Append(new Markdown.Paragraph("some text"));
            var table = new Markdown.Table("column 1", "column 2");
            table.Append("cell 1", "cell 2");
            markdown.Append(table);
            
            var writer = new MarkdownTextWriter(markdown.ToString());
            Approvals.Verify(writer);
        }
    }
}