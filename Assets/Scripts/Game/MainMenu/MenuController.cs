using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    protected GameSetting setting;
    protected ResourcesSetting resources;
    [SerializeField]
    protected ResultView resultView;
    [SerializeField]
    protected UIClickListenerSimple left;

    protected void Start()
    {
        setting = GameSetting.Instance;
        resources = ResourcesSetting.Instance;
        left.AddHandler(StartNewCase);
        //resultView.SetCurrent(resources.Money, resources.Loyalty, resources.Heresy);       
    }

    public void StartNewCase(PointerEventData _eventData)
    {
        SceneLoader.Load(SceneLoader.DOCUMENT);
    }
}
