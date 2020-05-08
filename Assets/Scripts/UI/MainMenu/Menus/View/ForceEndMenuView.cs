using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceEndMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TextMeshPro text;
    [SerializeField]
    protected Image accept,refuse;
    [SerializeField]
    protected GameObject background;

    public void Hide()
    {
        accept.enabled = refuse.enabled = false;
        accept.raycastTarget = refuse.raycastTarget = false;
    }

    public void Show(bool _side)
    {
        accept.enabled = refuse.enabled = true;
        accept.raycastTarget = refuse.raycastTarget = true;
        text.SetText("Sentence in favor of\n" + (_side ? "Prosecutor" : "Lawyer"));
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
