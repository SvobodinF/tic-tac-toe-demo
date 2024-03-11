using System;

public class ScoreService
{
    public event Action<int> OnScoreChangedEvent;

    private readonly IStorage _storage;
    private readonly Player _player;

    private int _score;

    public ScoreService(IStorage storage, Player player, Game game)
    {
        _storage = storage;
        _player = player;

        game.OnMatchEndEvent += IncreaseScore;
    }

    public void Init()
    {
        Load();
    }

    private void IncreaseScore(Player player, MatchResultStatus matchResultStatus)
    {
        if (player == null)
            return;

        if (player != _player || matchResultStatus != MatchResultStatus.WIN)
            return;

        _score++;
        Save();

        OnScoreChangedEvent?.Invoke(_score);
    }

    private void Load()
    {
        _storage.LoadByKey(_player.SAVE_KEY, out _score, 0);
        OnScoreChangedEvent?.Invoke(_score);
    }

    private void Save()
    {
        _storage.SaveByKey(_player.SAVE_KEY, _score);
    }
}
