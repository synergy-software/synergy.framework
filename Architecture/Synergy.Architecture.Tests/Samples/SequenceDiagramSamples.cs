using Synergy.Architecture.Annotations.Diagrams.Sequence;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Architecture.Diagrams.Sequence;
using Synergy.Documentation.Annotations;
using Synergy.Documentation.Code;
using static Synergy.Architecture.Annotations.Diagrams.Sequence.SequenceDiagramGroupType;

namespace Synergy.Architecture.Tests.Samples;

[CodeFilePath]
public class SequenceDiagramSamples
{
    public static CodeFile SequenceDiagrams => CodeFolder.Current()
                                                         .File($"{nameof(SequenceDiagramSamples)}.md");

    [Fact]
    public async Task Sequence()
    {
        var blueprint = TechnicalBlueprint
                        .Titled("Sequence diagrams samples")
                        .Add(this.IfElseDiagrams())
                        .Add(this.LoopAfterLoopDiagram())
                        .Add(this.DatabaseDiagrams())
                        .Add(this.OverrideMessageAndResultDiagrams())
            ;

        await File.WriteAllTextAsync(SequenceDiagrams.FilePath, blueprint.Render());
    }

    private IEnumerable<SequenceDiagram> IfElseDiagrams()
    {
        yield return SequenceDiagram
                     .From(new SequenceDiagramActor("Some Actor", Note: "very hand some"))
                     .Calling<SequenceDiagramSamples>(c => c.IfElse())
                     .Footer("This diagram shows if-else."
                     );
    }

    [SequenceDiagramExternalCall("Chrome", "https://www.google.com", Group = SequenceDiagramGroupType.Alt, GroupHeader = "when google is available")]
    [SequenceDiagramExternalCall("Firefox", "https://www.foogle.com", Group = SequenceDiagramGroupType.Alt, GroupHeader = "when google is available")]
    [SequenceDiagramExternalCall("Firefox", "https://www.google.com", Group = SequenceDiagramGroupType.Else, GroupHeader = "otherwise")]
    private void IfElse()
    {
    }

    private IEnumerable<SequenceDiagram> LoopAfterLoopDiagram()
    {
        yield return SequenceDiagram
                     .From(new SequenceDiagramActor("Upset Actor", Note: "very upset", Colour: "red"))
                     .Calling<SequenceDiagramSamples>(c => c.LoopAfterLoop())
                     .Footer("This diagram shows loop placed after another loop."
                     );
    }

    [SequenceDiagramExternalCall("Chrome", "https://www.google.com", Group = Loop, GroupHeader = "Looping until something happens")]
    [SequenceDiagramExternalCall("Firefox", "https://www.foogle.com", Group = Loop, GroupHeader = "Looping until something happens")]
    [SequenceDiagramExternalCall("Firefox", "https://www.google.com", Group = Loop, GroupHeader = "This should be different loop")]
    private void LoopAfterLoop()
    {
    }

    private IEnumerable<SequenceDiagram> DatabaseDiagrams()
    {
        yield return SequenceDiagram
                     .From(new SequenceDiagramActor("Some\\nactor", Archetype:SequenceDiagramArchetype.Participant))
                     .Calling<SequenceDiagramSamples>(c => c.Upsert())
                     .Footer("This diagram shows standard database operations."
                     );
    }

    [SequenceDiagramDatabaseCall($"SELECT * FROM [Item] WHERE [Id] = @itemId")]
    [SequenceDiagramDatabaseCall($"INSERT INTO [Item] VALUES ...", Group = Alt, GroupHeader = "if item does not exist yet")]
    [SequenceDiagramDatabaseCall($"UPDATE [Item] SET ... WHERE [Id] = @itemId", Group = Else, GroupHeader = "else")]
    private void Upsert()
    {
    }
    
    private IEnumerable<SequenceDiagram> OverrideMessageAndResultDiagrams()
    {
        yield return SequenceDiagram
                     .From(new SequenceDiagramActor("Some\\nactor", Archetype:SequenceDiagramArchetype.Control))
                     .Calling<SequenceDiagramSamples>(c => c.OverrideMessageAndResult())
                     .Footer("This diagram shows how to override message and result for ordinary [SequenceDiagramCall]."
                     );
    }

    [SequenceDiagramCall(typeof(Helper), nameof(Helper.SomeStaticMethod), 
        Message = "GET https://www.google.com",
        Result = "200 OK")]
    private void OverrideMessageAndResult()
    {
    }
}

internal class Helper
{
    public static void SomeStaticMethod()
    {
    }
}