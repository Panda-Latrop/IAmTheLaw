using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuView : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text overText;
    [SerializeField]
    protected GameObject background;
    [SerializeField]
    protected SpriteRenderer[] roomR;
    protected string[] overs = {"Without money,\nwithout home, without future","People hate you","Authorities got rid of you"};

    public void Show(ResourcesSetting resources)
    {
        if (resources.Money <= -100)
        {
            roomR[0].enabled = true;
            roomR[1].enabled = false;
            roomR[2].enabled = false;
            overText.SetText(overs[0]);
            return;
        }
        if (resources.Loyalty <= -100)
        {
            roomR[0].enabled = false;
            roomR[1].enabled = true;
            roomR[2].enabled = false;
            overText.SetText(overs[1]);
            return;
        }
        if (resources.Heresy <= -100)
        {
            roomR[0].enabled = false;
            roomR[1].enabled = false;
            roomR[2].enabled = true;
            overText.SetText(overs[2]);
            return;
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
