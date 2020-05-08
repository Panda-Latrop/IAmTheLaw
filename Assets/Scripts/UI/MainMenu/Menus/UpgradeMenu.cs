using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeMenu : MenuElement
{
    protected ResourcesSetting resources;
    [SerializeField]
    protected UIClickListenerSimple backB, buyB;
    [SerializeField]
    protected int back;
    [SerializeField]
    protected UpgradeMenuView view;

    public override void Show()
    {
        base.Show();
        ShowHouse();
        view.ShowBack();
        view.SetResources(resources.Money, resources.Heresy);
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }
    protected void ShowHouse()
    {
        if (resources.HouseInfo + 1 >= resources.HouseCount)
        {
            ScriptableHouse house = resources.GetHouse();
            view.ShowHouse(house, false, true);
        }
        else
        {
            ScriptableHouse house = resources.GetHouse(resources.HouseInfo + 1);
            view.ShowHouse(house, house.Cost > resources.Money,false);
        }


    }
    protected void Buy(PointerEventData _eventData)
    {
        resources.HouseInfo++;       
        resources.Money -= resources.GetHouse().Cost;
        resources.Heresy += resources.GetHouse().Heresy;
        view.SetResources(resources.Money, resources.Heresy);
        ShowHouse();
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
    }
}
