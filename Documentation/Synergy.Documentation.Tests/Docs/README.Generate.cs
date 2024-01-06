using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;

namespace Synergy.Documentation.Tests.Docs;

partial class README
{
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");
    private readonly CodeFolder architectureFolder = CodeFolder.Current().Up().Sub("Architecture");

    // TODO: Marcin Celej [from: Marcin Celej on: 06-01-2024]: Add here reading file path through [SourceFile] from Synergy.Documentation.Annotations
    private CodeFile Api => this.architectureFolder.Sub("Public").File("Api.cs");
    private Markdown.Link ApiLink => Markdown.Link.To(Api).RelativeFrom(readmeFile);
    
    private CodeFile ApiForSynergyDocs => this.architectureFolder.Sub("Public").File("Api.of.Synergy.Documentation.verified.md");
    private Markdown.Link ApiForSynergyDocsLink => Markdown.Link.To(ApiForSynergyDocs).RelativeFrom(readmeFile);
    
    private CodeFile Package => this.architectureFolder.Sub("Public").File("Package.cs");
    private Markdown.Link PackageLink => Markdown.Link.To(Package).RelativeFrom(readmeFile);
    
    private CodeFile Todos => this.architectureFolder.Sub("Debt").File("Todos.cs");
    private Markdown.Link TodosLink => Markdown.Link.To(Todos).RelativeFrom(README.readmeFile);

    private CodeFile TodosDebt => this.architectureFolder.Sub("Debt").File("Todos.Technical.Debt.verified.md");
    private Markdown.Link TodosDebtLink => Markdown.Link.To(TodosDebt).RelativeFrom(README.readmeFile);
    
    private CodeFile Relations => this.architectureFolder.Sub("Dependencies").File("Relations.cs");
    private Markdown.Link RelationsLink => Markdown.Link.To(Relations).RelativeFrom(README.readmeFile);

    private CodeFile RelationsOfMarkdown => this.architectureFolder.Sub("Dependencies").File("Relations.of.Markdown.verified.md");
    private Markdown.Link RelationsOfMarkdownLink => Markdown.Link.To(RelationsOfMarkdown).RelativeFrom(README.readmeFile);
    
    [Fact]
    public void Generate()
    {
        var content = this.TransformText();
        File.WriteAllText(readmeFile.FilePath, content);
    }
}