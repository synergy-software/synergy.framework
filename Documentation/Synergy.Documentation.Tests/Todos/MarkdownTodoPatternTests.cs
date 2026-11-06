using Synergy.Documentation.Todos.Patterns;

namespace Synergy.Documentation.Tests.Todos;

public class MarkdownTodoPatternTests
{
    [Fact]
    public void FindsTodosInLinkReferenceComments()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "# Title",
            "[//]: # (TODO: Write the documentation)",
            "Text with [//]: # (TODO: an inline comment) in a paragraph.",
            "[//]: # (Not a todo)"
        });

        Assert.Equal(
            new[]
            {
                "TODO: Write the documentation",
                "TODO: an inline comment"
            },
            Todos(content)
        );
    }

    [Fact]
    public void LinkReferenceCommentEndsAtFirstParenthesis()
    {
        string content = "[//]: # (TODO: [#1 Story](https://example.com/1) Fix it)";

        Assert.Equal(new[] { "TODO: [#1 Story](https://example.com/1" }, Todos(content));
    }

    [Fact]
    public void FindsTodosWithMarkdownLinksInHtmlComments()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "# Title",
            "<!-- TODO: [#1 Story (DevOps)](https://example.com/1) {by:Jane Doe on:2026-11-06} Fix it -->",
            "  <!-- TODO: Indented comment -->",
            "<!-- Not a todo -->"
        });

        Assert.Equal(
            new[]
            {
                "TODO: [#1 Story (DevOps)](https://example.com/1) {by:Jane Doe on:2026-11-06} Fix it",
                "TODO: Indented comment"
            },
            Todos(content)
        );
    }

    [Fact]
    public void FindsTodosInMultilineHtmlComments()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "<!-- Open points.",
            "     TODO: First point.",
            "     TODO: Second point.",
            "-->"
        });

        Assert.Equal(
            new[]
            {
                "TODO: First point.",
                "TODO: Second point."
            },
            Todos(content)
        );
    }

    [Fact]
    public void IgnoresHtmlCommentsThatDoNotStartALine()
    {
        string content = "Use `<!-- TODO: message -->` to add a TODO.";

        Assert.Empty(Todos(content));
    }

    [Fact]
    public void ReturnsBothFormsInSourceOrder()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "<!-- TODO: First -->",
            "[//]: # (TODO: Second)",
            "<!-- TODO: Third -->"
        });

        Assert.Equal(new[] { "TODO: First", "TODO: Second", "TODO: Third" }, Todos(content));
    }

    [Fact]
    public void DoesNotReportLinkReferenceCommentInsideHtmlCommentTwice()
    {
        string content = string.Join(Environment.NewLine, new[]
        {
            "<!--",
            "[//]: # (TODO: Hidden twice)",
            "-->"
        });

        Assert.Single(Todos(content));
    }

    private static string[] Todos(string content)
    {
        var pattern = new MarkdownTodoPattern();
        return pattern.Matches(content)
                      .Select(pattern.TodoExtractor)
                      .ToArray();
    }
}
