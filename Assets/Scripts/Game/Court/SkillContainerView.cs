using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BorderStruct
{
    public Color color;
    public SkillView prefab;
    public SpriteRenderer spriteBorder,acceptButton,refuseButton;
    
    public void OnGizmos(Vector2 _pos)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_pos, spriteBorder.size);
    }
}
public class SkillContainerView : MonoBehaviour
{
    [SerializeField]
    protected BorderStruct border;
    [SerializeField]
    protected List<SkillView> skills = new List<SkillView>();

    public void SetText(TextAsset _text,List<SkillStruct> _skills, string _side)
    {
        string[] skillTexts = new string[_skills.Count];
        XMLLoader.LoadSkills(_text, _side, ref skillTexts);
        //GenerateLines(skillTexts.Length);  
        for (int i = 0; i < _skills.Count; i++)
        {
            skills[i].SetText(skillTexts[i], _skills[i].power);
        }
    }
    public void SetColorToLine(int _id, Color _color)
    {
        skills[_id].SetColor(_color);
    }
    public void SetDefaultColor()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].SetColor(border.color);
        }      
    }
    public int GetLineCount()
    {
        return skills.Count;
    }
    public void GenerateLines(int _count)
    {
        //Debug.Log(_count.ToString());
        ClearLines();
        Vector2 size = border.spriteBorder.size;
        size.y /= (float)_count;
        Vector2 start = Vector2.zero;
        start.y = border.spriteBorder.size.y / 2.0f - size.y / 2.0f;
        Vector2 step = Vector2.zero;
        step.y = -size.y;
        //Debug.Log("si " + size + " sta " + start + " ste " + step);
        for (int i = 0; i < _count; i++)
        {
            skills.Add(Instantiate(border.prefab));
            skills[i].transform.SetParent(transform);
            skills[i].transform.localPosition = start + step * i;
            skills[i].SetSize(size);
            skills[i].SetColor(border.color);
        }
    }
   
    public void ClearLines()
    {
        skills.Clear();
        for (int i = 0; transform.childCount > 0; i++)
        {   
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    protected void OnDrawGizmosSelected()
    {
        border.OnGizmos(transform.position);
    }
}
