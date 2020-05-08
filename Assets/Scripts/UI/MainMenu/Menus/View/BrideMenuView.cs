using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrideMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text text;
   
    [SerializeField]
    protected GameObject background;

    public void SetText(ScriptableCase _case, ResourcesSetting _resources)
    {
        string str = "";
        XMLLoader.LoadBribe(_case.DocumentText, ref str, _case.Bribe.toJail, _resources.Heresy > 0 ?_resources.Heresy* 100 : 100);
        text.SetText(str);
    }
    public void HideBack()
    {
        background.SetActive(false);
    }
    public void ShowBack()
    {
        background.SetActive(true);
    }
}
