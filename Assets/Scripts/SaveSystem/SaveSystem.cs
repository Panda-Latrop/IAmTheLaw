using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public int money, loyalty, heresy;
    public int houseInfo;
    public StuffInfo[] stuffInfos;
}

public class SaveSystem
{
    public static bool SaveExist(string _name = "save.sv")
    {
        return File.Exists(Application.dataPath + "/Saves/" + _name);
    }

    public static void SaveFile(SaveData _data, string _name = "save.sv")
    {
        Debug.Log(Application.dataPath);
        string destination = Application.dataPath + "/Saves/";
        FileStream file;
       if (!Directory.Exists(destination))
            Directory.CreateDirectory(destination);
        if (File.Exists(destination))
            file = File.OpenWrite(destination + _name);
        else
            file = File.Create(destination + _name);

        BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, _data);
        file.Close();
    }

    public static bool LoadFile(ref SaveData _data, string _name = "save.sv")
    {
        Debug.Log(Application.dataPath);
        string destination = Application.dataPath + "/Saves/" + _name;//persistentDataPath
        FileStream file;

        if (File.Exists(destination))
            file = File.OpenRead(destination);
        else
        {
            Debug.LogWarning("File not found");
            return false;
        }
        BinaryFormatter bf = new BinaryFormatter();
        _data = (SaveData)bf.Deserialize(file);
        file.Close();
        return true;
    }
    public static void DeleteFile(string _name = "save.sv")
    {
        string destination = Application.dataPath + "/Saves/" + _name;
        if (File.Exists(destination))
            File.Delete(destination);
    }
}
