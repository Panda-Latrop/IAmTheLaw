using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New House", menuName = "Case/House", order = 52)] // 1
public class ScriptableHouse : ScriptableObject
{
    [SerializeField]
    protected string house;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected int cost, heresy;
    [SerializeField]
    protected List<Vector2> stuffPlace;

    public string House => house;
    public Sprite Sprite => sprite;
    public int Cost => cost;
    public int Heresy => heresy;
    public Vector2 GetPlace(int _id) => stuffPlace[_id];

}
