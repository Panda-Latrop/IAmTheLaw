using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ResultController : MonoBehaviour
{
    protected GameSetting setting;
    protected ResourcesSetting resources;
    [SerializeField]
    protected ResultView resultView;
    [SerializeField]
    protected UIClickListenerSimple next;
    ResultSruct result;

    protected void Start()
    {
        setting = GameSetting.Instance;
        resources = ResourcesSetting.Instance;
        next.AddHandler(ToMainMenu);

        bool win,bride;
        for (int i = 0; i < setting.CaseCount; i++)
        {
            bride = setting.GetGameCase(i).BribeAccept && setting.GetGameCase(i).ToJail == setting.Day.GetCase(i).Bribe.toJail;
            win = setting.Day.GetCase(i).ToJail == setting.GetGameCase(i).ToJail;
            if (win)
            {
                result.winTime++;
                result.winMoney +=   setting.Day.GetCase(i).Result.winMoney;
                result.winLoyalty += setting.Day.GetCase(i).Result.winLoyalty;
                if(!bride)
                    result.winHeresy +=  setting.Day.GetCase(i).Result.winHeresy;
            }
            else
            {
                result.loseTime++;
                result.loseMoney +=   setting.Day.GetCase(i).Result.loseMoney;
                result.loseLoyalty += setting.Day.GetCase(i).Result.loseLoyalty;
                if (!bride)
                    result.loseHeresy +=  setting.Day.GetCase(i).Result.loseHeresy;
            }         
            if (bride)
            {
                result.brideTime++;
                result.brideMoney += resources.Heresy > 0 ? resources.Heresy * 100 : Global.STANDARD_BRIBE;
                result.brideLoyalty += 0;
                result.brideHeresy += setting.Day.GetCase(i).Bribe.heresy;
            }       
            if(setting.GetGameCase(i).FailLoyalty != 0 || setting.GetGameCase(i).FailHeresy != 0)
            {
                result.failTime++;
                result.failLoyalty += setting.GetGameCase(i).FailLoyalty;
                result.failHeresy += setting.GetGameCase(i).FailHeresy;
            }

        }
        result.totalMoney = result.winMoney + result.loseMoney + result.brideMoney;
        result.totalLoyalty = result.winLoyalty + result.loseLoyalty + result.brideLoyalty + result.failLoyalty;
        result.totalHeresy = result.winHeresy + result.loseHeresy + result.brideHeresy + result.failHeresy;
        resources.Money += result.totalMoney;
        resources.Loyalty += result.totalLoyalty;
        resources.Heresy += result.totalHeresy;
        result.totalMoney = resources.Money;
        result.totalLoyalty = resources.Loyalty;
        result.totalHeresy = resources.Heresy;
        setting.RestartDay();/////////////////////////////////////////////////////////////
        resultView.SetResult(result);
    }

    public void ToMainMenu(PointerEventData _eventData)
    {
        if (resources.Money < Global.GAME_OVER_MONEY || resources.Loyalty <= Global.GAME_OVER_LOYALTY || resources.Heresy <= Global.GAME_OVER_HERESY)
        {
            LoadSceneSetting.Instance.state = LoadSceneState.delete;
            SceneLoader.Load(SceneLoader.GAME_OVER);
        }        
        else
        {
            SceneLoader.Load(SceneLoader.HOME);
        }

    }
}
