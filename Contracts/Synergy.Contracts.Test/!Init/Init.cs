using System.Runtime.CompilerServices;
using DiffEngine;
using NUnit.Framework;
using VerifyTests;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Synergy.Contracts.Test;

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