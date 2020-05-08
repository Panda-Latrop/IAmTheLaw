using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DocumentMenu : MenuElement
{
    
    protected GameSetting setting;
    protected ScriptableCase case_;
    protected GameCaseStruct gameCase;
    [SerializeField]
    protected UIClickListenerSimple back,left,right,bride;
    [SerializeField]
    protected BrideMenu brideMenu;
    [SerializeField]
    protected DocumentMenuView view;
    [SerializeField]
    protected int toBack,toBride;

    public override void Show()
    {
        base.Show();
        view.SetText(case_);
        view.HideBride();
        view.ShowBack();
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }
    public void SetGameCase(GameCaseStruct _case) => gameCase = _case;
    public void SetCase(ScriptableCase _case) => case_ = _case;
    protected void Bride(PointerEventData _eventData)
    {
        brideMenu.SetCase(case_);
        brideMenu.SetGameCase(gameCase);
        controller.ShowMenu(toBride);
    }
    protected void Left(PointerEventData _eventData)
    {
        view.PreviousPage();
        view.HideBride();
    }
    protected void Right(PointerEventData _eventData)
    {
        view.NextPage();
        if (view.IsEnd && case_.BribeIsAllow)
            view.ShowBride();
        else
            view.HideBride();
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
        bride.AddHandler(Bride);
    }
}
