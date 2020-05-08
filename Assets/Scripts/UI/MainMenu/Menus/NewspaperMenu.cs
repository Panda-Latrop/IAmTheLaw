using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewspaperMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerSimple back;
    [SerializeField]
    protected NewspaperMenuView view;
    [SerializeField]
    protected int toBack;

    public override void Show()
    {
        base.Show();
        view.ShowBack();   
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }
    protected void ToBack(PointerEventData _eventData)
    {
        controller.ShowMenu(toBack);
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        back.AddHandler(ToBack);
        view.Show(setting.Day.NewspaperText,ResourcesSetting.Instance);
    }

}
