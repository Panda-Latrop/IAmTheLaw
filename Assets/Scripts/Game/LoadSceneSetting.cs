using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadSceneState
{
    delete,
    save,
    load,
}
public class LoadSceneSetting : DestroyableSingleton<LoadSceneSetting>
{
    public LoadSceneState state = LoadSceneState.save;
}
