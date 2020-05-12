using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MenuElement
{ 
    [SerializeField]
    protected UIClickListenerSimple continueB,newGameB, optionB, exitB;
    [SerializeField]
    protected MainMenuView view;
    [SerializeField]
    protected int toOption;

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
    protected void ToContinue(PointerEventData _eventData)
    {
        LoadSceneSetting.Instance.state = LoadSceneState.load;
        SceneLoader.Load(SceneLoader.HOME);
    }
    protected void ToNewGame(PointerEventData _eventData)
    {
        SceneLoader.Load(SceneLoader.HOME);
    }
    protected void ToOption(PointerEventData _eventData)
    {

    }
    protected void ToExit(PointerEventData _eventData)
    {
        Application.Quit();
    }
    public override void OnStart()
    {
        continueB.AddHandler(ToContinue);
        newGameB.AddHandler(ToNewGame);
        optionB.AddHandler(ToOption);
        exitB.AddHandler(ToExit);
        if (!SaveSystem.SaveExist())
            view.HideContinue();
    }
}
