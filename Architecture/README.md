# Synergy.Architecture nuget packages

Here is the documentation of the `Synergy.Architecture.Diagrams` and `Synergy.Architecture.Annotations` nuget packages.
They were created to help developers to add sequence diagrams from code.
It is based on the idea of *Diagrams as Code*
 
Find packages on nuget: 
- [Synergy.Architecture.Diagrams](https://www.nuget.org/packages/Synergy.Architecture.Diagrams/)
- [Synergy.Architecture.Annotations](https://www.nuget.org/packages/Synergy.Architecture.Annotations/)

## Generating Sequence Diagrams from code

To generate sequence diagrams from code, we use the following tool:

```csharp
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Architecture.Diagrams.Sequence;
using Synergy.Documentation.Code;
using Synergy.Sample.Web.API.Controllers;
using Synergy.Sample.Web.API.Services.Users.Commands.CreateUser;
using Synergy.Sample.Web.API.Services.Users.Domain;
using Synergy.Sample.Web.API.Services.Users.Queries.GetUser;
using Synergy.Sample.Web.API.Services.Users.Queries.GetUsers;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Architecture;

public class Diagrams
{
    public static CodeFile CodeFile => CodeFile.Current();
    public static CodeFile SequenceDiagrams => CodeFolder.Current()
                                                         .File("Diagrams.of.Sequence.for.Sample.md");

    [Fact]
    public async Task Sequence()
    {
        var blueprint = TechnicalBlueprint
                        .Titled("Sequence diagrams for Sample Web API management")
                        .Register<ICreateUserCommandHandler, CreateUserCommandHandler>()
                        .Register<IGetUsersQueryHandler, GetUsersQueryHandler>()
                        .Register<IGetUserQueryHandler, GetUserQueryHandler>()
                        .Register<IUserRepository, UserRepository>()
                        .Add(Create())
                        .Add(Read())
            ;

        await File.WriteAllTextAsync(Diagrams.SequenceDiagrams.FilePath, blueprint.Render());

        //
        // WARN: Temporarily removed using Verify due to problem with PlantUml which generates different link to image on server side even thought the diagram content is the same
        //

        // await Verify(blueprint.Render())
        //     .UseFileName(RequisitionSequenceDiagrams.FileNameWithoutExtension)
        //     .UseExtension(RequisitionSequenceDiagrams.Extension);
    }

    private static IEnumerable<SequenceDiagram> Create()
    {
        yield return SequenceDiagram
                     .From(Actors.Browser)
                     .Calling<UsersController>(c => c.Create(null!, null!))
                     .Footer("This diagram shows the full path of user creation.\\n" +
                             "To see what happens next - check the next diagrams below."
                     );
    }
    
    private static IEnumerable<SequenceDiagram> Read()
    {
        yield return SequenceDiagram
                     .From(Actors.Browser)
                     .Calling<UsersController>(c => c.GetUsers());
        
        yield return SequenceDiagram
                     .From(Actors.Browser)
                     .Calling<UsersController>(c => c.GetUser(null!));
    }
}
```

For sample code, please check: [Diagrams.cs](../Web/Sample/Synergy.Sample.Web.API.Tests/Architecture/Diagrams.cs)

To see the results, please check: [Diagrams.of.Sequence.for.Sample.md](../Web/Sample/Synergy.Sample.Web.API.Tests/Architecture/Diagrams.of.Sequence.for.Sample.md)

**Note:**

To make it working in your project, you need to decorate your code with the following attributes:

- [SequenceDiagramActivation]
- [SequenceDiagramCall]
- [SequenceDiagramDatabaseCall]
- [SequenceDiagramDeactivation]
- [SequenceDiagramElement]
- [SequenceDiagramExternalActivation]
- [SequenceDiagramExternalCall]
- [SequenceDiagramNote]
- [SequenceDiagramSelfCall]

For sample code, please check:

- [GetUserQueryHandler.cs](../Web/Sample/Synergy.Sample.Web.API.Services/Users/Queries/GetUser/GetUserQueryHandler.cs)
- [UserRepository.cs](../Web/Sample/Synergy.Sample.Web.API.Services/Users/Domain/UserRepository.cs)

### Sample

Check also the following sample ([SequenceDiagramSamples.cs](Synergy.Architecture.Tests/Samples/SequenceDiagramSamples.cs)):

```csharp
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
```

This above test produces: [SequenceDiagramSamples.md](Synergy.Architecture.Tests/Samples/SequenceDiagramSamples.md)

