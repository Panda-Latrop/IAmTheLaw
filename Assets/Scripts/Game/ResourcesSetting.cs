using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StuffInfo
{
    public int level;
    public bool sold;
    public StuffInfo(int level, bool sold)
    {
        this.level = level;
        this.sold = sold;
    }
    public StuffInfo(StuffInfo stuff)
    {
        level = stuff.level;
        sold = false;
    }
}

public class ResourcesSetting : Singleton<ResourcesSetting>
{
    [SerializeField]
    protected int money, loyalty, heresy;
    [SerializeField]
    protected List<ScriptableHouse> houses;
    [SerializeField]
    protected int houseInfo;
    [SerializeField]
    protected List<ScriptableStuff> stuffs;
    [SerializeField]
    protected List<StuffInfo> stuffInfos;

    public int Money { get => money; set => money = value; }
    public int Loyalty { get => loyalty; set => loyalty = value; }
    public int Heresy { get => heresy; set => heresy = value; }
    public ScriptableHouse GetHouse(int _id) => houses[_id];
    public ScriptableHouse GetHouse() => houses[houseInfo];
    public int HouseInfo { get => houseInfo; set => houseInfo = value; }
    public int HouseCount => houses.Count;
    public ScriptableStuff GetStuff(int _id) => stuffs[_id];
    public StuffInfo GetStuffInfo(int _id) => stuffInfos[_id];
    public void SetStuffInfo(int _id, StuffInfo _value) => stuffInfos[_id] = _value;
    public int StuffCount => stuffs.Count;

    public void SetSave(SaveData _data)
    {
        money = _data.money;
        loyalty = _data.loyalty;
        heresy = _data.heresy;
        stuffInfos.Clear();
        for (int i = 0; i < _data.stuffInfos.Length; i++)
        {
            stuffInfos.Add(_data.stuffInfos[i]);
        }
    }
    public SaveData GetSave()
    {
        SaveData data = new SaveData();
        data.money = money;
        data.loyalty = loyalty;
        data.heresy = heresy;
        data.stuffInfos = new StuffInfo[stuffInfos.Count];
        for (int i = 0; i < stuffInfos.Count; i++)
        {
            data.stuffInfos[i] = stuffInfos[i];
        }
        return data;
    }
}

