using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForceEndMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerSimple accept, refuse;
    [SerializeField]
    protected ForceEndMenuView view;
    [SerializeField]
    protected CourtController courtController;

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
    public void ShowDialog(int _left, int _right)
    {
        view.Show(_left > _right);
    }
    public void OnAccept(PointerEventData _eventData)
    {
        courtController.ForceEnd();
    }
    public void OnRefuse(PointerEventData _eventData)
    {
        courtController.ForceResume();
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        accept.AddHandler(OnAccept);
        refuse.AddHandler(OnRefuse);
    }
}