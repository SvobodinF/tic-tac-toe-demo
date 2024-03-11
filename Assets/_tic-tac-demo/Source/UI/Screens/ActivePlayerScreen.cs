using UnityEngine;

public class ActivePlayerScreen : MonoBehaviour
{
    [SerializeField] private ActivePlayer _firstPlayerMarker;
    [SerializeField] private ActivePlayer _secondPlayerMarker;

    private Player _firstPlayer;
    private Player _secondPlayer;
    private Game _game;

    public void Init(Player firstPlayer, Player secondPlayer, Game game)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
        _game = game;

        _game.OnPlayerSwitchedEvent += ToggleActive;
        _game.OnMatchEndEvent += OnMatchEnd;

        ToggleActive(firstPlayer);
    }

    private void ToggleActive(Player player)
    {
        if (player == null)
        {
            _firstPlayerMarker.SetActive(false);
            _secondPlayerMarker.SetActive(false);
            return;
        }

        _firstPlayerMarker.SetActive(_firstPlayer == player);
        _secondPlayerMarker.SetActive(_secondPlayer == player);
    }

    private void OnMatchEnd(Player player, MatchResultStatus matchResultStatus)
    {
        ToggleActive(player);
    }
}
