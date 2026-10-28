using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Synergy.Documentation.Annotations;

/// <summary>
/// Associates a type with its source file, capturing the file path automatically at compile time.
/// </summary>
/// <remarks>
/// Attribute usages are emitted only when the DOCUMENTATION compilation symbol is defined.
/// </remarks>
[Conditional("DOCUMENTATION")]
public class CodeFilePathAttribute : Attribute
{
    /// <summary>
    /// Gets the source file path associated with the annotated type.
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Initializes the attribute with an explicit path or the caller's source file path.
    /// </summary>
    /// <param name="filePath">
    /// The source file path. When omitted, the compiler supplies the path of the file containing the attribute usage.
    /// </param>
    public CodeFilePathAttribute([CallerFilePath] string filePath = "")
    {
        FilePath = filePath;
    }
}