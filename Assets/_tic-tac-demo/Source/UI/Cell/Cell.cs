using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public event Action<int> OnClickEvent;

    [SerializeField] private Image _icon;

    [SerializeField] private float _animationDuration;

    private int _index;

    public void Init(int index)
    {
        _index = index;
    }

    public void SetSpite(Sprite sprite)
    {
        _icon.sprite = sprite;
        OnSpriteChangeFeedback();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(_index);
    }

    private void OnSpriteChangeFeedback()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, _animationDuration);
    }
}
