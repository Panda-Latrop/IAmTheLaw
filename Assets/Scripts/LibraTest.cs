using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraTest : MonoBehaviour
{
    public Transform middle, left, right;
    public int leftPow, rightPow, maxToMax = 50;
    public float maxAngle = 30.0f;

    [ContextMenu("Left")]
    public void AddToLeft()
    {
        leftPow += 10;
        MoveLibra();
    }
    [ContextMenu("Right")]
    public void AddToRight()
    {
        rightPow += 10;
        MoveLibra();
    }
    public void MoveLibra()
    {
        int delta = leftPow - rightPow;
        float procent = (float)delta/ (float)maxToMax;
        if (procent >= 1.0f)
            procent = 1.0f;
        if (procent <= -1.0f)
            procent = -1.0f;
        middle.rotation = Quaternion.AngleAxis(maxAngle * procent, Vector3.forward);
        left.rotation = right.rotation = Quaternion.identity;
    }
}
