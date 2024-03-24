using UnityEngine;

public class BoardScreen : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Sprite _xSprite;
    [SerializeField] private Sprite _oSprite;

    private Board _board;
    private UserInput[] _userInput;
    private Cell _cellPrefab;

    public void Init(Board board, UserInput[] userInput, Cell cellPrefab)
    {
        _board = board;
        _userInput = userInput;
        _cellPrefab = cellPrefab;

        CreateCells(_board.MatrixSize);
    }

    private void CreateCells(int matrixSize)
    {
        for (int i = 0; i < matrixSize; i++)
        {
            Cell cell = Instantiate(_cellPrefab, _container);
            cell.Init(i);

            foreach (UserInput userInput in _userInput)
                userInput.AddCell(cell);

            Mark mark = _board.GetMarkByIndex(i);
            
            mark.OnStatusChangedEvent += (status) => OnMarkStatusChanged(status, cell);
        }
    }

    private void OnMarkStatusChanged(MarkStatus markStatus, Cell cell)
    {
        Sprite sprite = markStatus switch
        {
            MarkStatus.O => _oSprite,
            MarkStatus.X => _xSprite,
            _ => null
        };

        cell.SetSpite(sprite);
    }
}
