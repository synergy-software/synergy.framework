using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Annotations;

// TODO: Marcin Celej [from: Marcin Celej on: 16-02-2024]: Consider renaming to SourceFileLocationAttribute

[Conditional("DOCUMENTATION")]
public class CodeFilePathAttribute : Attribute
{
    public string FilePath { get; }

    public CodeFilePathAttribute([CallerFilePath] string filePath = "")
    {
        FilePath = filePath;
    }
}