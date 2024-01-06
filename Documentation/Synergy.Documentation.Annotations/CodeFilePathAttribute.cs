using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Annotations;

[Conditional("DOCUMENTATION")]
public class CodeFilePathAttribute : Attribute
{
    public string FilePath { get; }

    public CodeFilePathAttribute([CallerFilePath] string filePath = "")
    {
        FilePath = filePath;
    }
}