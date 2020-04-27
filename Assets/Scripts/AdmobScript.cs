using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobScript : MonoBehaviour
{
	InterstitialAd interstitial;
	public string InterstitialId;

	void Start ()
	{
		RequestInterstitial ();
	}

	public void showInterstitialAd ()
	{
		//Show Ad
		if (interstitial.IsLoaded ()) {
            SceneHandler.GetInstance().DebugLog("Interstitial Loaded !!");
			interstitial.Show ();
        }
        else
        {
            SceneHandler.GetInstance().DebugLog("Interstitial Fail to load !!");
            RequestInterstitial();
        }
	}
    
	private void RequestInterstitial ()
	{
		#if UNITY_ANDROID
		string adUnitId = InterstitialId;
#elif UNITY_IPHONE
			string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
			string adUnitId = "unexpected_platform";
#endif

      //  SceneHandler.GetInstance().DebugLog("Interstitial Request ...");
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd (adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder ().Build ();
		// Load the interstitial with the request.
		interstitial.LoadAd (request);
	}

}