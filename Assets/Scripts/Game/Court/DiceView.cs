using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DiceStruct
{
    public Sprite[] sides;
}
public class DiceView : MonoBehaviour
{
    [SerializeField]
    protected DiceStruct diceStruct;
    [SerializeField]
    protected SpriteRenderer[] dices;
    [SerializeField]
    protected TMPro.TMP_Text[] addTexts;
    [SerializeField]
    protected SpriteRenderer[] addImages;

    public void SetDice(int _id, int _value)
    {
        dices[_id].enabled = true;
        dices[_id].sprite = diceStruct.sides[_value];
    }
    public void HideDice(int _id)
    {
        dices[_id].enabled = false;
        for (int i = 0; i < addTexts.Length; i++)
        {
            addTexts[i].enabled = false;
            addImages[i].enabled = false;
        }
    }
    public void UnhideDice(int _id)
    {
        for (int i = 0; i < _id; i++)
        {
            dices[i].enabled = false;
        }
        dices[_id].enabled = true;
        for (int i = _id + 1; i < dices.Length; i++)
        {
            dices[i].enabled = false;
        }
    }
    public void HideAllDice()
    {
        dices[0].enabled = false;
        dices[1].enabled = false;
        dices[2].enabled = false;
        for (int i = 0; i < 3; i++)
        {
            addTexts[i].enabled = false;
            addImages[i].enabled = false;
        }
    }
    public void ShowPlus(int _add)
    {
        addTexts[2].enabled = true;
        addImages[2].enabled = true;
        addTexts[2].SetText("+" + _add + "\nPower");
    }
    public void ShowFail(int _side, int _add)
    {
        addTexts[_side].enabled = true;
        addImages[_side].enabled = true;
        addTexts[_side].SetText("-" + _add + "\nFail");
    }
}
