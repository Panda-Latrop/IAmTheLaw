using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class StartMenuView : MonoBehaviour
{
    protected struct StuffView
    {
        public bool seted;
        public SpriteRenderer render;
        public Transform transform;

        public StuffView(Transform _transform, SpriteRenderer _render)
        {
            seted = true;
            render = _render;
            transform = _transform;
        }
    }


    [SerializeField]
    protected TMPro.TMP_Text money, heresy, loyalty;
    [SerializeField]
    protected GameObject roomBack;
    [SerializeField]
    protected SpriteRenderer roomR;
    protected StuffView[] stuffs = null;

    public void SetResources(int _money, int _loyalty, int _heresy)
    {
        money.SetText(_money.ToString());
        loyalty.SetText(_loyalty.ToString());
        heresy.SetText(_heresy.ToString());
    }
    public void ShowRoom(ResourcesSetting resources)
    {
        ScriptableHouse house = resources.GetHouse();
        roomR.sprite = house.Sprite;

        if(stuffs == null)
        {
            stuffs = new StuffView[resources.StuffCount];
        }
        for (int i = 0; i < resources.StuffCount; i++)
        {
            if (resources.GetStuffInfo(i).level > 0 )
            {
                int level = resources.GetStuffInfo(i).level - 1;
                if (!stuffs[i].seted)
                {
                    GameObject gameObject = new GameObject("Stuff");
                    StuffView view = new StuffView(gameObject.transform, gameObject.AddComponent<SpriteRenderer>());
                    view.transform.SetParent(roomBack.transform,false);
                    view.render.sprite = resources.GetStuff(i).Stuff(level).sprite;
                    view.transform.localPosition = house.GetPlace(i);
                    stuffs[i] = view;
                }
                else
                {
                    stuffs[i].render.sprite = resources.GetStuff(i).Stuff(level).sprite;
                    stuffs[i].transform.localPosition = house.GetPlace(i);
                }
            }
        }

    }
    public void HideBack()
    {
        roomBack.SetActive(false);
    }
    public void ShowBack()
    {
        roomBack.SetActive(true);
    }
}
