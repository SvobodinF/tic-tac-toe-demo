using UnityEngine;

public abstract class Player : ISaveble
{
    public bool CanTurn => _canTurn;

    public string SAVE_KEY => $"USER_{Name}";

    public readonly string Name;
    public readonly Sprite Icon;
    protected readonly Input Input;

    private bool _canTurn;

    public Player(string name, UserIconContainer userIconContainer)
    {
        Name = name;
        Icon = userIconContainer.GetSprite(this);
    }

    public virtual void SetCurrent(bool isCurrent)
    {
        _canTurn = isCurrent;
    }
}
