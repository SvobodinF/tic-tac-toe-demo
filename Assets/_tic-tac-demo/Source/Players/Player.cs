public abstract class Player
{
    public bool CanTurn => _canTurn;

    public readonly string Name;

    protected readonly Input Input;

    private bool _canTurn;

    public Player(string name)
    {
        Name = name;
    }

    public virtual void SetCurrent(bool isCurrent)
    {
        _canTurn = isCurrent;
    }
}
