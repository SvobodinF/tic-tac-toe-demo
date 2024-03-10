using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Viewport")]
    [SerializeField] private BoardScreen _boardScreen;

    [Header("Prefabs")]
    [SerializeField] private Cell _cellPrefab;

    private void Start()
    {
        Player firstPlayer = new User("Player 1");
        //Player secondPlayer = new User("Player 2");
        Player secondPlayer = new PC();

        Board board = new Board(3);
        Game game = new Game(firstPlayer, secondPlayer, board);
        UserInput firstUserInput = new UserInput(game, firstPlayer);
        AIInput secondUserInput = new AIInput(secondPlayer, game, board);

        _boardScreen.Init(board, new UserInput[]{ firstUserInput }, _cellPrefab);
    }
}
