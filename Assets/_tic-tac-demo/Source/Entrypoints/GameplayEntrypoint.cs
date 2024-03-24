using UnityEngine;

public class GameplayEntrypoint : Entrypoint
{
    [Header("Viewport")]
    [SerializeField] private BoardScreen _boardScreen;
    [SerializeField] private ScoreScreen _scoreScreen;
    [SerializeField] private LineContainer _lineContainer;
    [SerializeField] private ActivePlayerScreen _activePlayerScreen;
    [SerializeField] private FinalScreen _finalScreen;

    [Header("Prefabs")]
    [SerializeField] private Cell _cellPrefab;

    [Header("ScriptableObjects")]
    [SerializeField] private UserIconContainer _userIconContainer;

    private void Start()
    {
        IStorage storage = new PlayerPrefsStorage();
        SceneLoader sceneLoader = new SceneLoader();

        Player firstPlayer = new User("Player 1", _userIconContainer);
        //Player secondPlayer = new User("Player 2");
        Player secondPlayer = new PC(_userIconContainer);

        Board board = new Board(3);
        Game game = new Game(firstPlayer, secondPlayer, board, sceneLoader);

        UserInput firstUserInput = new UserInput(game, firstPlayer);
        AIInput secondUserInput = new AIInput(secondPlayer, game, board);

        ScoreService firstScore = new ScoreService(storage, firstPlayer, game);
        ScoreService secondScore = new ScoreService(storage, secondPlayer, game);

        UserInfo firstPlayerInfo = new UserInfo(firstPlayer.Icon, firstScore);
        UserInfo secondPlayerInfo = new UserInfo(secondPlayer.Icon, secondScore);

        _boardScreen.Init(board, new UserInput[]{ firstUserInput }, _cellPrefab);
        _scoreScreen.Init(firstPlayerInfo, secondPlayerInfo);
        _lineContainer.Init(board);
        _activePlayerScreen.Init(firstPlayer, secondPlayer, game);
        _finalScreen.Init(game);

        firstScore.Init();
        secondScore.Init();
    }
}
