using System.Threading.Tasks;
using UnityEngine;

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

        int[] array = _board.ToInt32Array();
        string debug = "";
        foreach (int value in array)
        {
            debug += value + ",";
        }
        Debug.Log(debug);
        EndTurnPosition bestPositionToEndTurn = _miniMax.GetBestPosition(aiPlayChoice, array, 0);
        int newBestPositionToPick = bestPositionToEndTurn.position;

        Debug.Log(newBestPositionToPick);

        _game.Turn(Player, newBestPositionToPick);
    }

    private async void OnPlayerSwitched(Player player)
    {
        Debug.Log("Player switched");
        if (player != Player)
            return;

        Debug.Log("Wait AI TURN");
        await Task.Delay(300);

        Debug.Log("AI TURN");
        Turn();
    }
}
