using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Docs;

partial class README
{
    private static CodeFile Markdown => CodeFolder.Current().Up(2).File($"{nameof(README)}.md");

    // TODO: Marcin Celej [from: Marcin Celej on: 06-01-2024]: Add here reading file path through from [SourceFile] from Synergy.Documentation.Annotations
    private static CodeFile Relations => CodeFolder.Current().Up().Sub("Architecture").Sub("Dependencies").File("Relations.cs");
    private static CodeFile RelationsOfMarkdown => CodeFolder.Current().Up().Sub("Architecture").Sub("Dependencies").File("Relations.of.Markdown.verified.md");
    
    private Markdown.Link RelationsLink => new Markdown.Link(Relations).RelativeTo(Markdown);
    private Markdown.Link RelationsOfMarkdownLink => new Markdown.Link(RelationsOfMarkdown).RelativeTo(Markdown);
    
    [Fact]
    public void Generate()
    {
        var content = this.TransformText();
        File.WriteAllText(README.Markdown.FilePath, content);
    }
}