using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get50CoinsScript : MonoBehaviour {

    private Animator anim;
    [SerializeField] private Button OKBtn;
    [SerializeField] private Button CancelBtn;
    [SerializeField] private CongratsMessage Congrats;
    private string PID = "NULL";
    void Start () {
        anim = GetComponent<Animator>();
    }

    private void AnimationGet50CoinsDone()
    {
        Congrats.OpenCongratsWatchVideo(50);
    }
    public void Open(string pid)
    {
        this.PID = pid;
        anim = GetComponent<Animator>();
        anim.SetTrigger("open");
        OKBtn.onClick.AddListener(onCLICKOK);
        CancelBtn.onClick.AddListener(onCLICKCancel);
    }

    private void onCLICKOK()
    {
        if(PID != "NULL")
        {
            EventHandler.onRewardVideoComplete += RewardVideoComplete;
            EventHandler.onRewardVideoFailed += RewardVideoFailed;
            EventHandler.onRewardVideoSkiped += RewardVideoFailed;
            AdManager.Instance.UnityAd_Show(PID);

        }
    }
    public void RewardVideoComplete()
    {
        EventHandler.onRewardVideoComplete -= RewardVideoComplete;
        EventHandler.onRewardVideoSkiped -= RewardVideoFailed;
        SceneHandler.GetInstance().AddToTotalCoins(50);
        AnimationGet50CoinsDone();
        Close();
    }
    public void RewardVideoFailed()
    {
        EventHandler.onRewardVideoComplete -= RewardVideoComplete;
        EventHandler.onRewardVideoSkiped -= RewardVideoFailed;
        Close();
    }
    
    private void onCLICKCancel()
    {
        Close();
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
