using Synergy.Documentation.Todos.Patterns;

namespace Synergy.Documentation.Tests.Todos;

public class XmlTodoPatternTests
{
    [Fact]
    public void FindsTodosInMultilineXmlComments()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "<!-- Security override.",
            "     TODO: Remove the direct package references once the dependency is patched.",
            "     TODO: Verify the override is no longer needed.",
            "-->",
            "<PropertyGroup>",
            "    <Description>TODO: This is not a comment.</Description>",
            "</PropertyGroup>"
        });

        var pattern = new PropsTodoPattern();
        string[] todos = pattern.Matches(content)
                                .Select(pattern.TodoExtractor)
                                .ToArray();

        Assert.Equal(
            new[]
            {
                "TODO: Remove the direct package references once the dependency is patched.",
                "TODO: Verify the override is no longer needed."
            },
            todos
        );
    }
}
