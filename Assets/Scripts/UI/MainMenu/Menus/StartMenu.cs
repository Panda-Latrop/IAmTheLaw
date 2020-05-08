using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MenuElement
{
    protected ResourcesSetting resources;
    [SerializeField]
    protected UIClickListenerSimple startB, upgradeB, shopB,cheat, cheat2, cheat3;
    [SerializeField]
    protected StartMenuView view;
    [SerializeField]
    protected int upgrade, shop;

    public override void Show()
    {
        base.Show();
        view.SetResources(resources.Money, resources.Loyalty, resources.Heresy);
        view.ShowBack();
        view.ShowRoom(resources);
    }
    public override void Hide()
    {
        base.Hide();
        view.HideBack();
    }

    protected void ToGame(PointerEventData _eventData)
    {
        SceneLoader.Load(SceneLoader.DOCUMENT);
    }
    protected void ToUpgrade(PointerEventData _eventData)
    {
        controller.ShowMenu(upgrade);
    }
    protected void ToShop(PointerEventData _eventData)
    {
        controller.ShowMenu(shop);
    }
    protected void Cheat(PointerEventData _eventData)
    {
        resources.Money += 1000;
        view.SetResources(resources.Money, resources.Loyalty, resources.Heresy);
    }
    protected void Cheat2(PointerEventData _eventData)
    {
        resources.Loyalty += 50;
        view.SetResources(resources.Money, resources.Loyalty, resources.Heresy);
    }
    protected void Cheat3(PointerEventData _eventData)
    {
        resources.Heresy += 50;
        view.SetResources(resources.Money, resources.Loyalty, resources.Heresy);
    }
    public override void OnStart()
    {
        resources = ResourcesSetting.Instance;
        startB.AddHandler(ToGame);
        upgradeB.AddHandler(ToUpgrade);
        shopB.AddHandler(ToShop);
        cheat.AddHandler(Cheat);
        cheat2.AddHandler(Cheat2);
        cheat3.AddHandler(Cheat3);
    }
}
