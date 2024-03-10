public class UserInput : Input
{
    private readonly Game _game;

    public UserInput(Game game, Player player) : base(player)
    {
        _game = game;
    }

    public void AddCell(Cell cell)
    {
        cell.OnClickEvent += OnMarkSelected;
    }

    private void OnMarkSelected(int index)
    {
        if (Player.CanTurn == false)
            return;

        _game.Turn(Player, index);
    }
}
