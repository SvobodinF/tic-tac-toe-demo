using System;
using System.Collections.Generic;
using UnityEngine;

public class LineContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] _horizontalLines;
    [SerializeField] private GameObject[] _verticalLines;
    [SerializeField] private GameObject[] _diagonalLines;

    private Board _board;

    public void Init(Board board)
    {
        _board = board;

        _board.OnLineMatchedEvent += ShowLine;

        HideAll();
    }

    public void ShowLine(int index, LineDirection lineDirection)
    {
        HideAll();

        Action<int> showLine = lineDirection switch
        {
            LineDirection.HORIZONTAL => ShowHorizontal,
            LineDirection.VERTICAL => ShowVertical,
            LineDirection.DIAGONAL => ShowDiagonal,
            _ => null,
        };

        showLine?.Invoke(index);
    }

    private void ShowHorizontal(int index)
    {
        _horizontalLines[index].SetActive(true);
    }

    private void ShowVertical(int index)
    {
        _verticalLines[index].SetActive(true);
    }

    private void ShowDiagonal(int index)
    {
        _diagonalLines[index].SetActive(true);
    }

    private void HideAll()
    {
        List<GameObject> lines = new List<GameObject>();
        lines.AddRange(_horizontalLines);
        lines.AddRange(_verticalLines);
        lines.AddRange(_diagonalLines);

        foreach (GameObject line in lines)
        {
            line.SetActive(false);
        }
    }
}

public enum LineDirection
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL
}
