using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text[] texts;
    [SerializeField]
    protected GameObject background;

    public void Show(TextAsset _nw, ResourcesSetting _resources)
    {
        int info = 1;
        if (_resources.Loyalty > 50)
            info = 0;
        if (_resources.Loyalty < -50)
            info = 2;
            string[] strs = XMLLoader.LoadNewspaper(_nw, info);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].SetText(strs[i]);
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
