using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _matchResultText;
    [SerializeField] private Button _restartButton;

    private Game _game;

    public void Init(Game game)
    {
        _game = game;

        SetText("");

        _restartButton.onClick.AddListener(_game.Restart);

        _game.OnMatchEndEvent += OnMatchEnd;
        _restartButton.gameObject.SetActive(false);
    }

    private void SetText(string text)
    {
        _matchResultText.text = text;
    }

    private void OnMatchEnd(Player player, MatchResultStatus matchResultStatus)
    {
        string playerName = matchResultStatus != MatchResultStatus.TIE ? player.Name : "";

        SetText($"{playerName}{GetPostfix(matchResultStatus)}");
        _restartButton.gameObject.SetActive(true);
    }

    private string GetPostfix(MatchResultStatus matchResultStatus)
    {
        return matchResultStatus switch
        {
            MatchResultStatus.LOSE => " - LOSE",
            MatchResultStatus.WIN => " - WIN",
            MatchResultStatus.TIE => "TIE",
            _ => "",
        };
    }
}
