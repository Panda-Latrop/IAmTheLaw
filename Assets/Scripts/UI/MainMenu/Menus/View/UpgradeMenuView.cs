using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text room, money, heresy, heresyPlus, buyT;
    [SerializeField]
    protected Image buy, heresyPlusI;
    [SerializeField]
    protected SpriteRenderer roomR;
    [SerializeField]
    protected GameObject roomBack;
    [SerializeField]
    protected Color normal, cant;

    public void SetResources(int _money, int _heresy)
    {
        money.SetText(_money.ToString());
        heresy.SetText(_heresy.ToString());
    }

    public void ShowHouse(ScriptableHouse _house, bool _cant, bool _final)
    {
        room.text = _house.House;
        roomR.sprite = _house.Sprite;
        if(!_final)
        {
            heresyPlusI.enabled = true;
            heresyPlus.text = "+" + _house.Heresy.ToString();
            if (!_cant)
            {
                buy.raycastTarget = true;
                buy.color = buyT.color = normal;
                buyT.SetText("Купить\n" + _house.Cost.ToString());
            }
            else
            {
                buy.raycastTarget = false;
                buy.color = buyT.color = cant;
                buyT.SetText("Стоит\n" + _house.Cost.ToString());
            }
        }
        else
        {
            buy.enabled = heresyPlusI.enabled = false;
            buyT.SetText(" ");
            heresyPlus.SetText(" ");
        }
    }
    public void FinalBuy()
    {
        buy.enabled = heresyPlusI.enabled = false;
        buyT.SetText(" ");
        heresyPlus.SetText(" "); 
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
