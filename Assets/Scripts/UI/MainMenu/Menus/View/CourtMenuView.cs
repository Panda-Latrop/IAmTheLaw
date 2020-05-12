using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourtMenuView : MonoBehaviour
{
    [SerializeField]
    protected Image accept, refuse;
    [SerializeField]
    protected GameObject background;

    public void Hide()
    {
        accept.enabled = refuse.enabled = false;
        accept.raycastTarget = refuse.raycastTarget = false;
        
    }

    public void Show()
    {
        accept.enabled = refuse.enabled = true;
        accept.raycastTarget = refuse.raycastTarget = true;
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
