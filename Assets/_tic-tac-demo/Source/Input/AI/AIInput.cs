using System.Threading.Tasks;

public class AIInput : Input
{
    private readonly MiniMax _miniMax;
    private readonly Game _game;
    private readonly Board _board;

    public AIInput(Player player, Game game, Board board) : base(player)
    {
        _miniMax = new MiniMax();
        _game = game;
        _board = board;

        _game.OnPlayerSwitchedEvent += (player) => OnPlayerSwitched(player);
    }

    private void Turn()
    {
        int aiPlayChoice = _game.GetStatus(Player) == MarkStatus.X ? -1 : 1;
        _miniMax.aiPick = aiPlayChoice;

        EndTurnPosition bestPositionToEndTurn = _miniMax.GetBestPosition(aiPlayChoice, _board.ToInt32Array(), 0);
        int newBestPositionToPick = bestPositionToEndTurn.position;

        _game.Turn(Player, newBestPositionToPick);
    }

    private async void OnPlayerSwitched(Player player)
    {
        if (player != Player)
            return;

        await Task.Delay(300);

        Turn();
    }
}
