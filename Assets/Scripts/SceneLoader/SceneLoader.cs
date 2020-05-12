using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static readonly int MAIN_MENU = 0, HOME = 1, DOCUMENT = 2, COURT = 3, RESULT = 4, GAME_OVER = 5;
    public static void Load(int _id, LoadSceneMode _mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(_id, _mode);
    }
}
