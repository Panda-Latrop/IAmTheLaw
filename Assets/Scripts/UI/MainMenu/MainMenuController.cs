using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    protected List<MenuElement> menus;
    [SerializeField]
    protected int current = 0;

    protected void Start()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].OnStart();
            menus[i].SetController(this);
            menus[i].Hide();
        }
        menus[current].Show();
    }


    public void ShowMenu(int _id)
    {
        menus[current].Hide();
        current = _id;
        menus[current].Show();
    }

}
