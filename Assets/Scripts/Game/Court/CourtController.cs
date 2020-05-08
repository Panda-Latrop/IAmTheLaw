using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtController : MonoBehaviour
{
    protected ResourcesSetting resources;
    protected GameSetting setting;
    [SerializeField]
    protected CourtMenu menu;
    [SerializeField]
    protected GameState state;
    [SerializeField]
    protected Side side;

    [SerializeField]
    protected SkillContainerView leftSideView, rightSideView;
    [SerializeField]
    protected DiceView diceView;
    [SerializeField]
    protected ScoreView scoreView;
    [SerializeField]
    protected DoorView doorView;
    [SerializeField]
    protected WinTextView winView;

    protected List<Skill> leftSide = new List<Skill>(), rightSide = new List<Skill>();
    protected int currentSkill;
    protected int[] power = new int[2];
    [SerializeField]
    protected float waitTime = 1f, waitTimeOnRefuse = 1f, newCaseWaitTime = 2f, endCaseWaitTime;
    protected float nextTime;
    [SerializeField]
    protected int maxTurn = 6;
    protected int curTurn = 0;
    protected bool autoDoor;

    //public event System.Action<int> OnRoundEnd;

    protected void Start()
    {
        setting = GameSetting.Instance;
        resources = ResourcesSetting.Instance;
        for (int j = 0; j < setting.Case.GetSkills(0).Count; j++)
            leftSide.Add(new Skill());
        for (int j = 0; j < setting.Case.GetSkills(1).Count; j++)
            rightSide.Add(new Skill());
        NewGame();
    }

    protected GameState State
    {
        get => state;
        set
        {
            state = value;
            switch (state)
            {

                case GameState.start:
                    enabled = true;
                    break;
                case GameState.startCase:
                    enabled = true;
                    nextTime = Time.time + newCaseWaitTime;
                    doorView.Open();
                    state = GameState.startCaseWait;
                    break;
                case GameState.endCase:
                    enabled = true;
                    nextTime = Time.time + endCaseWaitTime;
                    state = GameState.endCaseWait;
                    break;
                case GameState.input:
                    enabled = false;
                    break;
                case GameState.end:        
                    enabled = true;
                    doorView.Close();
                    nextTime = Time.time + newCaseWaitTime;
                    state = GameState.endWait;
                    break;
                case GameState.wait:
                    enabled = true;
                    nextTime = Time.time + waitTime;
                    break;
                case GameState.waitRefuse:
                    enabled = true;
                    nextTime = Time.time + waitTimeOnRefuse;
                    break;
                default:
                    break;
            }
        }
    }
    public void NewGame()
    {
        for (int j = 0; j < setting.Case.GetSkills(0).Count; j++)
            leftSide[j].SetUp(setting.Case.GetSkills(0)[j]);
        for (int j = 0; j < setting.Case.GetSkills(1).Count; j++)
            rightSide[j].SetUp(setting.Case.GetSkills(1)[j]);
        GetSkillsView(0).SetText(setting.Case.SkillsText, setting.Case.GetSkills(0), "left");
        GetSkillsView(1).SetText(setting.Case.SkillsText, setting.Case.GetSkills(0), "right");
        diceView.HideAllDice();
        menu.HideButton();     
        GetSkillsView(0).SetDefaultColor();
        GetSkillsView(1).SetDefaultColor();
        power[0] = power[1] = 0;
        scoreView.SetScore(0, 0);
        scoreView.SetScore(0, 1);
        winView.Hide();

        curTurn = 0;
        State = GameState.start;
    }

    [ContextMenu("SideTurn")]
    public void SideTurn()
    {
        diceView.HideDice(2);
        diceView.HideDice((int)(side == Side.side0 ? Side.side1 : Side.side0));
        GetSkillsView((int)(side)).SetDefaultColor();

        currentSkill = Random.Range(0, 6);
        diceView.SetDice((int)side, currentSkill);
        GetSkillsView((int)side).SetColorToLine(currentSkill, Color.white);
        menu.ShowButton(side);
        //sideViews[(int)side].ShowButtons(true);
        State = GameState.input;
    }
    public void PlayerTurn(bool _accept = true)
    {
        if (_accept)
        {
            GetSkillsView((int)side).SetColorToLine(currentSkill, Color.green);
            power[(int)side] += GetSkills((int)side)[currentSkill].Power;
        }
        else
        {
            int rand = Random.Range(1, 7);
            int add = resources.Heresy / 100;

            diceView.SetDice(2, rand - 1);
            bool result;
            if (add > 0 && rand != 6)
            {
                diceView.ShowPlus(add);
                result = (rand + add > GetSkills((int)side)[currentSkill].Cost);// || !(rand == 6 && GetSkills((int)side)[currentSkill].Cost == 6);
            }
            else
            {
                result = (rand  > GetSkills((int)side)[currentSkill].Cost);
            }
            
            GetSkillsView((int)side).SetColorToLine(currentSkill, result ? Color.black : Color.green);
            if (!result)
                power[(int)side] += GetSkills((int)side)[currentSkill].Power;
        }
        menu.HideButton();
        scoreView.SetScore(power[(int)side], (int)side);
        side = side == Side.side0 ? Side.side1 : Side.side0;
        curTurn++;
        if (curTurn >= maxTurn)
        {
            EndGame();
            return;
        }
        else
        {
            // Дилог в раунд
            //OnRoundEnd?.Invoke(curTurn);
            if (_accept)
                State = GameState.wait;
            else
                State = GameState.waitRefuse;
        }
            
    }

    public void EndGame()
    {
        State = GameState.endCase;
        setting.ToJail = (power[0] > power[1]);
        winView.ToJail(setting.ToJail);
        //if (setting.NextCase())
        //{
        //    NewGame();
        //}
        //else
        //{
        //    SceneLoader.Load(SceneLoader.RESULT);
        //}
    }
    protected List<Skill> GetSkills(int _id)
    {
        if (_id == 0)
            return leftSide;
        else
            return rightSide;
    }
    protected SkillContainerView GetSkillsView(int _id)
    {
        if (_id == 0)
            return leftSideView;
        else
            return rightSideView;
    }
    public void Update()
    {
        switch (state)
        {
            case GameState.start:
                if (Input.anyKey || autoDoor)
                {
                    autoDoor = true;
                    State = GameState.startCase;
                }
                              
                return;
            case GameState.startCaseWait:
                if (Time.time >= nextTime)
                    SideTurn();
                return;           
            case GameState.endCaseWait:
                if (Time.time >= nextTime)
                    State = GameState.end;
                return;
            case GameState.endWait:
                if (Time.time >= nextTime)
                {
                    if (setting.NextCase())
                        NewGame();
                    else
                        SceneLoader.Load(SceneLoader.RESULT);
                }
                return;
            case GameState.waitRefuse:
            case GameState.wait:
                if (Time.time >= nextTime)
                    SideTurn();
                return;
            default:
                break;
        }
    }
    protected void OnDestroy()
    {
       // OnRoundEnd = null;
    }
}
public enum GameState
{
    start,
    startCase,
    startCaseWait,
    endCaseWait,
    endCase,
    endWait,
    wait,
    waitRefuse,
    input,
    end,

}
public enum Side
{
    side0 = 0,
    side1 = 1,
}
[System.Serializable]
public class Skill
{
    protected SkillStruct skillStruct;
    protected bool isBlock;


    public Skill()
    {
        isBlock = false;
    }

    public Skill(SkillStruct _skillStruct)
    {
        skillStruct = _skillStruct;
        isBlock = false;
    }

    public void SetUp(SkillStruct _skillStruct)
    {
        skillStruct = _skillStruct;
        isBlock = false;
    }

    public int Power => skillStruct.power;
    public int Cost => skillStruct.cost;
    public bool IsBlock { get => isBlock; set => isBlock = value; }
}