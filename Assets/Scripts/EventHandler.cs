using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    public delegate void Events();
    public delegate void EventsWithParam(string type);
   
    public static event Events onShipDieEvent;
    public static event Events onStartGame;
    public static event Events onRewardVideoComplete;
    public static event Events onRewardVideoFailed;
    public static event Events onRewardVideoSkiped;
   
    public static event Events onLevelWinEvent;
    public static event Events onEndOfLevelEvent; //to stop camera follow in the end of level

    public static event Events onBuyExtraStageEvent;
    public static event Events onBuyCoinsEvent;
    public static event EventsWithParam onPurchaseDone;
    public static event Events onGeneratePhase;
    
    public static void PurchaseDone_TR(string nameOfPurchase)
    {
        if (onPurchaseDone != null) onPurchaseDone(nameOfPurchase);
    }
    public static void GeneratePhase_TR()
    {
        if (onGeneratePhase != null) onGeneratePhase();
    }
    public static void BuyCoins_TR()
    {
        if (onBuyCoinsEvent != null) onBuyCoinsEvent();
    }
    public static void BuyExtraStage_TR()
    {
        if (onBuyExtraStageEvent != null) onBuyExtraStageEvent();
    }
    public static void RewardVideoSkiped_TR()
    {
        if (onRewardVideoSkiped != null) onRewardVideoSkiped();
    }
    public static void RewardVideoFailed_TR()
    {
        if (onRewardVideoFailed != null) onRewardVideoFailed();
    }
    public static void RewardVideoComplete_TR()
    {
        if (onRewardVideoComplete != null) onRewardVideoComplete();
    }
    public static void LevelInTheEnd_TR()
    {
        if (onEndOfLevelEvent != null) onEndOfLevelEvent();
    }
    public static void LevelWin_TR()
    {
        if (onLevelWinEvent != null) onLevelWinEvent();
    }
   
    public static void GameStarted_TR()
    {
        if (onStartGame != null) onStartGame();
    }
    public static void ShipDie_TR()
    {
        if (onShipDieEvent != null) onShipDieEvent();
    }

}
