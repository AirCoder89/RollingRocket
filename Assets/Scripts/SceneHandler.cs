using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    static protected SceneHandler instance;
    public Text Debug;
    public bool UseAnalytics = false;
    [HideInInspector] public int ResetCounter;
    [HideInInspector] public AdmobScript Admob;
    [HideInInspector] public StagesAndLevels Stages;
    [HideInInspector] public SettingsManager Settings;
    [HideInInspector] public AudioManager Audio;
    [HideInInspector] public int TOTAL_LEVEL_COINS; //include Bonus !
    private float XCheckPos;
    private float YCheckPos;
    private int LevelCoins=0;
    private int BonusCoins;
    private bool iscontinue = false;
    private bool isLatecontinue = false;
    private bool isCheckedpoint = false;
    
    private float FinalDistance;
    private float MaxFuel;
    
    private string NextLevelName;
    private string CurrentLevelName = "Level01";
    private int CoinsInLevel = 0;
    private void OnLevelWasLoaded(int level)
    {
        if (instance != null)
        {
            return;
        }
        //execute 1 time during the game
        Application.targetFrameRate = 60;
        ResetCounter = 0;
        
    }
    
    public void DebugLog(string txt)
    {
        Debug.text += "\n" + txt;
    }
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        Settings = GetComponent<SettingsManager>();
        Admob = GetComponent<AdmobScript>();
        Stages = GetComponent<StagesAndLevels>();
        Audio = GetComponent<AudioManager>();
        LevelCoins = 0;
        TOTAL_LEVEL_COINS = 0;

        //Purchaser.Instance.BuyNoAds();
    }

    public void UpdateCoins()
    {
        float distance = FinalDistance / 100;
        BonusCoins = Mathf.RoundToInt(distance) * 3;

        TOTAL_LEVEL_COINS = LevelCoins + BonusCoins;
       // AddToTotalCoins(totalLevelCoins);

        print("------------------------");
        print("FinalDistance: " + FinalDistance);
        print("FinalDistance /100: " + distance);
        print("- Coins: " + GetTotalCoins());
        print("- LevelCoins: " + LevelCoins);
        print("- BonusCoins: " + BonusCoins);
        print("- TOTAL LEVEL Coins: " + TOTAL_LEVEL_COINS);
        print("- TOTAL Coins: " + GetTotalCoins());
        print("------------------------");
    }

    public void RemoveCheckPoint()
    {
        iscontinue = false;
        isLatecontinue = false;
        isCheckedpoint = false;
        PlayerPrefs.SetString("lastCP", "None");
        XCheckPos = 0;
        YCheckPos = 0;
    }
  
    public void AddToTotalCoins(int HowMuch)
    {
        int totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        totalCoins += HowMuch;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }
    public void buySomeThing(int cost)
    { 
        int totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        totalCoins -= cost;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }
    public void RemoveLastCheckPointFromScene()
    {
        GameObject[] allCheckPt = GameObject.FindGameObjectsWithTag("CheckPoint");
        string lastCPName = PlayerPrefs.GetString("lastCP", "None");
        if(lastCPName != "None")
        {
            foreach (GameObject CP in allCheckPt)
            {
                if (CP.gameObject.name == lastCPName)
                {
                    Destroy(CP);
                    return;
                }
                /* if(CP.GetComponent<CheckPointScript>().GetID() == IDLastCheckPoint)
                  {
                      Destroy(CP);
                      return;
                  }*/
            }
        }
        
    }
    
    public void SaveTotalCoins(int totalCoins)
    {
        PlayerPrefs.SetInt("totalCoins",totalCoins);
    }

    public void HitCoin()
    {
        LevelCoins += 1;
        ShipController.Instance.UpdateCoinsText(LevelCoins);
    }
   
    
    public void ResetLevelCoins()
    {
        LevelCoins = 0;
        TOTAL_LEVEL_COINS = 0;
    }

    public void InitOnStart()
    {
        System.GC.Collect();
        //this will call every new stage! or reset or continue
        GameOverPanelScript.Instance.IsOpen = false;
    }
    public void GAME_OVER()
    {
        GameObject FuelPanel = GameObject.FindGameObjectWithTag("FuelPanelAnim");
        if (FuelPanel != null)
        {
            FuelPanel.SetActive(false);
        }
        StartCoroutine(Show_Game_Over_Panel());
    }

    IEnumerator Show_Game_Over_Panel()
    {
        yield return new WaitForSeconds(0.5f);
        GameOverPanelScript.Instance.ShowMe();
    }
    //######################## GET & SET #####################################
    //--Get
    #region Gets
    public static SceneHandler GetInstance()
    {
        return instance;
    }
    public bool GetisContinue()
    {
        return iscontinue;
    }
    public bool GetisLateContinue()
    {
        return isLatecontinue;
    }
    public int GetLevelCoins()
    {
        return LevelCoins;
    }
    public int GetBonusLevel()
    {
        return BonusCoins;
    }
    public int GetTotalCoins()
    {
        return PlayerPrefs.GetInt("totalCoins", 0);
    }
    public Vector3 GetSavedPosition()
    {
        return new Vector3(XCheckPos, YCheckPos, 0);
    }
    public float GetMaxFuel()
    {
        return MaxFuel;
    }
    public bool GetisCheckedPoint()
    { //check if player hit at least one check point
        return isCheckedpoint;
    }

    public void SetNextLevelName(string levelName)
    {
        NextLevelName = levelName;
    }
    public string GetNextLevelName()
    {
        return NextLevelName;
    }
    public string GetCurrentLevelName()
    {
        return CurrentLevelName;
    }
    public float GetFinalDistance()
    {
        return FinalDistance;
    }
    public int GetCoinsInLevel()
    {
        return CoinsInLevel;
    }
    #endregion
    #region Sets
    //--Set
    public void SetCoinsInLevel(int nbCoins)
    {
        CoinsInLevel = nbCoins;
    }
    public void SetIsContinue(bool status)
    {
        iscontinue = status;
    }
    public void SetCurrentLevelName(string levelName)
    {
        CurrentLevelName = levelName;
    }
    public void SetIsLateContinue(bool status)
    {
        isLatecontinue = status;
    }
    public void SetCheckPoint(string nameCP, float Xpos, float Ypos)
    {
        isCheckedpoint = true;
        //IDLastCheckPoint = id;
        PlayerPrefs.SetString("lastCP", nameCP);
        XCheckPos = Xpos;
        YCheckPos = Ypos;
    }
    public void SetMaxFuel()
    {
        ShipData Ship = new ShipData();
        int MaxFuelLevel = Ship.GetLevelFuelTank();
        MaxFuel = Ship.GetFuelTank(MaxFuelLevel);
    }
    public void SetFinalDistance(float d)
    {
        FinalDistance = d;
    }
    #endregion
}
