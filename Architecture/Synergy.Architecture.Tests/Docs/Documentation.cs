using Synergy.Architecture.Diagrams.Markdown;

namespace Synergy.Architecture.Tests.Docs;

public class Documentation
{
    [Fact(DisplayName = "Inject PlantUML diagrams into all *.md files starting from the root")]
    public void inject_plantuml_diagrams()
    {
        var root = Root.Folder.Path;
        PlantUmlDiagrams.Process(root, links: false, images: "images\\generated");
    }
}