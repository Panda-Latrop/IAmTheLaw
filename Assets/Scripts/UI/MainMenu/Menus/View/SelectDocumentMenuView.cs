using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDocumentMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text[] texts;
    [SerializeField]
    protected Image[] images;
    [SerializeField]
    protected Transform[] transforms;
    [SerializeField]
    protected Vector2 position, offset;
    [SerializeField]
    protected SpriteRenderer[] renderers;
    [SerializeField]
    protected TMPro.TMP_Text[] renderTexts;
    [SerializeField]
    protected GameObject background;
    protected bool showed = false;

    public void Show(ScriptableDay _day)
    {
            string number = "";

            for (int i = 0; i < 3 && i < _day.CaseCount; i++)
            {
                renderTexts[i].enabled = texts[i].enabled = images[i].enabled = images[i].raycastTarget = renderers[i].enabled = true;
                number = _day.GetCase(i).Number;
                texts[i].SetText("Case " + number);
                renderTexts[i].SetText(number);
                transforms[i].localPosition = position + (-1 + i) * offset; ;
            }
            for (int i = _day.CaseCount; i < 3; i++)
            {
                renderTexts[i].enabled = texts[i].enabled = images[i].enabled = images[i].raycastTarget = renderers[i].enabled = false;
            }

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
