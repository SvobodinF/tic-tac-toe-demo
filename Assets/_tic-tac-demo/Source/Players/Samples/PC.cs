public class PC : Player
{
    public PC(UserIconContainer userIconContainer) : base("PC", userIconContainer)
    {
    }

    public override void SetCurrent(bool isCurrent)
    {
        base.SetCurrent(isCurrent);
    }
}
