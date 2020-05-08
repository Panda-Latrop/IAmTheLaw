using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourtMenuView : MonoBehaviour
{
    [SerializeField]
    protected Image accept0, accept1, refuse0, refuse1;

    public void Hide()
    {
        accept0.enabled = accept1.enabled = refuse0.enabled = refuse1.enabled = false;
        accept0.raycastTarget = accept1.raycastTarget = refuse0.raycastTarget = refuse1.raycastTarget = false;
        
    }

    public void Show(Side _side)
    {
        if (_side == Side.side0)
        {       
            accept0.enabled = refuse0.enabled = true;
            accept0.raycastTarget = refuse0.raycastTarget = true;
            accept1.enabled = refuse1.enabled = false;
            accept1.raycastTarget = refuse1.raycastTarget = false;
        }
        else
        {
            accept0.enabled = refuse0.enabled = false;
            accept0.raycastTarget = refuse0.raycastTarget = false;
            accept1.enabled = refuse1.enabled = true;
            accept1.raycastTarget = refuse1.raycastTarget = true;
        }
    }
}
