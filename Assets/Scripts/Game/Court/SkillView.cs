using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillViewStruct
{
    public Transform line;
    public SpriteRenderer spriteLine,spritePower;
    public TMPro.TMP_Text textSkill, textPower;
}

public class SkillView : MonoBehaviour
{
    [SerializeField]
    protected SkillViewStruct skill;

    public void SetSize(Vector2 _size)
    {
        skill.spriteLine.size = _size;
        Vector2 powSize = _size;
        powSize.x = powSize.y;
        skill.spritePower.size = powSize;
        Vector2 pos = Vector2.zero;
        pos.x = -_size.x / 2.0f + powSize.x / 2.0f;
        skill.spritePower.transform.localPosition = pos;
        skill.textPower.transform.localPosition = pos;
    }
    public void SetText(string _text, int _power)
    {
        skill.textSkill.SetText(_text);
        skill.textPower.SetText(_power.ToString());
    }
    public void SetColor(Color _color)
    {
        skill.spriteLine.color = skill.spritePower.color = _color;
    }
}