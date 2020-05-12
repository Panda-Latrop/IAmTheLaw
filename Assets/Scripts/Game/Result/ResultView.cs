using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct ResultSruct
{
    public int winTime, loseTime, brideTime,failTime;
    public int winMoney, winLoyalty, winHeresy;
    public int loseMoney, loseLoyalty, loseHeresy;
    public int brideMoney, brideLoyalty, brideHeresy;
    public int totalMoney, totalLoyalty, totalHeresy;
    public int failLoyalty, failHeresy;
}
[System.Serializable]
public struct ResultLineStruct
{
    public TMPro.TMP_Text[] texts;
    public SpriteRenderer line;

    public void Show(string _action, int _money, int _loyalty, int _heresy)
    {
        line.enabled = true;
        texts[0].SetText(_action);
        texts[1].SetText((_money > 0 ? "+" : "") + _money.ToString());
        texts[1].color = _money >= 0 ? Color.green : Color.red;
        texts[2].SetText((_loyalty > 0 ? "+" : "") + _loyalty.ToString());
        texts[2].color = _loyalty >= 0 ? Color.green : Color.red;
        texts[3].SetText((_heresy > 0 ? "+" : "") + _heresy.ToString());
        texts[3].color = _heresy >= 0 ? Color.green : Color.red;
    }
    public void Show(string _action, int _money, int _loyalty, int _heresy, bool _no)
    {
        line.enabled = true;
        texts[0].SetText(_action);
        texts[1].SetText(_money.ToString());
        texts[1].color = _money >= 0 ? Color.green : Color.red;
        texts[2].SetText( _loyalty.ToString());
        texts[2].color = _loyalty >= 0 ? Color.green : Color.red;
        texts[3].SetText( _heresy.ToString());
        texts[3].color = _heresy >= 0 ? Color.green : Color.red;
    }
    public void Hide()
    {
        line.enabled = false;
        for (int i = 0; i < texts.Length; i++)
            texts[i].enabled = false;
    }
}

public class ResultView : MonoBehaviour
{
    [SerializeField]
    protected ResultLineStruct[] lines;
    protected string[] actions = { "Successful Cases", "Unsuccessful Cases", "Bribes", "Other:", "Amercement:", "Total:" };

    public void SetResult(ResultSruct _result)
    {
        int current = -1;
        if (_result.winTime > 0)
        {
            current++;
            lines[current].Show(actions[0] + " x" + _result.winTime.ToString() + ":", _result.winMoney, _result.winLoyalty, _result.winHeresy);
        }
        if (_result.loseTime > 0)
        {
            current++;
            lines[current].Show(actions[1] + " x" + _result.loseTime.ToString() + ":", _result.loseMoney, _result.loseLoyalty, _result.loseHeresy);
        }
        if (_result.brideTime > 0)
        {
            current++;
            lines[current].Show(actions[2] + " x" + _result.brideTime.ToString() + ":", _result.brideMoney, _result.brideLoyalty, _result.brideHeresy);
        }
        if (_result.failTime > 0)
        {
            current++;
            lines[current].Show(actions[4] + " x" + _result.failTime.ToString() + ":",0, _result.failLoyalty, _result.failHeresy);
        }
        current++;
        lines[current].Show(actions[5], _result.totalMoney, _result.totalLoyalty, _result.totalHeresy,true);
        for (int i = current + 1; i < lines.Length; i++)
        {
            lines[i].Hide();
        }
    }
}
