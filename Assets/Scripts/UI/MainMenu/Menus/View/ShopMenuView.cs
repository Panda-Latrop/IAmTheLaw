using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text stuff,money, heresy, heresyPlus, buyT;
    [SerializeField]
    protected Image next, prev, buy,heresyPlusI;
    [SerializeField]
    protected SpriteRenderer stuffR;
    [SerializeField]
    protected GameObject suffBack;
    [SerializeField]
    protected Color normal, sold, cant;

    public void SetResources(int _money, int _heresy)
    {
        money.SetText(_money.ToString());
        heresy.SetText(_heresy.ToString());
    }
    
    public void ShowStuff(Stuff _stuff, bool _sold, int _money , int _info)
    {
        stuff.text = _stuff.name;
        stuffR.sprite = _stuff.sprite;
        if (_sold)
        {
            heresyPlusI.enabled = buy.raycastTarget = false;
            heresyPlus.text = " ";
            buy.color = buyT.color = sold;
            buyT.SetText("Продано\n");
        }
        else
        {
            heresyPlusI.enabled = true;
            heresyPlus.text = "+" + _stuff.heresy.ToString();
            if (_stuff.cost <= _money)
            {
                buy.raycastTarget = true;
                buy.color = buyT.color = normal;
                buyT.SetText("Купить\n" + _stuff.cost.ToString());
            }
            else
            {
                buy.raycastTarget = false;
                buy.color = buyT.color = cant;
                buyT.SetText("Стоит\n" + _stuff.cost.ToString());
            }  
        }
        switch (_info)
        {
            case -1:
                next.raycastTarget = next.enabled = true;
                prev.raycastTarget = prev.enabled = false;
                break;
            case 0:
                next.raycastTarget = next.enabled = true;
                prev.raycastTarget = prev.enabled = true;
                break;
            case 1:
                next.raycastTarget = next.enabled = false;
                prev.raycastTarget = prev.enabled = true;
                break;
            default:
                break;
        }
    }
    public void Sold()
    {
        heresyPlusI.enabled = buy.raycastTarget = false;
        heresyPlus.text = " ";
        buy.color = buyT.color = sold;
        buyT.SetText("Продано\n");
    }
    public void HideBack()
    {
        suffBack.SetActive(false);
    }
    public void ShowBack()
    {
        suffBack.SetActive(true);
    }
}
