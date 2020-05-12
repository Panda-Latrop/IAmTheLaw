using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectDocumentMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerArg case0, case1, case2;
    [SerializeField]
    protected UIClickListenerSimple toGame,toNews, toLaws;
    [SerializeField]
    protected DocumentMenu documentMenu;
    [SerializeField]
    protected SelectDocumentMenuView view;
    [SerializeField]
    protected int show,news,laws;

    public override void Show()
    {
        base.Show();
        //view.Show(setting.Day);
        view.ShowBack();
        //view.ShowRoom(resources);
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }

    protected void ToGame(PointerEventData _eventData)
    {
        SceneLoader.Load(SceneLoader.COURT);
    }
    protected void ToShow(PointerEventData _eventData,int _case)
    {
        documentMenu.SetCase(setting.Day.GetCase(_case));
        documentMenu.SetGameCase(setting.GetGameCase(_case));
        controller.ShowMenu(show);
    }
    protected void ToNews(PointerEventData _eventData)
    {      
        controller.ShowMenu(news);
    }
    protected void ToLaws(PointerEventData _eventData)
    {
        controller.ShowMenu(laws);
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;
        toGame.AddHandler(ToGame);
        case0.AddHandler(ToShow);
        case1.AddHandler(ToShow);
        case2.AddHandler(ToShow);
        toNews.AddHandler(ToNews);
        toLaws.AddHandler(ToLaws);
        view.Show(setting.Day);
    }
}
