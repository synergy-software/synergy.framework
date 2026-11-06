using System.Text.RegularExpressions;

namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Extracts TODOs from Markdown files written in either comment form:
/// <c>[//]: # (TODO ...)</c> or an HTML comment <c>&lt;!-- TODO ... --&gt;</c>.
/// </summary>
/// <remarks>
/// The link-reference form ends at the first <c>)</c>, so its message cannot contain parentheses.
/// The HTML comment form allows any text, including Markdown links, and stays hidden in rendered Markdown.
/// It is collected only when the comment starts a line, so an example in inline code is not reported.
/// </remarks>
public record MarkdownTodoPattern() : TodoPattern(
    "md",
    new Regex("\\[\\/\\/\\]\\:[ \\t]+#[ \\t]+\\((TODO.*?)\\)"), // [//]: # (TODO Write the documentation of Markdown class usage)
    match => match.Groups[1].Value.Trim()
)
{
    private static readonly Regex htmlCommentRegex = new("^[ \\t]*<!--.*?-->", RegexOptions.Singleline | RegexOptions.Multiline);
    private static readonly Regex htmlCommentTodoRegex = new("\\b(TODO[^\\r\\n]*?)(?=\\s*-->|\\r?$)", RegexOptions.Multiline); // <!-- TODO Link the [work item](https://example.com/1) -->

    /// <summary>
    /// Finds TODOs in both comment forms, in source order.
    /// </summary>
    /// <param name="content">The Markdown file contents to search.</param>
    /// <returns>TODO matches from link-reference comments and from HTML comments that start a line.</returns>
    public override IEnumerable<Match> Matches(string content)
    {
        var htmlComments = htmlCommentRegex.Matches(content).ToList();

        var todos = new List<(int Position, Match Todo)>();
        foreach (Match comment in htmlComments)
        foreach (Match todo in htmlCommentTodoRegex.Matches(comment.Value))
            todos.Add((comment.Index + todo.Index, todo));

        foreach (Match todo in Regex.Matches(content))
        {
            if (htmlComments.Any(comment => todo.Index >= comment.Index && todo.Index < comment.Index + comment.Length))
                continue;

            todos.Add((todo.Index, todo));
        }

        foreach (var todo in todos.OrderBy(todo => todo.Position))
            yield return todo.Todo;
    }
}
