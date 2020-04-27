using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelWinHandler : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI CollectedCoinsTxt;
    [SerializeField] private TextMeshProUGUI BonusCoins;
    [SerializeField] private TextMeshProUGUI TotalLevelCoinsTxt;
    
    
    [SerializeField] private Button NextLevelBtn;
    [SerializeField] private Button ReplayLevelBtn;
    [SerializeField] private Button MainMenuBtn;

    [SerializeField] private CongratsMessage Congrats;
    [SerializeField] private TextMeshProUGUI TotalCoinsTxt;

    [SerializeField] private Button DoubleEarnBtn;
    [SerializeField] private Animator DoubleAnim;

    private int GiftCoins = 0;
    private int levelCoins = 0;
    private int ClearBonus = 0;
    private int TotalCoins = 0;
    private string placementID;
    
    void Start () {
        StartCoroutine(ShowLevelAndBonusCoins());
    }
    private void UpdateTotalCoins()
    {
        TotalCoins = SceneHandler.GetInstance().GetTotalCoins();
        if (TotalCoins < 10)
        {
            TotalCoinsTxt.text = "000" + TotalCoins.ToString();
        }
        else if (TotalCoins >= 10 && TotalCoins < 100)
        {
            TotalCoinsTxt.text = "00" + TotalCoins.ToString();
        }
        else if (TotalCoins >= 100 && TotalCoins < 1000)
        {
            TotalCoinsTxt.text = "0" + TotalCoins.ToString();
        }
        else
        {
            TotalCoinsTxt.text = TotalCoins.ToString();
        }
    }
    IEnumerator ShowLevelAndBonusCoins()
    {
        yield return new WaitForSeconds(3f); //until the open animation Complete !!
       

        //levelCoins
        while ( levelCoins < SceneHandler.GetInstance().GetLevelCoins() )
        {
            levelCoins++;
            if(levelCoins < 10)
            {
                CollectedCoinsTxt.text = "00" + levelCoins.ToString();
            }else if (levelCoins >= 10 && levelCoins < 100)
            {
                CollectedCoinsTxt.text = "0" + levelCoins.ToString();
            }else if(levelCoins >= 100)
            {
                CollectedCoinsTxt.text = levelCoins.ToString();
            }

            yield return new WaitForEndOfFrame();

            yield return null;
        }
        
        //
        //Mathf.RoundToInt(Random.Range(30, 50))
        //clear bonus
        while ( ClearBonus < SceneHandler.GetInstance().GetBonusLevel() )
        {
            ClearBonus++;
            if (ClearBonus < 10)
            {
                BonusCoins.text = "00" + ClearBonus.ToString();
            }
            else if (ClearBonus >= 10 && ClearBonus < 100)
            {
                BonusCoins.text = "0" + ClearBonus.ToString();
            }
            else if (ClearBonus >= 100)
            {
                BonusCoins.text = ClearBonus.ToString();
            }

            //yield return new WaitForSeconds(0.01f);
            yield return new WaitForEndOfFrame();

            yield return null;
        }


        //end
        int Total = levelCoins + ClearBonus;

        if (Total < 10)
        {
            TotalLevelCoinsTxt.text = "00" + Total.ToString();
        }
        else if (Total >= 10 && Total < 100)
        {
            TotalLevelCoinsTxt.text = "0" + Total.ToString();
        }
        else if (Total >= 100)
        {
            TotalLevelCoinsTxt.text = Total.ToString();
        }

        SceneHandler.GetInstance().AddToTotalCoins(Total); //add the collected level coins and clear bonus to TOTAL Coins
        UpdateTotalCoins();
        //show TOTAL and play Coins FX

        //-- add event to btn's
        ShowGiftAnimation();
        yield return null;
    }
    
    private void ShowGiftAnimation()
    {
        if (TotalCoins >= Mathf.RoundToInt(SceneHandler.GetInstance().GetCoinsInLevel() / 4))
        {
            GiftCoins = Mathf.RoundToInt(Random.Range(20f, 50f));
            PlayGUIAnimationGift.Instance.PlayGiftAnimation(GiftCoins);
        }

        AddEventListener();
    }

    private void DoubleReward()
    {
        //adVideo btn
        if (AdManager.Instance.IsNetworkAvailable())
        {
            placementID = AdManager.Instance.IsRewardVideoReadyToShow();
            if (!placementID.Equals("NULL"))
            {
                DoubleAnim.SetTrigger("open");
                DoubleEarnBtn.onClick.AddListener(onClickADVideo);
            }
        }
    }
    private void AddEventListener()
    {

        DoubleReward();

        //-rest
        NextLevelBtn.onClick.AddListener(GoNextLevel);
        ReplayLevelBtn.onClick.AddListener(GoRepeatLevel);
        MainMenuBtn.onClick.AddListener(GoMainMenu);
    }
    public void GoNextLevel()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
        LoadingBarScript.Instance.LoadScene(SceneHandler.GetInstance().GetNextLevelName(), "level");
    }

    public void GoRepeatLevel()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
        LoadingBarScript.Instance.LoadScene(SceneHandler.GetInstance().GetCurrentLevelName(), "level");
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
        LoadingBarScript.Instance.LoadScene("MainMenu", "mainMenu");
        
    }

   
    public void onClickADVideo()
    {
        if (!placementID.Equals("NULL"))
        {
            DoubleAnim.SetTrigger("close");
            EventHandler.onRewardVideoComplete += RewardVideoComplete;
            EventHandler.onRewardVideoFailed += RewardVideoFailed;
            EventHandler.onRewardVideoSkiped += RewardVideoFailed;
            AdManager.Instance.UnityAd_Show(placementID);
        }
    }
    public void RewardVideoComplete()
    {
        SceneHandler.GetInstance().AddToTotalCoins(SceneHandler.GetInstance().TOTAL_LEVEL_COINS);
        Congrats.OpenCongratsWatchVideo(SceneHandler.GetInstance().TOTAL_LEVEL_COINS);
        UpdateTotalCoins();
    }
   
    public void RewardVideoFailed()
    {
        //ADVideoBtn.interactable = false;
        //ADVideoBtn.onClick.RemoveListener(onClickADVideo);
    }
}
