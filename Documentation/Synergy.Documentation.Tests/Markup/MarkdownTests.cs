using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;
using Synergy.Documentation.Tests.Docs;

namespace Synergy.Documentation.Tests.Markup;

[UsesVerify]
public class MarkdownTests
{
    private static readonly CodeFile currentFile = CodeFile.Current();
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");
    private static readonly CodeFile synergyPng = CodeFolder.Current().File("synergy.png");
    
    [Fact]
    public async Task CreateMarkdownDocument()
    {
        var markdown = new Markdown.Document();
        markdown
            .Append(new Markdown.Header1("header - level 1"))
            .Append(new Markdown.Header2("header - level 2"))
            .Append(new Markdown.Header3("header - level 3"))
            .Append(new Markdown.Paragraph("some text"))
            .Append(new Markdown.Code("var code = new PieceOfCode();"))
            .Append(SampleTable())
            .Append(new Markdown.Quote("some quote"))
            .Append(new Markdown.Image(MarkdownTests.synergyPng).RelativeFrom(currentFile))
            .Append(new Markdown.Link(readmeFile).RelativeFrom(currentFile));

        await Verifier.Verify(markdown.ToString(), "md");

        Markdown.Table SampleTable()
        {
            var table = new Markdown.Table("column 1", "column 2");
            table.Append("cell 1", "cell 2");
            return table;
        }
    }
}