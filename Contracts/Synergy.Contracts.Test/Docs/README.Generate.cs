using System.IO;
using Synergy.Documentation.Code;
using Xunit;

namespace Synergy.Contracts.Test.Docs;

partial class README
{
    private static readonly CodeFile readmeFile = CodeFolder.Current().Up(2).File($"{nameof(README)}.md");
    
    [Fact]
    public void Generate()
    {
        // TODO: Marcin Celej [from: Marcin Celej on: 03-02-2024]: Prepare full description of Contract checks
        var content = this.TransformText();
        File.WriteAllText(readmeFile.FilePath, content);
    }
}