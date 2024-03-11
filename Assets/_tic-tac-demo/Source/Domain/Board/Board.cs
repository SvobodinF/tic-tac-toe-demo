using System;
using UnityEngine;

public class Board
{
    public event Action<int, LineDirection> OnLineMatchedEvent;

    public readonly int MatrixSize;

    private readonly Mark[] _marks;

    public Board(int size)
    {
        MatrixSize = size * size;
        _marks = new Mark[MatrixSize];

        for (int i = 0; i < _marks.Length; i++)
        {
            _marks[i] = new Mark();
        }
    }

    public Mark GetMarkByIndex(int index)
    {
        return _marks[index];
    }

    public int[] ToInt32Array()
    {
        int[] result = new int[_marks.Length];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = (int)_marks[i].MarkStatus;
        }

        return result;
    }

    public bool CheckFullLine(MarkStatus markStatus)
    {
        return CheckHorizontal(markStatus) || CheckVertical(markStatus) || CheckDiagonals(markStatus);
    }

    public bool IsAllFilled()
    {
        for (int i = 0; i < _marks.Length; i++)
        {
            if (_marks[i].MarkStatus == MarkStatus.EMPTY)
            {
                return false;
            }
        }

        return true;
    }

    private bool CheckHorizontal(MarkStatus markStatus)
    {
        int size = (int)Mathf.Sqrt(_marks.Length);

        for (int i = 0; i < size; i++)
        {
            int matchCount = 0;

            for (int j = 0; j < size; j++)
            {
                int index = i * size + j;
                if (_marks[index].MarkStatus == markStatus)
                {
                    matchCount++;
                }
            }

            if (matchCount == size)
            {
                OnLineMatchedEvent?.Invoke(i, LineDirection.HORIZONTAL);
                return true;
            }
        }

        return false;
    }

    private bool CheckVertical(MarkStatus markStatus)
    {
        int size = (int)Mathf.Sqrt(_marks.Length);

        for (int i = 0; i < size; i++)
        {
            int matchCount = 0;

            for (int j = 0; j < size; j++)
            {
                int index = i + size * j;
                if (_marks[index].MarkStatus == markStatus)
                {
                    matchCount++;
                }
            }

            if (matchCount == size)
            {
                OnLineMatchedEvent?.Invoke(i, LineDirection.VERTICAL);
                return true;
            }
        }

        return false;
    }

    private bool CheckDiagonals(MarkStatus markStatus)
    {
        int size = (int)Mathf.Sqrt(_marks.Length);

        int mainDiagonalMatchCount = 0;
        int secondDiagonalMatchCount = 0;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int index = i * size + j;

                if (i == j)
                {
                    if (_marks[index].MarkStatus == markStatus)
                    {
                        mainDiagonalMatchCount++;
                    }
                }

                if (i == Mathf.Abs(j - (size - 1)))
                {
                    if (_marks[index].MarkStatus == markStatus)
                    {
                        secondDiagonalMatchCount++;
                    }
                }
            }

            if (mainDiagonalMatchCount == size || secondDiagonalMatchCount == size)
            {
                int index = mainDiagonalMatchCount == size ? 0 : 1;
                OnLineMatchedEvent?.Invoke(index, LineDirection.DIAGONAL);
                return true;
            }
        }

        return false;
    }
}
