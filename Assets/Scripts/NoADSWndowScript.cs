using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoADSWndowScript : MonoBehaviour {

    private Animator anim;
    [SerializeField] private Button OKBtn;
    [SerializeField] private Button CancelBtn;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        EventHandler.onPurchaseDone += onBuyNOAdsComplete;
    }
	
    public void Open()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("open");
        OKBtn.onClick.AddListener(onCLICKOK);
        CancelBtn.onClick.AddListener(onCLICKCancel);
    }

    public void FadeInComplete()
    {
       
    }

    private void onCLICKOK()
    {
        Purchaser.Instance.BuyNoAds();
    }

    private void onCLICKCancel()
    {
        Close();
    }
    public void onBuyNOAdsComplete(string purchaseName)
    {
        if(purchaseName == "NO_ADS")
        {
            EventHandler.onPurchaseDone -= onBuyNOAdsComplete;
            SceneHandler.GetInstance().Settings.SetShowADS(false);
            AnimationBuyDone();
            Close();
        }
    }

    private void AnimationBuyDone()
    {
       
    }
    public void Close()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("close");
    }

    public void FadeOutComplete()
    {
        //OKBtn.onClick.RemoveAllListeners();
        //CancelBtn.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
