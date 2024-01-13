using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DiffEngine;

[assembly: ExcludeFromCodeCoverage]

namespace Synergy.Architecture.Tests;

public static class Init
{
    [ModuleInitializer]
    public static void Initialize()
    {
        if (Repair.Mode)
            VerifierSettings.AutoVerify();

        DiffTools.UseOrder(DiffTool.Rider, DiffTool.VisualStudioCode, DiffTool.VisualStudio, DiffTool.WinMerge);
    }
}