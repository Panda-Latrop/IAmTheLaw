using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static readonly int MAIN_MENU = 0, DOCUMENT = 1, COURT = 2, RESULT = 3;
    public static void Load(int _id, LoadSceneMode _mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(_id, _mode);
    }
}
