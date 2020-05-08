using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stuff", menuName = "Case/Stuff", order = 52)] // 1
public class ScriptableStuff : ScriptableObject
{
    [SerializeField]
    protected List<Stuff> upgrades = new List<Stuff>();

    public Stuff Stuff(int _id) => upgrades[_id];
    public int Count => upgrades.Count;
    public int Last => upgrades.Count-1;
}
[System.Serializable]
public struct Stuff
{
    public string name;
    public Sprite sprite;
    public int cost, heresy;
}