using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyGiftWindow : MonoBehaviour {

    [SerializeField] private Button OkBtn;
    [SerializeField] private List<Button> GiftBtns;
    [SerializeField] private List<Sprite> ClaimedSprite;
    [SerializeField] private List<int> DailyGiftAmount;
    [SerializeField] private DailyReward RewardsCounter;
    private Animator anim;
    private int CurrentDay;
    private void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    private void onCLICKOK()
    {
        Close();
    }
    private void ClaimReward()
    {
        int reward;
        
            reward = DailyGiftAmount[CurrentDay];
            CurrentDay++;
            SetRewardDAY(CurrentDay);
            Debug.Log("YOU EARN >> " + reward);
            SceneHandler.GetInstance().AddToTotalCoins(reward);


        //start count time for the next gift
        RewardsCounter.StartAgain();

        //close me after an animation complete
        AnimationGetReward(reward);
    }
    private void AnimationGetReward(int r)
    {

        PlayGUIAnimationGift.Instance.PlayGiftAnimation(r);
        Close();
    }
    public void Open()
    {
        CurrentDay = GetRewardDAY(); //first day = 0 - day 12 = 11
        anim = GetComponent<Animator>();
        anim.SetTrigger("open");
        OkBtn.onClick.AddListener(onCLICKOK);

        for(int day=0; day < GiftBtns.Count; day++)
        {
            if(day < CurrentDay)
            {
                GiftBtns[day].GetComponent<Image>().sprite = ClaimedSprite[day];
                GiftBtns[day].onClick.RemoveAllListeners();
                GiftBtns[day].enabled = false;

            }else if (day == CurrentDay)
            {
                GiftBtns[day].interactable = true;
                GiftBtns[day].onClick.AddListener(ClaimReward);
            }
            else
            {
                GiftBtns[day].interactable = false;
                GiftBtns[day].onClick.RemoveAllListeners();
            }
        }

    }
    private int GetRewardDAY()
    {
        return PlayerPrefs.GetInt("RewardDay", 0);
    }
    private void SetRewardDAY(int day)
    {
        PlayerPrefs.SetInt("RewardDay", day);
    }
    public void Close()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("close");
    }

    public void FadeOutComplete()
    {
        gameObject.SetActive(false);
    }
}
