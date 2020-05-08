using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuElement : MonoBehaviour
{
    protected MainMenuController controller;
    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected CanvasGroup group;

    public abstract void OnStart();
    public void SetController(MainMenuController _controller)
    {
        controller = _controller;
    }
    public virtual void Show()
    {
        canvas.enabled = true;
        group.blocksRaycasts = true;
    }
    public virtual void Hide()
    {
        canvas.enabled = false;
        group.blocksRaycasts = false;
    }
}
