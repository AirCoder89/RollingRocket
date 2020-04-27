
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

//using admob;


public class AdManager : MonoBehaviour {
    public static AdManager Instance { set; get; }
    private ShowOptions AdSO;
   [SerializeField] private bool IsTestMode = false;
   [HideInInspector] public bool RealTimeNotworkStatus;
   private bool CheckRealTime;
    // Use this for initialization
   
    void Start () {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
       


        InitializeAD();

        CheckRealTime = false;
    }

    public void InitializeAD()
    {
        
          if (Advertisement.isSupported)
          {
              if (!Advertisement.isInitialized)
                  Advertisement.Initialize("2661719", IsTestMode);
        }
    }
    public string IsRewardVideoReadyToShow() //return placement id of the ready one else he return NULL
    {
       if(Advertisement.IsReady("rewardedVideo"))
         {
             return "rewardedVideo";
         }else if (Advertisement.IsReady("revivevideo2"))
         {
             return "revivevideo2";
         }
         else if (Advertisement.IsReady("earncoinsvideo"))
         {
             return "earncoinsvideo";
         }
         else
         {
             InitializeAD();
             return "NULL";
         }
      
    }

    public string IsSimpleVideoReadyToShow() //return placement id of the ready one else he return NULL
    {
        if(SceneHandler.GetInstance().Settings.isShowADS())
        {
            if (Advertisement.IsReady("video"))
            {
                return "video";
            }
            else if (Advertisement.IsReady("video2"))
            {
                return "video2";
            }
            else if (Advertisement.IsReady("video3"))
            {
                return "video3";
            }
            else
            {
                InitializeAD();
                return "NULL";
            }
        }
        else
        {
            return "NULL";
        }
        
    }
    public void RealTimeNetworkAvailable(bool startCheck)
    {
        CheckRealTime = startCheck;
    }
    public bool IsNetworkAvailable()
    {
        if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UnityAd_Show(string placementID)
    {
       /* #if UNITY_EDITOR
           StartCoroutine(WaitForAD());
        #endif*/

         AdSO = new ShowOptions();
         AdSO.resultCallback = HandleShowResult;

        Advertisement.Show(placementID, AdSO);
    }
    /*   IEnumerator WaitForAD()
      {
          float oldScale = Time.timeScale;
           Time.timeScale = 0f;
           yield return null;

           while (Advertisement.isShowing)
               yield return null;

           Time.timeScale = oldScale;


       }*/
    private void Update()
    {
       if(!CheckRealTime)
        {
            return;
        }
        else
        {
            RealTimeNotworkStatus = IsNetworkAvailable();
        }
    }
   
  private void HandleShowResult(ShowResult VideoResult)
    {
        switch(VideoResult)
        {
            case ShowResult.Failed: RewardVideoFailed(); break;
            case ShowResult.Skipped: RewardVideoSkipped(); break;
            case ShowResult.Finished: RewardVideoFinished(); break;
        }
    }

    private void RewardVideoFailed()
    {
        
        InitializeAD();
        EventHandler.RewardVideoFailed_TR();
    }
    private void RewardVideoSkipped()
    {
       
        InitializeAD();
        EventHandler.RewardVideoSkiped_TR();
    }
    private void RewardVideoFinished()
    {
       
        InitializeAD();
        EventHandler.RewardVideoComplete_TR();
    }
}
