using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public event Action<int> OnClickEvent;

    [SerializeField] private Image _icon;

    private int _index;

    public void Init(int index)
    {
        _index = index;
    }

    public void SetSpite(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(_index);
    }
}
