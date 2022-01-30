using ApprovalTests;
using Xunit;

namespace Synergy.Markdowns.Test
{
    public class MarkdownTests
    {
        [Fact]
        public void CreateMarkdownDocument()
        {
            var markdown = new Markdown.Document();
            markdown
                .Append(new Markdown.Header1("header - level 1"))
                .Append(new Markdown.Header2("header - level 2"))
                .Append(new Markdown.Header3("header - level 3"))
                .Append(new Markdown.Paragraph("some text"))
                .Append(new Markdown.Code("var code = new PieceOfCode();"))
                .Append(SampleTable())
                .Append(new Markdown.Quote("some quote"));

            var writer = new MarkdownTextWriter(markdown.ToString());
            Approvals.Verify(writer);

            Markdown.Table SampleTable()
            {
                var table = new Markdown.Table("column 1", "column 2");
                table.Append("cell 1", "cell 2");
                return table;
            }
        }
    }
}