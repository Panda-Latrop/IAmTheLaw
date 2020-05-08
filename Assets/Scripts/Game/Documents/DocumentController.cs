using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DocumentController : MonoBehaviour
{  
    protected GameSetting setting;
    [SerializeField]
    protected DocumentView documentView;
    [SerializeField]
    protected BrideView brideView;
    [SerializeField]
    protected SpriteRenderer refu, yes;
    [SerializeField]
    protected UIClickListenerSimple leftH, middleH, rightH;


    protected void Start()
    {
        setting = GameSetting.Instance;
        leftH.AddHandler(OnLeftClick);
        middleH.AddHandler(OnMiddleClick);
        rightH.AddHandler(OnRightClick);
        documentView.SetText(setting.Case.DocumentText, setting.Case.BribeIsAllow);
        if (setting.Case.BribeIsAllow)
            brideView.SetText(setting.Case.DocumentText, setting.Case.Bribe.toJail, setting.Case.Bribe.size);
        refu.enabled = yes.enabled = false;
    }
    protected void OnLeftClick(PointerEventData _eventData)
    {
        if (setting.Case.BribeIsAllow)
        {
            if (!brideView.IsHide)
            {
                setting.BribeAccept = false;
                Debug.Log("Refuse : Next Scene");
                LoadScene();
                return;
            }
        }   
        if (!documentView.IsStart)
        {
            documentView.PreviousPage();
            return;
        }
    }
    protected void OnMiddleClick(PointerEventData _eventData)
    {
        if (setting.Case.BribeIsAllow)
        {
            if (!brideView.IsHide)
            {
                brideView.Hide();
                refu.enabled = yes.enabled = false;
                documentView.SetPage(documentView.MaxPage);
                return;
            }
        }
        if (documentView.IsStart)
        {
            documentView.NextPage();
            return;
        }
          

    }
    protected void OnRightClick(PointerEventData _eventData)
    {
        if (setting.Case.BribeIsAllow)
        {
            if (!brideView.IsHide)
            {
                setting.BribeAccept = true;
                Debug.Log("Accept : Next Scene");
                LoadScene();
                return;
            }
            if (documentView.IsEnd && brideView.IsHide)
            {
                brideView.Unhide();
                refu.enabled = yes.enabled = true;
                documentView.SetPage(-2);
                return;
            }
        }
        else
        {
            if (documentView.IsEnd)
            {
                
                return;
            }
        }   
        if (!documentView.IsStart)
        {
            documentView.NextPage();
            return;
        }
    }
    protected void LoadScene()
    {
        SceneLoader.Load(SceneLoader.COURT);
    }
}
