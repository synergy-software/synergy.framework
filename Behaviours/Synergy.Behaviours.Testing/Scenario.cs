using System.Collections.ObjectModel;

namespace Synergy.Behaviours.Testing;

public record Scenario(string Title, ReadOnlyCollection<string> Tags)
{
    public string Method 
        => FeatureGenerator.ToMethod(this.Title);

    public bool IsTagged(string tag)
        => this.Tags.Any(t => t.TrimStart('@').Equals(tag.TrimStart('@'), StringComparison.InvariantCultureIgnoreCase));
}