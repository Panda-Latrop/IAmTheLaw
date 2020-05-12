using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransactionState
{
    none,
    toStart,  
    toEnd,
    toAccept,
    toRefuse,
    toChoice,
    toAction,
    toRestart,
    toRound,
}
public enum CourtState
{
    input,
    wait,
}

public class CourtControllerV2 : MonoBehaviour
{
    protected ResourcesSetting resources;
    protected GameSetting setting;

    [SerializeField]
    protected DialogController dialog;
    [SerializeField]
    protected MainMenuController menuController;
    [SerializeField]
    protected CourtMenu courtMenu;
    [SerializeField]
    protected ForceEndMenu forceEndMenu;

    protected CourtState cState;
    protected TransactionState tran;
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
    protected float waitOnAccept = 1f, waitOnRefuse = 1f, newCaseWaitTime = 2f, endCaseWaitTime, waitToForce;
    protected float nextTime;
    [SerializeField]
    protected int maxTurn = 12, forceTurn = 6;
    protected int curTurn = 0;
    protected bool autoDoor;


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

    public void NewGame()
    {
        dialog.Load(setting);
        for (int j = 0; j < setting.Case.GetSkills(0).Count; j++)
            leftSide[j].SetUp(setting.Case.GetSkills(0)[j]);
        for (int j = 0; j < setting.Case.GetSkills(1).Count; j++)
            rightSide[j].SetUp(setting.Case.GetSkills(1)[j]);
        GetSkillsView(0).SetText(setting.Case.SkillsText, setting.Case.GetSkills(0), "left");
        GetSkillsView(1).SetText(setting.Case.SkillsText, setting.Case.GetSkills(0), "right");
        GetSkillsView(0).SetDefaultColor();
        GetSkillsView(1).SetDefaultColor();
        diceView.HideAllDice();
        courtMenu.HideButton();
        scoreView.SetScore(0, 0);
        scoreView.SetScore(0, 1);
        power[0] = power[1] = 0;
        winView.Hide();
        curTurn = 0;

        if (!autoDoor)
            State(CourtState.input, TransactionState.toStart);
        else
            State(CourtState.wait, TransactionState.toStart);

    }

    public void NPCAction()
    {
        currentSkill = Random.Range(0, 6);

        diceView.HideDice(2);
        diceView.HideDice((int)(side == Side.side0 ? Side.side1 : Side.side0));
        GetSkillsView((int)(side)).SetDefaultColor();
        diceView.SetDice((int)side, currentSkill);
        GetSkillsView((int)side).SetColorToLine(currentSkill, Color.white);

        dialog.StartDialog((int)side, currentSkill);

        courtMenu.ShowButton();
        State(CourtState.input);
    }

    public void PlayerAccept()
    {          
        dialog.StopDialog();
        courtMenu.HideButton();
        dialog.StartDialog(DialogState.toAccept, (int)side, currentSkill, false);
        State(CourtState.wait,TransactionState.toAccept);
    }
    public void OnPlayerAccept()
    {
        power[(int)side] += GetSkills((int)side)[currentSkill].Power;
        GetSkillsView((int)side).SetColorToLine(currentSkill, Color.green);
        scoreView.SetScore(power[(int)side], (int)side);

        //nextTime = Time.time + waitOnAccept;
        State(CourtState.input, TransactionState.toRound);
    }
    public void PlayerRefuse()
    {
       
        dialog.StopDialog();
        courtMenu.HideButton();
    
        dialog.StartDialog(DialogState.toRefuse, (int)side, currentSkill, false);


       // nextTime = Time.time + waitOnRefuse;
        State(CourtState.input, TransactionState.toRefuse);
    }
    public void OnPlayerRefuse()
    {
        bool result;
        int rand = Random.Range(1, 7);
        int add = resources.Heresy / 100;
        Skill skill = GetSkills((int)side)[currentSkill];

        if (add > 0 && rand !=  6)
            diceView.ShowPlus(add);        
        else
            add = 0;

        result = (rand + add > skill.Cost) || (rand == 6 && skill.Cost == 6);
        if (!result)
        {
            power[(int)side] += skill.Power;

            if (side == Side.side0)
            {
                diceView.ShowFail(1, 10);
                setting.GameCase.FailHeresy -= Global.FAIL_HERESY;
            }

            else
            {
                diceView.ShowFail(0, 10);
                setting.GameCase.FailLoyalty -= Global.FAIL_LOYALTY;
            }

        }

           

        dialog.StopDialog();
        courtMenu.HideButton();

        diceView.SetDice(2, rand - 1);
        GetSkillsView((int)side).SetColorToLine(currentSkill, result ? Color.black : Color.red);
        scoreView.SetScore(power[(int)side], (int)side);

        dialog.StartDialog(result ? DialogState.toSuccses: DialogState.toFailed, (int)side, currentSkill, false);


        //nextTime = Time.time + waitOnRefuse;
        State(CourtState.input, TransactionState.toRound);
    }
    public void RoundEnd()
    {       
        curTurn++;
        side = side == Side.side0 ? Side.side1 : Side.side0;

        if (curTurn >= maxTurn)
        {
            EndGame();
        }
        else
        {
            if (curTurn == forceTurn)
            {
                State(CourtState.wait, TransactionState.toChoice);
            }
            else
            {
                State(CourtState.wait, TransactionState.toAction);
            }
        }

       

    }

    public void ForceAction(bool _forceEnd)
    {
        menuController.ShowMenu(0);
        dialog.StopDialog();
        winView.Hide();
        diceView.HideAllDice();
        courtMenu.HideButton();
        if (_forceEnd)
        {
            EndGame();
        }          
        else
        {
            nextTime = Time.time + waitOnAccept;
            State(CourtState.wait, TransactionState.toAction);
        }
           
    }
    public void EndGame()
    {
        dialog.StopDialog();
        setting.ToJail = (power[0] > power[1]);
        winView.ToJail(setting.ToJail);
        doorView.Close();
        nextTime = Time.time + endCaseWaitTime;
        State(CourtState.wait, TransactionState.toEnd);
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

    public void State(CourtState _state, TransactionState _transaction = TransactionState.none)
    {
        cState = _state;
        tran = _transaction;
        if (_transaction == TransactionState.none)
            enabled = false;
        else
            enabled = true;
    }

    public void TransactionAction()
    {
        switch (tran)
        {
            case TransactionState.toStart:
                doorView.Open();
                autoDoor = true;
                nextTime = Time.time + newCaseWaitTime;
                State(CourtState.wait, TransactionState.toAction);     
                break;
            case TransactionState.toEnd:
                if (setting.NextCase())
                    NewGame();
                else
                    SceneLoader.Load(SceneLoader.RESULT);
                break;
            case TransactionState.toAccept:
                OnPlayerAccept();
                break;
            case TransactionState.toRefuse:
                OnPlayerRefuse();
                break;
            case TransactionState.toChoice:
                menuController.ShowMenu(1);
                forceEndMenu.ShowDialog(power[0], power[1]);
                State(CourtState.input);
                break;
            case TransactionState.toAction:
                NPCAction();
                break;
            case TransactionState.toRestart:
                break;
            case TransactionState.toRound:
                RoundEnd();
                break;            
            default:
                break;
        }
    }


    public void Update()
    {
        switch (cState)
        {
            case CourtState.input:
                if (Input.anyKeyDown)
                    TransactionAction();
                break;
            case CourtState.wait:
                if (Time.time >= nextTime)
                    TransactionAction();
                break;
            default:
                break;
        }
    }


    //protected void OnDestroy()
    //{
    //    // OnRoundEnd = null;
    //}
}