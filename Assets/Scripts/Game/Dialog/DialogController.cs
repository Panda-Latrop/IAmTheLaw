using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class DialogBranch
{
    public int round;
    public List<DialogNode> nodes;
    public DialogNode stopNode;
}
[System.Serializable]
public class DialogNode
{
    public string text;
    public float delay;
    public int side;
}


public class DialogController : MonoBehaviour
{
    protected GameSetting setting;
    [SerializeField]
    protected DialogCloudView[] dialogViews;
    protected List<DialogBranch> dialog;
    protected int currentBranch = 0, currentNode = 0, opponent = 0;
    protected float nextTime;
    protected bool dialogEnd;

    protected void Start()
    {

        setting = GameSetting.Instance;
        XMLLoader.LoadDialog(setting.Case.DialogText, ref dialog);
        for (int i = 0; i < dialogViews.Length; i++)
            dialogViews[i].Hide(true);
        enabled = false;
    }
    [ContextMenu("Start")]
    public void SYAR()
    {
        StopDialog();
        StartDialog(0);
    }
    public void StartDialog(int _id)
    {
        //enabled = true;
        //currentBranch = _id;
        //currentNode = 0;
        //GetNextNode();
        bool round = false;
        for (int i = 0; i < dialog.Count; i++)
        {
           
            if (dialog[i].round == _id)
            {
                currentBranch = i;
                round = true;
                break;
            }
            
        }
        Debug.Log("Call " + round);
        if (!round)
        {
            return;
        }
         
        enabled = true;
        GetNextNode();
    }
    public bool GetNextNode()
    {
        DialogNode node = dialog[currentBranch].nodes[currentNode];
        if (opponent != node.side)
        {
            dialogViews[opponent].Hide(true);
            opponent = node.side;
        }
        dialogViews[opponent].Hide(false);
        dialogViews[opponent].SetText(node.text);
        currentNode++;
        nextTime = Time.time + node.delay;
        return currentNode < dialog[currentBranch].nodes.Count;

    }
    public void StopDialog()
    {
        currentBranch = 0; currentNode = 0; opponent = 0;
        for (int i = 0; i < dialogViews.Length; i++)
            dialogViews[i].Hide(true);
        dialogEnd = enabled = false;
    }
    protected void Update()
    {
        if (Time.time >= nextTime)
        {
            if (dialogEnd)
                StopDialog();
            else
                dialogEnd = !GetNextNode();
        }

    }
}
