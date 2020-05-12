using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{
    void Awake()
    {
        switch (LoadSceneSetting.Instance.state)
        {
            case LoadSceneState.save:
                SaveSystem.SaveFile(ResourcesSetting.Instance.GetSave());
                break;
            case LoadSceneState.load:
                SaveData data = null;
                if (SaveSystem.LoadFile(ref data))
                    ResourcesSetting.Instance.SetSave(data);
                LoadSceneSetting.Instance.state = LoadSceneState.save;
                break;
            case LoadSceneState.delete:
                SaveSystem.DeleteFile();
                break;

            default:
                break;
        }
    }
}
