using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSet : MonoBehaviour
{
    public CourtController court;
    public DialogController dialog;
    void Start()
    {
        //court.OnRoundEnd += dialog.StartDialog;
    }

}
