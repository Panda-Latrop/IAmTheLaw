using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentMenuView : MonoBehaviour
{
    [SerializeField]
    protected Sprite back, page;
    [SerializeField]
    protected SpriteRenderer spriteLeft, spriteRight, spriteMoney;
    [SerializeField]
    protected TMPro.TMP_Text textLeft, textRight;
    [SerializeField]
    protected Image bride, next, prev;
    [SerializeField]
    protected GameObject background;
    protected bool moneyDisplay, endMiss;
    protected int currentPage, maxPages;
    protected string number;
    protected string[] pages;

    public int CurrentPage => currentPage;
    public int MaxPage => maxPages;
    public bool IsStart { get { return currentPage < 0; } }
    public bool IsEnd { get { return currentPage >= maxPages; } }

    public void SetText(ScriptableCase _case)
    {
        moneyDisplay = _case.BribeIsAllow;
        maxPages = XMLLoader.LoadDocument(_case.DocumentText, ref number, ref pages);
        if (maxPages % 2 == 0)
        {
            endMiss = false;
        }
        else
        {
            maxPages += 1;
            endMiss = true;
        }
        SetPage(0);
         currentPage = 0;
    }
    public void NextPage()
    {
        if (OpenPage(currentPage + 2))
            currentPage += 2;
    }
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
            if (_page < 0)
                currentPage = 0;
            if (_page > maxPages)
                currentPage = maxPages;
        }
    }
    protected bool OpenPage(int _page)
    {
        if (_page < 0 || _page > maxPages)
            return false;

        if (_page == 0)
        {
            spriteLeft.sprite = back;
            spriteRight.sprite = page;
            spriteMoney.enabled = false;
            textLeft.enabled = false;
            textRight.SetText(pages[_page]);
            textRight.enabled = true;
            prev.enabled = prev.raycastTarget = false;
            next.enabled = next.raycastTarget = true;
            return true;
        }
        if (_page > 1 && _page < maxPages)
        {
            spriteLeft.sprite = page;
            spriteRight.sprite = page;
            spriteMoney.enabled = false;
            textLeft.SetText(pages[_page - 1]);
            textLeft.enabled = true;
            textRight.SetText(pages[_page]);
            textRight.enabled = true;
            prev.enabled = prev.raycastTarget = true;
            next.enabled = next.raycastTarget = true;
            return true;
        }
        if (_page >= maxPages - 1)
        {
            spriteLeft.sprite = page;
            spriteRight.sprite = back;
            if (!endMiss)
            {
                textLeft.SetText(pages[_page - 1]);
                textLeft.enabled = true;
            }
            else
                textLeft.enabled = false;
            textRight.enabled = false;
            prev.enabled = prev.raycastTarget = true;
            next.enabled = next.raycastTarget = false;
            return true;
        }
        return false;
    }
    public void ShowBride()
    {
        bride.enabled = bride.raycastTarget = spriteMoney.enabled = true;
    }
    public void HideBride()
    {
        bride.enabled = bride.raycastTarget = spriteMoney.enabled = false;
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
