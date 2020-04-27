
using UnityEngine;
using UnityEngine.UI;


public class GameOverPanelScript : MonoBehaviour {

    public static GameOverPanelScript Instance;

    [SerializeField] private CoinsPacksScript CoinsPacks;
    [SerializeField] private GameObject BodyContinue;
    [SerializeField] private GameObject BodyReplay;
    [SerializeField] private GameObject BodyNoConnection;
    [SerializeField] private GameObject BodyNoCoins;
    [SerializeField] private GameObject BodyNoAds;
    [SerializeField] private Text LevelProgressTxt;
    [SerializeField] private Text TotalCoinsTxt;
    [HideInInspector] public bool IsOpen;

    // Use this for initialization
  
    public  GameOverPanelScript()
    {
        Instance = this;
        IsOpen = false;
    }
   
    private void OnLevelWasLoaded(int level)
    {
        IsOpen = false;
    }
    public void onFadeINComplete()
    {
        
        //---GC
        //System.GC.Collect();
        //--
        if (gameObject != null)
        {
            LevelProgressTxt.text = GameObject.FindGameObjectWithTag("LevelMapping").GetComponent<LevelMapScript>().GetProgress();
            TotalCoinsTxt.text = SceneHandler.GetInstance().GetTotalCoins().ToString();

            //Show Body
            if (SceneHandler.GetInstance().GetisCheckedPoint())
            {
                BodyContinue.gameObject.SetActive(true);
                BodyReplay.gameObject.SetActive(false);
            }
            else
            {
                BodyContinue.gameObject.SetActive(false);
                BodyReplay.gameObject.SetActive(true);
            }
            BodyNoConnection.gameObject.SetActive(false);
            BodyNoCoins.gameObject.SetActive(false);
            BodyNoAds.gameObject.SetActive(false);
        }
    }
    public void ShowMe()
    {
        EventHandler.onBuyCoinsEvent += BuyCoinsComplete;
        LevelManager.Instance.HideButtonControl();
      //  if (!IsOpen && !PauseMenuScript.isOpen)
      //  {
            IsOpen = true;
            gameObject.SetActive(true);
            GetComponent<Animator>().SetTrigger("open");
      //  }
    }
    public void onClickWatchAD()
    {
        if(AdManager.Instance.IsNetworkAvailable())
        {
            
            string placementID = AdManager.Instance.IsRewardVideoReadyToShow();
            if (!placementID.Equals("NULL"))
            {
                SceneHandler.GetInstance().DebugLog("RewardVideo ID: " + placementID);
                EventHandler.onRewardVideoComplete += RewardVideoComplete;
                EventHandler.onRewardVideoFailed += RewardVideoFailed;
                AdManager.Instance.UnityAd_Show(placementID);
            }
            else
            {
                NoAds();
            }
        }
        else
        {
            NoConnection();
        }
    }

    public void onClickReplay()
    {
        IsOpen = false;
        SceneHandler.GetInstance().ResetCounter++;
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
        LevelManager.Instance.RestartLevel();
    }

   
    public void onClickIGiveUp()
    {
        IsOpen = false;
        /* if(AdmobManager.Instance.interstitial.IsLoaded())
         {
             AdmobManager.Instance.ShowInterstitial();
         }
         else
         {*/
        
        string PID = AdManager.Instance.IsSimpleVideoReadyToShow();
            if (!PID.Equals("NULL"))
            {
                SceneHandler.GetInstance().DebugLog("Simple Video ID: " + PID);
                AdManager.Instance.UnityAd_Show(PID);
            }
            else
            {
                SceneHandler.GetInstance().DebugLog("Fail to load Simple Video");
             }
        //}
        
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
        LoadingBarScript.Instance.LoadScene("MainMenu", "mainMenu");
    }

    public void onClickPay100Coins()
    {
        if(SceneHandler.GetInstance().GetTotalCoins() >= 100)
        {
            IsOpen = false;
            SceneHandler.GetInstance().ResetCounter++;
            SceneHandler.GetInstance().buySomeThing(100); //remove from the total coins >100!
            LevelManager.Instance.ContinueLevel();
        }
        else
        {
            noCoins();
        }
    }

    public void onClickOK_NoConnection()
    {
        BodyContinue.gameObject.SetActive(true);
        BodyNoConnection.gameObject.SetActive(false);
        BodyReplay.gameObject.SetActive(false);
        BodyNoCoins.gameObject.SetActive(false);
        BodyNoAds.gameObject.SetActive(false);
    }

    private void NoConnection()
    {
        BodyNoConnection.gameObject.SetActive(true);
        BodyContinue.gameObject.SetActive(false);
        BodyReplay.gameObject.SetActive(false);
        BodyNoCoins.gameObject.SetActive(false);
        BodyNoAds.gameObject.SetActive(false);
    }
    private void noCoins()
    {
        BodyNoCoins.gameObject.SetActive(true);
        BodyContinue.gameObject.SetActive(false);
        BodyReplay.gameObject.SetActive(false);
        BodyNoConnection.gameObject.SetActive(false);
        BodyNoAds.gameObject.SetActive(false);
    }

    public void NoAds()
    {
        BodyNoAds.gameObject.SetActive(true);
        BodyNoCoins.gameObject.SetActive(false);
        BodyContinue.gameObject.SetActive(false);
        BodyReplay.gameObject.SetActive(false);
        BodyNoConnection.gameObject.SetActive(false);
    }
    public void RewardVideoComplete()
    {
        IsOpen = false;
        SceneHandler.GetInstance().ResetCounter++;
        EventHandler.onRewardVideoComplete -= RewardVideoComplete;
        LevelManager.Instance.ContinueLevel();

    }
    public void RewardVideoFailed()
    {
        EventHandler.onRewardVideoFailed -= RewardVideoFailed;
        NoAds();
    }
    public void RewardVideoSkiped()
    {

    }

    public void GetMoreCoins()
    {
        //Show a Coins (Offre)!
        onClickOK_NoConnection();
        CoinsPacks.SetDialogActive(true);
        CoinsPacks.Open();
    }

    public void BuyCoinsComplete()
    {
      if(TotalCoinsTxt != null)  TotalCoinsTxt.text = SceneHandler.GetInstance().GetTotalCoins().ToString();
    }
}
