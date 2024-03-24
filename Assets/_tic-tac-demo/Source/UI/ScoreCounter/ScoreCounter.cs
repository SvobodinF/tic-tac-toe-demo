using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Image _userIcon;
    [SerializeField] private TMP_Text _counter;

    public void Init(Sprite sprite)
    {
        _userIcon.sprite = sprite;
    }

    public void SetScore(int score)
    {
        _counter.text = $"{score}";
    }
}
