using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;
using Synergy.Documentation.Tests.Architecture.Dependencies;
using Synergy.Documentation.Tests.Architecture.Public;
using Synergy.Documentation.Tests.Comments;

namespace Synergy.Documentation.Tests.Docs;

// TODO: Marcin Celej [from: Marcin Celej on: 09-01-2024]: Extract this to a separate project Synergy.Documentation.Docs with all needed samples inside

partial class README
{
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");

    private CodeFile ApiFile => CodeFile.For<Architecture.Public.Api>();
    private Markdown.Link ApiLink => Markdown.Link.To(this.ApiFile).RelativeFrom(readmeFile);
    
    private CodeFile ApiForSynergyDocs => this.ApiFile.Folder.File("Api.of.Synergy.Documentation.verified.md");
    private Markdown.Link ApiForSynergyDocsLink => Markdown.Link.To(this.ApiForSynergyDocs).RelativeFrom(readmeFile);
    
    private CodeFile PackageFile => CodeFile.For<Package>();
    private Markdown.Link PackageLink => Markdown.Link.To(this.PackageFile).RelativeFrom(readmeFile);
    
    private CodeFile TodosFile => CodeFile.For<Architecture.Debt.Todos>();
    private Markdown.Link TodosLink => Markdown.Link.To(this.TodosFile).RelativeFrom(README.readmeFile);

    private CodeFile TodosDebt => this.TodosFile.Folder.File("Todos.Technical.Debt.verified.md");
    private Markdown.Link TodosDebtLink => Markdown.Link.To(this.TodosDebt).RelativeFrom(README.readmeFile);
    
    private CodeFile RelationsFile => CodeFile.For<Relations>();
    private Markdown.Link RelationsLink => Markdown.Link.To(this.RelationsFile).RelativeFrom(README.readmeFile);

    private CodeFile RelationsOfMarkdown => this.RelationsFile.Folder.File("Relations.of.Markdown.verified.md");
    private Markdown.Link RelationsOfMarkdownLink => Markdown.Link.To(this.RelationsOfMarkdown).RelativeFrom(README.readmeFile);
    
    private CodeFile NoteTestsFile => CodeFile.For<NoteTests>();
    private Markdown.Link NoteTestsLink => Markdown.Link.To(this.NoteTestsFile).RelativeFrom(README.readmeFile);
    
    [Fact]
    public void Generate()
    {
        var content = this.TransformText();
        File.WriteAllText(readmeFile.FilePath, content);
    }
}