using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Viewport")]
    [SerializeField] private BoardScreen _boardScreen;
    [SerializeField] private ScoreScreen _scoreScreen;

    [Header("Prefabs")]
    [SerializeField] private Cell _cellPrefab;

    [Header("ScriptableObjects")]
    [SerializeField] private UserIconContainer _userIconContainer;

    private void Start()
    {
        IStorage storage = new PlayerPrefsStorage();

        Player firstPlayer = new User("Player 1", _userIconContainer);
        //Player secondPlayer = new User("Player 2");
        Player secondPlayer = new PC(_userIconContainer);

        Board board = new Board(3);
        Game game = new Game(firstPlayer, secondPlayer, board);

        UserInput firstUserInput = new UserInput(game, firstPlayer);
        AIInput secondUserInput = new AIInput(secondPlayer, game, board);

        ScoreService firstScore = new ScoreService(storage, firstPlayer, game);
        ScoreService secondScore = new ScoreService(storage, secondPlayer, game);

        UserInfo firstPlayerInfo = new UserInfo(firstPlayer.Icon, firstScore);
        UserInfo secondPlayerInfo = new UserInfo(secondPlayer.Icon, secondScore);

        _boardScreen.Init(board, new UserInput[]{ firstUserInput }, _cellPrefab);
        _scoreScreen.Init(firstPlayerInfo, secondPlayerInfo);

        firstScore.Init();
        secondScore.Init();
    }
}
