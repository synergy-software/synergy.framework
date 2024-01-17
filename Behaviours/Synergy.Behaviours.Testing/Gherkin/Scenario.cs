namespace Synergy.Behaviours.Testing.Gherkin;

public record Scenario(
    string Title,
    List<string> Tags,
    List<Step> Steps,
    Rule? Rule,
    Line Line
)
{
    public bool IsTagged(string tag, params string[] tags)
    {
        if (this.IsTagged(tag))
            return true;
        
        return tags.Any(t => this.IsTagged(t));
    }
    
    private bool IsTagged(string tag)
        => this.Tags.Any(t => t.TrimStart('@').Equals(tag.TrimStart('@'), StringComparison.InvariantCultureIgnoreCase));
}