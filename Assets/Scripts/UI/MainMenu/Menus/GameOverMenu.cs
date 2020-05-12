using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverMenu : MenuElement
{
    protected ResourcesSetting resources;
    [SerializeField]
    protected UIClickListenerSimple exitB;
    [SerializeField]
    protected GameOverMenuView view;

    public override void Show()
    {
        base.Show();
        view.Show(resources);
        view.ShowBack();
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }

    protected void ToExit(PointerEventData _eventData)
    {
        SceneLoader.Load(SceneLoader.MAIN_MENU);
    }
    
    public override void OnStart()
    {
        resources = ResourcesSetting.Instance;
        exitB.AddHandler(ToExit);
    }
}
