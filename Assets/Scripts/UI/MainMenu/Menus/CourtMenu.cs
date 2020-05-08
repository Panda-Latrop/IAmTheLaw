using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CourtMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerSimple accept0, refuse0, accept1, refuse1;
    [SerializeField]
    protected CourtMenuView view;
    [SerializeField]
    protected CourtController courtController;

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public void OnAccept(PointerEventData _eventData)
    {
        courtController.PlayerTurn(true);
    }
    public void OnRefuse(PointerEventData _eventData)
    {
        courtController.PlayerTurn(false);
    }
    public void ShowButton(Side _side)
    {
        view.Show(_side);
    }
    public void HideButton()
    {
        view.Hide();
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        accept0.AddHandler(OnAccept);
        refuse0.AddHandler(OnRefuse);
        accept1.AddHandler(OnAccept);
        refuse1.AddHandler(OnRefuse);
    }
}
