namespace Synergy.Behaviours.Testing.Gherkin;

public record Examples(
    Examples.Row Header,
    List<Examples.Row> Rows,
    Line Line
)
{
    public const string Keyword = "Examples";
    
    public record Row(
        List<string> Values,
        Line Line
    )
    {
        public const string Keyword = "|";
    }
}