using System;
using System.Threading.Tasks;
using UnityEngine;

public class Game
{
    public Action<Player> OnPlayerSwitchedEvent;

    private readonly Player _firstPlayer;
    private readonly Player _secondPlayer;
    private readonly Board _board;

    private Player _currentPlayer;
    private Player _winner;

    public Game(Player firstPlayer, Player secondPlayer, Board board)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
        _board = board;

        _currentPlayer = firstPlayer;
        _currentPlayer.SetCurrent(true);
    }

    public async void Turn(Player player, int cellIndex)
    {
        if (player != _firstPlayer && player != _secondPlayer)
            throw new ArgumentException("Invalid player try turn");

        if (player != _currentPlayer)
            return;

        if (_winner != null)
            return;

        Select(cellIndex);
        await Task.Delay(100);
        FlipPlayers(player);
    }

    private void FlipPlayers(Player currentPlayer)
    {
        if (_winner != null)
            return;

        Debug.Log($"Current: {_currentPlayer.Name}");
        _currentPlayer.SetCurrent(false);

        _currentPlayer = currentPlayer == _firstPlayer ? _secondPlayer : _firstPlayer;

        Debug.Log($"Next: {_currentPlayer.Name}");
        _currentPlayer.SetCurrent(true);
        OnPlayerSwitchedEvent?.Invoke(_currentPlayer);
    }

    private void Select(int index)
    {
        MarkStatus status = GetStatus(_currentPlayer);

        Mark mark = _board.GetMarkByIndex(index);
        mark.ChangeMarkStatus(status);
        bool isWin = _board.CheckFullLine(status);
        bool isTie = _board.IsAllFilled();

        if (isWin == true)
        {
            _winner = _currentPlayer;
            Debug.Log($"{_currentPlayer.Name} is WINNER");
            return;
        }

        if (isTie == true)
        {
            Debug.Log("game is TIE");
        }

    }

    public MarkStatus GetStatus(Player player)
    {
        if (player == _firstPlayer)
            return MarkStatus.X;

        if (player == _secondPlayer)
            return MarkStatus.O;

        return MarkStatus.EMPTY;
    }
}
