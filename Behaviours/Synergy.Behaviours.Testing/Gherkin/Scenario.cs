namespace Synergy.Behaviours.Testing.Gherkin;

public record Scenario(
    string Title,
    List<string> Tags,
    List<Step> Steps,
    Rule? Rule,
    Line Line
)
{
    public static string[] Keywords { get; } = { "Scenario", "Example" };

    public bool IsTagged(string tag, params string[] tags)
    {
        if (this.IsTagged(tag))
            return true;
        
        return tags.Any(t => this.IsTagged(t));
    }
    
    private bool IsTagged(string tag)
        => this.Tags.Any(t => t.TrimStart('@').Equals(tag.TrimStart('@'), StringComparison.InvariantCultureIgnoreCase));

    public virtual string[] Lines
    {
        get
        {
            var lines = new List<string>(1 + this.Steps.Count);
            lines.Add(this.Line.Text);
            lines.AddRange(this.Steps.Select(step => step.Line.Text));
            return lines.ToArray();
        }
    }
}