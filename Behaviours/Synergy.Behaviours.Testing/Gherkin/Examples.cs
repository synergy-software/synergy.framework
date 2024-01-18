namespace Synergy.Behaviours.Testing.Gherkin;

public record Examples(
    List<string> Header,
    List<List<string>> Rows,
    Line Line
)
{
    public const string Keyword = "Examples";
}