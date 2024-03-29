using System;
using System.Threading.Tasks;
using UnityEngine;

public class Game
{
    public Action<Player, MatchResultStatus> OnMatchEndEvent;
    public Action<Player> OnPlayerSwitchedEvent;

    private readonly Player _firstPlayer;
    private readonly Player _secondPlayer;
    private readonly Board _board;
    private readonly SceneLoader _sceneLoader;

    private Player _currentPlayer;
    private Player _winner;
    private bool _isTie;

    public Game(Player firstPlayer, Player secondPlayer, Board board, SceneLoader sceneLoader)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
        _board = board;
        _sceneLoader = sceneLoader;

        _currentPlayer = firstPlayer;
        _currentPlayer.SetCurrent(true);
    }

    public MarkStatus GetStatus(Player player)
    {
        if (player == _firstPlayer)
            return MarkStatus.X;

        if (player == _secondPlayer)
            return MarkStatus.O;

        return MarkStatus.EMPTY;
    }

    public async void Turn(Player player, int cellIndex)
    {
        if (player != _firstPlayer && player != _secondPlayer)
            throw new ArgumentException("Invalid player try turn");

        if (player != _currentPlayer)
            return;

        if (_winner != null || _isTie)
            return;

        Select(cellIndex);
        await Task.Delay(100);
        FlipPlayers(player);
    }

    public void Restart()
    {
        if (_winner == null && _isTie == false)
            return;

        _sceneLoader.LoadScene(SceneNameContainer.GAMEPLAY);
    }

    private void FlipPlayers(Player currentPlayer)
    {
        if (_winner != null || _isTie)
            return;

        _currentPlayer.SetCurrent(false);

        _currentPlayer = currentPlayer == _firstPlayer ? _secondPlayer : _firstPlayer;

        _currentPlayer.SetCurrent(true);
        OnPlayerSwitchedEvent?.Invoke(_currentPlayer);
    }

    private void Select(int index)
    {
        MarkStatus status = GetStatus(_currentPlayer);

        Mark mark = _board.GetMarkByIndex(index);
        mark.ChangeMarkStatus(status);
        CheckIsMatchEnd(status);
    }

    private void CheckIsMatchEnd(MarkStatus status)
    {
        bool isWin = _board.CheckFullLine(status);
        _isTie = _board.IsAllFilled();
        MatchResultStatus matchResultStatus = MatchResultStatus.PLAYING;

        if (isWin == false && _isTie == false)
            return;

        if (isWin == true)
        {
            _winner = _currentPlayer;
            Debug.Log($"{_currentPlayer.Name} is WINNER");
            matchResultStatus = MatchResultStatus.WIN;
        }
        
        if (_isTie == true)
        {
            matchResultStatus = MatchResultStatus.TIE;
        }

        OnMatchEndEvent.Invoke(_winner, matchResultStatus);
    }
}
