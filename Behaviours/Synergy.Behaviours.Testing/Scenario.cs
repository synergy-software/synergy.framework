using System.Collections.ObjectModel;

namespace Synergy.Behaviours.Testing;

public record Scenario(string Title, ReadOnlyCollection<string> Tags)
{
    public string Method
        => FeatureGenerator.ToMethod(this.Title);
    
    public bool IsTagged(string tag, params string[] tags)
    {
        if (this.IsTagged(tag))
            return true;
        
        return tags.Any(t => this.IsTagged(t));
    }
    
    private bool IsTagged(string tag)
        => this.Tags.Any(t => t.TrimStart('@')
                               .Equals(tag.TrimStart('@'), StringComparison.InvariantCultureIgnoreCase));
}