using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ScoreStruct
{
    public TMPro.TMP_Text leftScoreText, rightScoreText;
}

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    protected ScoreStruct scoreStruct;
    public void SetScore(int _score, int _side)
    {
        if (_side == 0)
            scoreStruct.leftScoreText.SetText(_score.ToString());
        else
            scoreStruct.rightScoreText.SetText(_score.ToString());
    }
}
