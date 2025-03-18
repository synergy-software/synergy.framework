using Synergy.Documentation.Code;

namespace Synergy;

public static class Root
{
    private static readonly CodeFile Solution = CodeFolder.Current().File("Synergy.Framework.sln");
    public static readonly CodeFolder Folder = CodeFolder.Current();
}