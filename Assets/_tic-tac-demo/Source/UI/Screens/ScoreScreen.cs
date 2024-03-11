using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private ScoreCounter _firstPlayerCounter;
    [SerializeField] private ScoreCounter _secondPlayerCounter;

    public void Init(UserInfo firstPlayer, UserInfo secondPlayer)
    {
        _firstPlayerCounter.Init(firstPlayer.Icon);
        _secondPlayerCounter.Init(secondPlayer.Icon);

        firstPlayer.ScoreService.OnScoreChangedEvent += (count) => UpdateCounter(_firstPlayerCounter, count);
        secondPlayer.ScoreService.OnScoreChangedEvent += (count) => UpdateCounter(_secondPlayerCounter, count);
    }

    private void UpdateCounter(ScoreCounter scoreCounter, int count)
    {
        scoreCounter.SetScore(count);
    }
}

public struct UserInfo
{
    public readonly Sprite Icon;
    public readonly ScoreService ScoreService;

    public UserInfo(Sprite icon, ScoreService scoreService)
    {
        Icon = icon;
        ScoreService = scoreService;
    }
}