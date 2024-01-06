using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Docs;

partial class README
{
    private static CodeFile ReadmeFile => CodeFolder.Current().Up(2).File($"{nameof(README)}.md");

    // TODO: Marcin Celej [from: Marcin Celej on: 06-01-2024]: Add here reading file path through [SourceFile] from Synergy.Documentation.Annotations
    private static CodeFile Api => CodeFolder.Current().Up().Sub("Architecture").Sub("Public").File("Api.cs");
    private Markdown.Link ApiLink => Markdown.Link.To(Api).From(ReadmeFile);
    
    private static CodeFile ApiForSynergyDocs => CodeFolder.Current().Up().Sub("Architecture").Sub("Public").File("Api.of.Synergy.Documentation.verified.md");
    private Markdown.Link ApiForSynergyDocsLink => Markdown.Link.To(ApiForSynergyDocs).From(ReadmeFile);
    
    private static CodeFile Package => CodeFolder.Current().Up().Sub("Architecture").Sub("Public").File("Package.cs");
    private Markdown.Link PackageLink => Markdown.Link.To(Package).From(ReadmeFile);
    
    private static CodeFile Todos => CodeFolder.Current().Up().Sub("Architecture").Sub("Debt").File("Todos.cs");
    private Markdown.Link TodosLink => Markdown.Link.To(Todos).From(ReadmeFile);

    private static CodeFile TodosDebt => CodeFolder.Current().Up().Sub("Architecture").Sub("Debt").File("Todos.Technical.Debt.verified.md");
    private Markdown.Link TodosDebtLink => Markdown.Link.To(TodosDebt).From(ReadmeFile);
    
    private static CodeFile Relations => CodeFolder.Current().Up().Sub("Architecture").Sub("Dependencies").File("Relations.cs");
    private Markdown.Link RelationsLink => Markdown.Link.To(Relations).From(ReadmeFile);

    private static CodeFile RelationsOfMarkdown => CodeFolder.Current().Up().Sub("Architecture").Sub("Dependencies").File("Relations.of.Markdown.verified.md");
    private Markdown.Link RelationsOfMarkdownLink => Markdown.Link.To(RelationsOfMarkdown).From(ReadmeFile);
    
    [Fact]
    public void Generate()
    {
        var content = this.TransformText();
        File.WriteAllText(README.ReadmeFile.FilePath, content);
    }
}