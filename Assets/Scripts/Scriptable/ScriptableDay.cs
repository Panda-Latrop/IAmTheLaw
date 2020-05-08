using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Day", menuName = "Case/Day", order = 52)] // 1
public class ScriptableDay : ScriptableObject
{
    [SerializeField]
    protected TextAsset newspaperText;
    [SerializeField]
    protected ScriptableCase[] cases;

    public TextAsset NewspaperText => newspaperText;
    public ScriptableCase GetCase(int _id) => cases[_id];
    public int CaseCount => cases.Length;
}
