using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LawbookMenuView : MonoBehaviour
{

    [SerializeField]
    protected TMPro.TMP_Text title, mainText;
    [SerializeField]
    protected Image upB,downB;
    [SerializeField]
    protected Image[] imagesB;
    [SerializeField]
    protected TMPro.TMP_Text[] textsB;
    [SerializeField]
    protected GameObject roomBack;

    
    public void ShowLow(LawbookArticle _article, int _arg)
    {
        title.SetText(_article.title);
        mainText.SetText(_article.text);

        for (int i = 0; i < _arg; i++)
        {
            imagesB[i].color = Color.white;
        }
        imagesB[_arg].color = Color.green;
        for (int i = _arg + 1; i < imagesB.Length; i++)
        {
            imagesB[i].color = Color.white;
        }
    }
    public void ChangeButtons(List<LawbookArticle> _laws ,int _offset, int _count,int _info)
    {
        for (int i = 0; i < _count; i++)
        {
            textsB[i].enabled = imagesB[i].enabled = imagesB[i].raycastTarget = true;
            textsB[i].SetText(_laws[i + _offset].name);
        }
        for (int i = _count + 1; i < imagesB.Length; i++)
        {
            textsB[i].enabled = imagesB[i].enabled = imagesB[i].raycastTarget = false;

        }
        switch (_info)
        {
            case 0:
                upB.enabled = upB.raycastTarget = false;
                downB.enabled = downB.raycastTarget = true;
                break;
            case 1:
                upB.enabled = upB.raycastTarget = true;
                downB.enabled = downB.raycastTarget = true;
                break;
            case 2:
                upB.enabled = upB.raycastTarget = true;
                downB.enabled = downB.raycastTarget = false;
                break;
            default:
                break;
        }
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
