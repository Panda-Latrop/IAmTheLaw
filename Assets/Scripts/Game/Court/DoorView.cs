using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorView : MonoBehaviour
{
    [SerializeField]
    protected Animator anim;
    protected int openHash, closeHash;

    protected void Start()
    {
        openHash = Animator.StringToHash("Open");
        closeHash = Animator.StringToHash("Close");
    }

    public void Open()
    {
        anim.SetTrigger(openHash);
    }
    public void Close()
    {
        anim.SetTrigger(closeHash);
    }
}
