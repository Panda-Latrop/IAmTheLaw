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
    protected TMPro.TMP_Text heresyPlus;
    [SerializeField]
    protected SpriteRenderer heresy;

    public void SetDice(int _id,int _value)
    {
        dices[_id].enabled = true;
        dices[_id].sprite = diceStruct.sides[_value];
    }
    public void HideDice(int _id)
    {
        dices[_id].enabled = false;
        heresyPlus.enabled = false;
        heresy.enabled = false;
    }
    public void UnhideDice(int _id)
    {
        dices[_id].enabled = true;
    }
    public void HideAllDice()
    {
        dices[0].enabled = false;
        dices[1].enabled = false;
        dices[2].enabled = false;
        heresyPlus.enabled = false;
        heresy.enabled = false;
    }
    public void ShowPlus(int _add)
    {
        heresyPlus.enabled = true;
        heresy.enabled = true;
        heresyPlus.SetText("+" + _add);
    }
}
