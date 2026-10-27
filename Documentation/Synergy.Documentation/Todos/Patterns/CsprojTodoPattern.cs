namespace Synergy.Documentation.Todos.Patterns;

/// <summary>
/// Represents a pattern for extracting TODOs from *.csproj files.
/// </summary>
/// <remarks>
/// The TODO must be inside an XML comment.
/// </remarks>
public record CsprojTodoPattern() : XmlTodoPattern("csproj");