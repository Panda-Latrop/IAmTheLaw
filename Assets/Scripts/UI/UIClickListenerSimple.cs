using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIClickListenerSimple : MonoBehaviour, IPointerClickHandler
{
    protected event Action<PointerEventData> OnClick;
    public void AddHandler(Action<PointerEventData> _clickHandler)
    {
        OnClick += _clickHandler;
    }
    public void OnPointerClick(PointerEventData _eventData)
    {
        OnClick?.Invoke(_eventData);
    }
    protected void OnDestroy()
    {
        OnClick = null;
    }
}
