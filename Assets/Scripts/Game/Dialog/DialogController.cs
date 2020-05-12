using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class DialogRoot
{
    public List<DialogSkills> sides;
}
[System.Serializable]
public class DialogSkills
{
    public List<DialogSkill> skills;
}
[System.Serializable]
public class DialogSkill
{
    public List<DialogNode> dialogs;
    public List<DialogNode> actions;
}
[System.Serializable]
public class DialogNode
{
    public string text;
    public float delay;
    public int side;
}

public enum DialogState
{
    toDialog,
    toAccept,
    toRefuse,
    toSuccses,
    toFailed
}

public class DialogController : MonoBehaviour
{
    [SerializeField]
    protected DialogCloudView[] dialogViews;
    protected DialogState state;
    protected DialogRoot dialog;
    protected int side = 0,skill = 0, node = 0, opponent = 0;
    protected float nextTime;
    protected bool dialogEnd,wait;

    public void Load(GameSetting _setting)
    {       
        XMLLoader.LoadDialog(_setting.Case.DialogText, ref dialog);
        for (int i = 0; i < dialogViews.Length; i++)
            dialogViews[i].Hide(true);
        enabled = false;
    }
    public void StartDialog(DialogState _state, int _side, int _skill,bool _wait = false)
    {
        //Debug.Log(" " + _side + " " + _skill);
        state = _state;
        side = _side;
        skill = _skill;
        wait = _wait;
        node = 0;
        GetNextNode();
        if (!wait)
            enabled = true;
    }
    public void StartDialog(int _side,int _skill, bool _wait = false)
    {
        //Debug.Log(" " + _side + " " + _skill);
        state = DialogState.toDialog; ;
        side = _side;
        skill = _skill;
        wait = _wait;
        node = 0;
        GetNextNode();
        if(!wait)
        enabled = true;
    }
    protected void GetNode(DialogState _state,ref DialogNode _node, ref int _count)
    {
        switch (_state)
        {
            case DialogState.toDialog:
                _node = dialog.sides[side].skills[skill].dialogs[node];
                _count = dialog.sides[side].skills[skill].dialogs.Count;
                break;
            case DialogState.toAccept:
                _node = dialog.sides[side].skills[skill].actions[0];
                _count =-1;
                break;
            case DialogState.toRefuse:
                _node = dialog.sides[side].skills[skill].actions[1];
                _count = -1;
                break;
            case DialogState.toSuccses:
                _node = dialog.sides[side].skills[skill].actions[2];
                _count = -1;
                break;
            case DialogState.toFailed:
                _node = dialog.sides[side].skills[skill].actions[3];
                _count = -1;
                break;
            default:
                break;
        }
    }

    public bool GetNextNode()
    {

        DialogNode node = null;
        int count = 0;
        GetNode(state,ref node, ref count);
        if (opponent != node.side)
        {
            dialogViews[opponent].Hide(true);
            opponent = node.side;
        }
        dialogViews[opponent].Hide(false);
        dialogViews[opponent].SetText(node.text);
        this.node++;
        nextTime = Time.time + node.delay;
        return (this.node < count);

    }
    public void StopDialog()
    {
        side = skill = opponent = node = 0;
        for (int i = 0; i < dialogViews.Length; i++)
            dialogViews[i].Hide(true);
        dialogEnd = enabled = false;
    }
    //protected void Update()
    //{
    //    if (Time.time >= nextTime)
    //    {
    //        if (dialogEnd && !wait)
    //            StopDialog();
    //        else
    //            dialogEnd = !GetNextNode();
    //        //switch (state)
    //        //{
    //        //    case DialogState.toDialog:
    //        //        break;
    //        //    case DialogState.toAccept:
    //        //        break;
    //        //    case DialogState.toRefuse:
    //        //        break;
    //        //    case DialogState.toSuccses:
    //        //        break;
    //        //    case DialogState.toFailed:
    //        //        break;
    //        //    default:
    //        //        break;
    //        //}
          
    //    }
    //}
}
