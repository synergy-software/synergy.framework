using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Tests.Samples;
using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;

namespace Synergy.Architecture.Tests.Docs;

// TODO: Marcin Celej [from: Marcin Celej on: 09-01-2024]: Extract this to a separate project Synergy.Documentation.Docs with all needed samples inside

partial class README
{
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");

    private CodeFile RealSequenceDiagramsFile => Sample.Web.API.Tests.Architecture.Diagrams.CodeFile;
    private Markdown.Link RealSequenceDiagramsLink => Markdown.Link.To(this.RealSequenceDiagramsFile).RelativeFrom(readmeFile);
    
    private CodeFile RealDiagramsFile => Sample.Web.API.Tests.Architecture.Diagrams.SequenceDiagrams;
    private Markdown.Link RealDiagramsLink => Markdown.Link.To(this.RealDiagramsFile).RelativeFrom(readmeFile);

    private string AttributesList => string.Join(Environment.NewLine, this.Attributes);

    private IEnumerable<string> Attributes =>
        typeof(SequenceDiagramActivationAttribute).Assembly.GetTypes()
                                                  .Where(attribute => attribute.Name.StartsWith("SequenceDiagram") && attribute.Name.EndsWith("Attribute"))
                                                  .Select(attribute => $"- [{attribute.Name.Replace("Attribute", "")}]");
    
    private CodeFile SampleSequenceDiagramsFile => CodeFile.For<SequenceDiagramSamples>();
    private Markdown.Link SampleSequenceDiagramsLink => Markdown.Link.To(this.SampleSequenceDiagramsFile).RelativeFrom(readmeFile);
    
    private CodeFile SequenceDiagramsFile => SequenceDiagramSamples.SequenceDiagrams;
    private Markdown.Link SequenceDiagramsLink => Markdown.Link.To(this.SequenceDiagramsFile).RelativeFrom(readmeFile);
    
    [Fact]
    public void Generate()
    {
        var content = this.TransformText();
        File.WriteAllText(readmeFile.FilePath, content);
    }
}