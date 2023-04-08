﻿using System.Runtime.CompilerServices;
using DiffEngine;
using VerifyTests;

namespace Synergy.Markdowns.Test;

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