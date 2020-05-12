using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameCaseStruct
{

    [SerializeField]
    protected bool bribeAccepted, toJail;
    [SerializeField]
    protected int failLoyalty, failHeresy;

    public GameCaseStruct()
    {      
        bribeAccepted = toJail = false;
        failLoyalty = failHeresy = 0;
    }
   
    public bool BribeAccept { get => bribeAccepted; set => bribeAccepted = value; }
    public bool ToJail { get => toJail; set => toJail = value; }
    public int FailLoyalty { get => failLoyalty; set => failLoyalty = value; }
    public int FailHeresy { get => failHeresy; set => failHeresy = value; }
}

public class GameSetting : Singleton<GameSetting>
{
    [SerializeField]
    protected ScriptableDay day;
    [SerializeField]
    protected GameCaseStruct[] cases;
    protected int currentCase;

    public void SetDay(ScriptableDay _day)
    {
        day = _day;
        cases = new GameCaseStruct[_day.CaseCount];
        for (int i = 0; i < _day.CaseCount; i++)
        {
            cases[i] = new GameCaseStruct();
        }
    }
    public ScriptableCase Case => day.GetCase(currentCase);
    public GameCaseStruct GameCase => cases[currentCase];
    public GameCaseStruct GetGameCase(int _id) => cases[_id];
    public ScriptableDay Day => day;
    public bool BribeAccept { get => cases[currentCase].BribeAccept; set => cases[currentCase].BribeAccept = value; }
    public bool ToJail { get => cases[currentCase].ToJail; set => cases[currentCase].ToJail = value; }
    public bool NextCase()
    {
        if(currentCase + 1 < cases.Length)
        {
            currentCase++;
            return true;
        }
        currentCase = 0;
        return false;
    }

    public void RestartDay()
    {
        for (int i = 0; i < cases.Length; i++)
        {
            cases[i].BribeAccept = cases[i].ToJail = false;
        }
    }
    public int CaseCount => day.CaseCount;
}

