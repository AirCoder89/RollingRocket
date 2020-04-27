using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobManager : MonoBehaviour {
   
    public static AdmobManager Instance;

    [HideInInspector]
    public InterstitialAd interstitial;

    private string Interstitial_ID;
 
    private void OnLevelWasLoaded(int level)
    {
        if (Instance != null)
        {
            return;
        }
    }
    private void Start()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
       
        Interstitial_ID = "ca-app-pub-7308931598839605/5362508755";
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        
        #if UNITY_ANDROID
                string adUnitId = Interstitial_ID;
#elif UNITY_IPHONE
			        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
			        string adUnitId = "unexpected_platform";
#endif
        SceneHandler.GetInstance().DebugLog("Request Interstitial");

        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
       // interstitial.OnAdLoaded += InterstitilLoaded;
       // interstitial.OnAdClosed += InterstitilClosed;
        interstitial.LoadAd(request);
    }

  /*  public void InterstitilLoaded(object sender, EventArgs args)
    {
        SceneHandler.GetInstance().DebugLog("Interstitial Loaded and ready to show");
    }
    public void InterstitilClosed(object sender, EventArgs args)
    {
        SceneHandler.GetInstance().DebugLog("Interstitial Closed .. send new request");
        RequestInterstitial();
    }*/


    //return true if there is an interstitial loaded - false if not
    //call this show ad! return false if there is no ad loaded!!
    public void ShowInterstitial()
    {
        SceneHandler.GetInstance().DebugLog("Showing Interstitial");
        interstitial.Show();
    }
   
}
