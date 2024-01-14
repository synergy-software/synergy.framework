using Synergy.Architecture.Tests.Samples;
using Synergy.Documentation.Code;
using Synergy.Documentation.Markup;

namespace Synergy.Architecture.Tests.Docs;

// TODO: Marcin Celej [from: Marcin Celej on: 09-01-2024]: Extract this to a separate project Synergy.Documentation.Docs with all needed samples inside

partial class README
{
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");

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