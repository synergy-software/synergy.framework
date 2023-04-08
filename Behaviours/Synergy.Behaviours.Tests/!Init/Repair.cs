namespace Synergy.Behaviours.Tests;

public class Repair
{
    // HERE
    public static bool Mode = false;

    [Fact]
    public void you_cannot_leave_repair_mode()
    {
        Assert.False(Repair.Mode, "You cannot leave repair mode On. Turn it off.");
    }
}