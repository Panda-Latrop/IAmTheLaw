using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIClickListenerArg : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected int arg;
    protected event Action<PointerEventData,int> OnClick;
    public void AddHandler(Action<PointerEventData,int> _clickHandler)
    {
        OnClick += _clickHandler;
    }
    public void OnPointerClick(PointerEventData _eventData)
    {
        OnClick?.Invoke(_eventData, arg);
    }
    protected void OnDestroy()
    {
        OnClick = null;
    }
}
