using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextView : MonoBehaviour
{
    [SerializeField]
   protected TMPro.TMP_Text text;
    [SerializeField]
    protected Color toJail, toFreedom;

    public void ToJail(bool _toJail)
    {
        text.enabled = true;
        if (_toJail)
        {
            text.SetText("To Jail");
            text.color = toJail;
        }
        else
        {
            text.SetText("To Freedom");
            text.color = toFreedom;
        }
    }
    public void Hide()
    {
        text.enabled = false;
    }
}
