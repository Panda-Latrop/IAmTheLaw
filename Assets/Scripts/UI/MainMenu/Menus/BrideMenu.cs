using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrideMenu : MenuElement
{

    protected GameSetting setting;
    protected ScriptableCase case_;
    protected GameCaseStruct gameCase;
    [SerializeField]
    protected UIClickListenerSimple back, left, right;
    [SerializeField]
    protected BrideMenuView view;
    [SerializeField]
    protected int toBack, toSelect;

    public override void Show()
    {
        base.Show();
        view.SetText(case_, ResourcesSetting.Instance);
        view.ShowBack();
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }
    public void SetGameCase(GameCaseStruct _case) => gameCase = _case;
    public void SetCase(ScriptableCase _case) => case_ = _case;

    protected void Left(PointerEventData _eventData)
    {
        gameCase.BribeAccept = false;
        controller.ShowMenu(toSelect);
    }
    protected void Right(PointerEventData _eventData)
    {
        gameCase.BribeAccept = true;
        controller.ShowMenu(toSelect);
    }
    protected void ToBack(PointerEventData _eventData)
    {
        controller.ShowMenu(toBack);
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        back.AddHandler(ToBack);
        left.AddHandler(Left);
        right.AddHandler(Right);        
    }
}
