using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Synergy.Architecture.Diagrams;
using Synergy.Architecture.Diagrams.Documentation;
using Synergy.Architecture.Diagrams.Sequence;
using Synergy.Documentation.Code;
using Synergy.Sample.Web.API.Controllers;
using Synergy.Sample.Web.API.Services.Users.Commands.CreateUser;
using Synergy.Sample.Web.API.Services.Users.Domain;
using Xunit;

namespace Synergy.Sample.Web.API.Tests.Architecture;

public class Diagrams
{
    public static CodeFile SequenceDiagrams => CodeFolder.Current()
                                                         .File("Diagrams.of.Sequence.for.Sample.md");

    [Fact]
    public async Task Sequence()
    {
        var blueprint = TechnicalBlueprint
                        .Titled("Sequence diagrams for Sample Web API management")
                        .Register<ICreateUserCommandHandler, CreateUserCommandHandler>()
                        .Register<IUserRepository, UserRepository>()
                        .Add(Create())
                        .Add(Read())
            ;

        File.WriteAllText(SequenceDiagrams.FilePath, blueprint.Render());

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
    }
}