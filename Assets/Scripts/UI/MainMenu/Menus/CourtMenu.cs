using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CourtMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerSimple accept, refuse;
    [SerializeField]
    protected CourtMenuView view;
    [SerializeField]
    protected CourtControllerV2 courtController;

    public override void Show()
    {
        base.Show();
        view.ShowBack();

    }
    public override void Hide()
    {
        base.Hide();
        view.Hide();
        view.HideBack();
    }
    public void OnAccept(PointerEventData _eventData)
    {
        courtController.PlayerAccept();
    }
    public void OnRefuse(PointerEventData _eventData)
    {
        courtController.PlayerRefuse();
    }
    public void ShowButton()
    {
        view.Show();
    }
    public void HideButton()
    {
        view.Hide();
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        accept.AddHandler(OnAccept);
        refuse.AddHandler(OnRefuse);
    }
}
