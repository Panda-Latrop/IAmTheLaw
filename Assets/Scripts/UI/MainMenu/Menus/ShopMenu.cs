using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopMenu : MenuElement
{
    protected ResourcesSetting resources;
    [SerializeField]
    protected UIClickListenerSimple backB, buyB, nextB, prevB;
    [SerializeField]
    protected int back;
    [SerializeField]
    protected ShopMenuView view;
    protected int current;

    public override void Show()
    {
        base.Show();
        ChangeStuff();
        view.ShowBack();
        view.SetResources(resources.Money, resources.Heresy);
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }
    protected void Next(PointerEventData _eventData)
    {
        current++;
        ChangeStuff();
    }
    protected void Previous(PointerEventData _eventData)
    {
        current--;
        ChangeStuff();
    }

    protected void ChangeStuff()
    {

        int srtEnd = 0;
        int level = resources.GetStuffInfo(current).level;
        bool sold = resources.GetStuffInfo(current).sold;
        ScriptableStuff stuffs = resources.GetStuff(current);
        if (current + 1 >= resources.StuffCount)
            srtEnd = 1;
        if (current - 1 < 0)
            srtEnd = -1;

        if (sold)
            level--;
        view.ShowStuff(stuffs.Stuff(level), sold, resources.Money, srtEnd);
    }
    protected void Buy(PointerEventData _eventData)
    {
        int level = resources.GetStuffInfo(current).level;
        Stuff stuff = resources.GetStuff(current).Stuff(level);
        resources.SetStuffInfo(current, new StuffInfo(level + 1, level + 1 > resources.HouseInfo));
        resources.Money -= stuff.cost;
        resources.Heresy += stuff.heresy;
        view.SetResources(resources.Money, resources.Heresy);
        ChangeStuff();
    }
    protected void Back(PointerEventData _eventData)
    {
        controller.ShowMenu(back);
    }
    public override void OnStart()
    {
        resources = ResourcesSetting.Instance;

        backB.AddHandler(Back);
        buyB.AddHandler(Buy);
        nextB.AddHandler(Next);
        prevB.AddHandler(Previous);
    }
}

