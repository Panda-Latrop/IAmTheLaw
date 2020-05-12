using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    protected Image contB;
    [SerializeField]
    protected TMPro.TMP_Text contT;
    [SerializeField]
    protected GameObject roomBack;

    public void HideContinue()
    {
        contB.enabled = contB.raycastTarget = contT.enabled = false;
    }

    public void HideBack()
    {
        roomBack.SetActive(false);
    }
    public void ShowBack()
    {
        roomBack.SetActive(true);
    }
}
