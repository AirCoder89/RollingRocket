using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    [SerializeField] private GameObject NoADSWindow;
    [SerializeField] private GameObject Get50CoinsWindow;
    [SerializeField] private GameObject DailyGift;
    [SerializeField] private GameObject DailyGiftWindow;
    [SerializeField] private Button Get50CoinsBtn;
    [SerializeField] private Button ShowADSBtn;

    private float timeSinceLastCalled;
    private float delay = 5f;
    private string PID = "NULL";
    // Use this for initialization
    void Start () {
        // PlayerPrefs.SetInt("RewardDay", 0);
        DailyGift = GameObject.FindGameObjectWithTag("DGift");

        PlayerPrefs.SetInt("LateContinue", 0);
        PlayerPrefs.SetInt("isContinue", 0);
        PlayerPrefs.SetInt("isCheckPoint", 0);
        
        SceneHandler.GetInstance().Audio.Play("MainGame",true);

        this.InitGUI();
    }

    private void Update()
    {
        //Every 5 sec ..
            timeSinceLastCalled += Time.deltaTime;
            if (timeSinceLastCalled > delay) 
            {
                timeSinceLastCalled = 0f;
                //check get 50 coins 
                UpdateGet50CoinsBtn();

                UpdateDailyGiftStatus();
                 UpdateShowADSBtn();
            }
    }

    private void InitGUI()
    {
        //if connected and there is a rewardVideo loaded > Active Get 5 Coins btn
        UpdateGet50CoinsBtn();
        UpdateDailyGiftStatus();
        UpdateShowADSBtn();
    }
    private void UpdateShowADSBtn()
    {
        if(SceneHandler.GetInstance().Settings.isShowADS())
        {
            ShowADSBtn.interactable = true;
        }
        else
        {
            ShowADSBtn.interactable = false;
        }
    }
    private void UpdateDailyGiftStatus()
    {
      //  DailyGift = GameObject.FindGameObjectWithTag("DGift");
        if (AdManager.Instance.IsNetworkAvailable())
        {
           
            DailyGift.GetComponent<DailyReward>().ENABLE_ME();
        }
        else
        {
            DailyGift.GetComponent<DailyReward>().DISABLE_ME();
        }
    }
    private void UpdateGet50CoinsBtn()
    {
        //Get50CoinsBtn = GameObject.FindGameObjectWithTag("Get50").GetComponent<Button>();
        if (AdManager.Instance.IsNetworkAvailable())
        {
            if(PID == "NULL")
            {
                PID = AdManager.Instance.IsRewardVideoReadyToShow();
                if (!PID.Equals("NULL"))
                {
                   // SceneHandler.GetInstance().DebugLog("Reward Video Get50 ID: " + PID);
                    //active Get50 btn
                    Get50CoinsBtn.interactable = true;
                }
                else
                {
                   // SceneHandler.GetInstance().DebugLog("Fail to load Reward Video Get50");
                    Get50CoinsBtn.interactable = false;
                }
            }
            
        }
        else
        {
            //no connection
            Get50CoinsBtn.interactable = false;
            PID = "NULL";
        }
    }
    public void onClickStart()
    {

         LoadingBarScript.Instance.LoadScene("LevelSelect", "win");
        // AudioManager.Instance.Play("ClickBtn");
    }

    public void GotoSettings()
    {
        LoadingBarScript.Instance.LoadScene("Settings", "win");
        //AudioManager.Instance.Play("ClickBtn");
    }
    public void GotoTheStore()
    {
        LoadingBarScript.Instance.LoadScene("Store", "win");
        //AudioManager.Instance.Play("ClickBtn");
    }
    public void VolumeUp()
    {
        SceneHandler.GetInstance().Audio.SetSoundEffectStatus(false);
    }
   
    public void ShowInterstitial()
    {
        SceneHandler.GetInstance().Admob.showInterstitialAd();
    }
    

    public void onClickFaceBook()
    {
        //PlayerPrefs.SetInt("RewardDay", 0);
        LoadingBarScript.Instance.LoadScene("Level03Stage06", "level");
    }

    public void onClickAboutCoder()
    {
        LoadingBarScript.Instance.LoadScene("Level03Stage07", "level");
    }

    public void onClickMoreGames()
    {
        //PlayerPrefs.SetInt("IsFirstOpen", 0);
        // PlayerPrefs.SetInt("RewardDay", 0);
        LoadingBarScript.Instance.LoadScene("Level03Stage05", "level");
    }


    public void onClickNoADS()
    {
        SceneHandler.GetInstance().AddToTotalCoins(5000);
      // NoADSWindow.SetActive(true);
      // NoADSWindow.GetComponent<NoADSWndowScript>().Open();
    }
    private void AnimationGet25CoinsAsGift()
    {
        PlayGUIAnimationGift.Instance.PlayGiftAnimation(25);
    }
    private int GetRewardDAY()
    {
        return PlayerPrefs.GetInt("RewardDay", 0);
    }
    public void OpenDailyGiftWindow()
    {
        int day = GetRewardDAY(); //first day = 0 - day 12 = 11
        if (day <= 11)
        {
            DailyGiftWindow.SetActive(true);
            DailyGiftWindow.GetComponent<DailyGiftWindow>().Open();
        }
        else
        {
            Debug.Log("YOU EARN >> " + 25);
            DailyGift.GetComponent<DailyReward>().StartAgain();
            SceneHandler.GetInstance().AddToTotalCoins(25);
            AnimationGet25CoinsAsGift();
        }
        
    }

    public void onClickGet50Coins()
    {
        if(PID != "NULL")
        {
            Get50CoinsWindow.SetActive(true);
            Get50CoinsWindow.GetComponent<Get50CoinsScript>().Open(PID);
            PID = "NULL";
        }
    }
}
