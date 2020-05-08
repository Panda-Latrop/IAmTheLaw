using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DocumentStruct 
{
    public Transform root,bribe;
    public Sprite front, back, page;
    public SpriteRenderer spriteFront, spriteLeft, spriteRight, spriteMoney;
    public TMPro.TMP_Text textFront, textLeft, textRight;
}
public class DocumentView : MonoBehaviour
{
    [SerializeField]
    protected DocumentStruct document;
    protected bool moneyDisplay,endMiss;
    protected int currentPage, maxPages;
    protected string number;
    protected string[] pages;

    public int CurrentPage => currentPage;
    public int MaxPage => maxPages;
    public bool IsStart { get { return currentPage < 0; } }
    public bool IsEnd { get { return currentPage >= maxPages - 1; }}
    public void SetText(TextAsset _text,bool _brideAllow)
    {
        moneyDisplay = _brideAllow;
        maxPages = XMLLoader.LoadDocument(_text, ref number,ref pages);
        if (maxPages % 2 == 0)
        {
            endMiss = false;
        }
        else
        {
            maxPages += 1;
            endMiss = true;
        }
        document.root.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-5.0f, 5.0f));
        document.bribe.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-60.0f, 60.0f));
        document.spriteFront.sprite = document.front;
        document.spriteFront.enabled = true;
        document.spriteLeft.enabled = false;
        document.spriteRight.enabled = false;
        document.spriteMoney.enabled = false;
        document.textFront.enabled = true;
        document.textLeft.enabled = false;
        document.textRight.enabled = false;
        document.textFront.SetText(number);
        currentPage = -2;
    }
    [ContextMenu("Next")]
    public void NextPage()
    {
        if (OpenPage(currentPage + 2))
            currentPage += 2;
    }
    [ContextMenu("Previous")]
    public void PreviousPage()
    {
        if (OpenPage(currentPage - 2))
            currentPage -= 2;
    }
    public void SetPage(int _page)
    {
        if (OpenPage(_page))
        {
            currentPage = _page;
        }
        else
        {
            if (_page < -2)
                currentPage = -2;
            if (_page > maxPages)
                currentPage = maxPages;
        }
    }
    protected bool OpenPage(int _page)
    {
        if (_page < -2 || _page > maxPages)
            return false;
        if (_page == -2)
        {
            document.spriteFront.enabled = true;
            document.spriteLeft.enabled = false;
            document.spriteRight.enabled = false;
            document.spriteMoney.enabled = false;
            document.textFront.enabled = true;
            document.textLeft.enabled = false;
            document.textRight.enabled = false;
            return true;
        }
        if (_page == 0)
        {
            document.spriteFront.enabled = false;
            document.spriteLeft.sprite = document.back;
            document.spriteLeft.enabled = true;
            document.spriteRight.sprite = document.page;
            document.spriteRight.enabled = true;
            document.spriteMoney.enabled = false;
            document.textFront.enabled = false;
            document.textLeft.enabled = false;
            document.textRight.SetText(pages[_page]);
            document.textRight.enabled = true;
            return true;
        }
        if (_page > 1 && _page < maxPages)
        {
            document.spriteFront.enabled = false;
            document.spriteLeft.sprite = document.page;
            document.spriteLeft.enabled = true;
            document.spriteRight.sprite = document.page;
            document.spriteRight.enabled = true;
            document.spriteMoney.enabled = false;
            document.textFront.enabled = false;
            document.textLeft.SetText(pages[_page - 1]);
            document.textLeft.enabled = true;
            document.textRight.SetText(pages[_page]);
            document.textRight.enabled = true;
            return true;
        }
        if (_page >= maxPages - 1)
        {
            document.spriteFront.enabled = false;
            document.spriteLeft.sprite = document.page;
            document.spriteLeft.enabled = true;
            document.spriteRight.sprite = document.back;
            document.spriteRight.enabled = true;
            document.spriteMoney.enabled = moneyDisplay;
            document.textFront.enabled = false;
            if (!endMiss)
            {
                document.textLeft.SetText(pages[_page - 1]);
                document.textLeft.enabled = true;
            }
            else
                document.textLeft.enabled = false;
            document.textRight.enabled = false;
            return true;
        }
        return false;
    }
}
