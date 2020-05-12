using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LawbookArticle
{
    public string name;
    public string title, text;
}

public class LawbookMenu : MenuElement
{
    protected GameSetting setting;
    [SerializeField]
    protected UIClickListenerSimple backB, upB, downB;
    [SerializeField]
    protected UIClickListenerArg[] articlesB;
    [SerializeField]
    protected LawbookMenuView view;
    [SerializeField]
    protected int back;
    protected List<LawbookArticle> laws;
    protected int offset,arg;

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
        controller.ShowMenu(back);
    }
    protected void Up(PointerEventData _eventData)
    {
        int info = 1;
        offset--;
        if (offset <= 0)
        {
            offset = 0;
            info = 0;
        }
        arg++;
        if (arg >= 2)
        {
            arg = 2;
        }
        view.ChangeButtons(laws, offset, 3, info);
        ChangeArticle(null, arg);
    }
    protected void Down(PointerEventData _eventData)
    {
        int info = 1;
        offset++;
        if (offset >= laws.Count - 4)
        {
            offset = laws.Count - 3;
            info = 2;         
        }
        arg--;
        if (arg <= 0)
        {
            arg = 0;
        }
        view.ChangeButtons(laws, offset, 3, info);
        ChangeArticle(null, arg);
    }
    protected void ChangeArticle(PointerEventData _eventData,int _arg)
    {
        arg = _arg;
        view.ShowLow(laws[arg + offset], arg);
    }
    public override void OnStart()
    {
        setting = GameSetting.Instance;

        backB.AddHandler(ToBack);
        upB.AddHandler(Up);
        downB.AddHandler(Down);

        articlesB[0].AddHandler(ChangeArticle);
        articlesB[1].AddHandler(ChangeArticle);
        articlesB[2].AddHandler(ChangeArticle);
        XMLLoader.LoadLawbook(setting.Day.LawbookText, ref laws);
        offset = 0;
        Up(null);
        ChangeArticle(null, 0);
    }
}
