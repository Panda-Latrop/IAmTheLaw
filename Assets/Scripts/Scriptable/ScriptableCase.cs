using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ResultGameStruct
{
    public int winMoney, winLoyalty, winHeresy;
    public int loseMoney, loseLoyalty, loseHeresy;
}

[System.Serializable]
public struct BribeStruct
{  
    public int size, heresy;
    public bool toJail;
}
[System.Serializable]
public struct SkillStruct
{
    public int power, cost;
}

[CreateAssetMenu(fileName = "New Case", menuName = "Case/Case", order = 52)] // 1
public class ScriptableCase : ScriptableObject
{
    [SerializeField]
    protected bool toJail, bribeIsAllow;
    [SerializeField]
    protected BribeStruct bribe;
    [SerializeField]
    protected TextAsset documentText, skillsText, dialogText;
    [SerializeField]
    protected ResultGameStruct result;
    [SerializeField]
    protected List<SkillStruct> leftSide = new List<SkillStruct>(), rightSide = new List<SkillStruct>();

    public bool ToJail => toJail;
    public bool BribeIsAllow => bribeIsAllow;
    public BribeStruct Bribe => bribe;
    public ResultGameStruct Result => result;

    public string Number => XMLLoader.FindNumber(documentText);

    public TextAsset DocumentText => documentText;
    public TextAsset SkillsText => skillsText;
    public TextAsset DialogText => dialogText;

    public List<SkillStruct> GetSkills(int _id)
    {
        if (_id == 0)
            return leftSide;
        else
            return rightSide;
    }
}
