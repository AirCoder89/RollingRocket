using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSelectGUIHandler : MonoBehaviour {

    
    [SerializeField] private GameObject[] Levels;
    [SerializeField] private CoinsPacksScript CoinsPack;
    [SerializeField] private BuyExtraStageDialog BuyExtraStage;

    [SerializeField] private NextAndBackPage GUIController;
    [SerializeField] private Button BackBtn;
    [SerializeField] private TextMeshProUGUI TotalCoinsTxt;
    
    private int MaxLevels;
    private SwipeHandler Swipe;
    private int SelectedLevel;
    public static LevelSelectGUIHandler Instance;
    private int TOTAL_COINS;
    void Start () {
        Instance = this;
        //set Dialog Disactive
        CoinsPack.SetDialogActive(false);
        BuyExtraStage.SetDialogActive(false);

        BackBtn.onClick.AddListener(onClickBACK);
        UpdateTotalCoinsText();
        Swipe = GetComponent<SwipeHandler>();
        SelectedLevel = 0; //must be 0 (level 1 selected at the start)
        MaxLevels = Levels.Length;
        InitGUILevels();
        EventHandler.onBuyCoinsEvent += onBuyCoinsComplete;
        
    }
    public void onBuyCoinsComplete()
    {
        //some animation here !!

        UpdateTotalCoinsText();
    }
    private void UpdateTotalCoinsText()
    {
        TOTAL_COINS = SceneHandler.GetInstance().GetTotalCoins();

        if (TOTAL_COINS < 10)
        {
            TotalCoinsTxt.text = "000" + TOTAL_COINS.ToString();
        }
        else if (TOTAL_COINS >= 10 && TOTAL_COINS < 100)
        {
            TotalCoinsTxt.text = "00" + TOTAL_COINS.ToString();
        }
        else if (TOTAL_COINS >= 100 && TOTAL_COINS < 1000)
        {
            TotalCoinsTxt.text = "0" + TOTAL_COINS.ToString();
        }
        else
        {
            TotalCoinsTxt.text = TOTAL_COINS.ToString();
        }
    }
    public void onClickBACK()
    {
        LoadingBarScript.Instance.LoadScene("MainMenu", "win");
    }
    void Update () {
        //Swipe handler Animation
        if(!CoinsPack.isOpen)
        {
            if (Swipe.SwipeLeft && SelectedLevel < MaxLevels - 1)
            {
                //if statment > fade out the current level on screen
                if (Levels[SelectedLevel].GetComponent<GUILevel>().isLocked)
                {
                    //level locked
                    Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("LockedFadeOut");
                }
                else
                {
                    //Level unlocked
                    Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("FadeOut");
                }

                SelectedLevel++;
                UpdateGUILevels(); //Fade in the next level
            }
            else if (Swipe.SwipeRight && SelectedLevel > 0)
            {
                //if statment > fade out the current level on screen
                if (Levels[SelectedLevel].GetComponent<GUILevel>().isLocked)
                {
                    //level locked
                    Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("LockedFadeOut");
                }
                else
                {
                    //Level unlocked
                    Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("FadeOut");
                }

                SelectedLevel--;
                UpdateGUILevels();//Fade in the next level
            }
        }
        
	}
    private void UpdateGUILevels()
    {
        if (Levels[SelectedLevel].GetComponent<GUILevel>().isLocked)
        {
            //Next level is locked
            Levels[SelectedLevel].SetActive(true);
            Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("LockedFadeIn");
        }
        else
        {
            //Next level is unlocked
            Levels[SelectedLevel].SetActive(true);
            Levels[SelectedLevel].GetComponent<Animator>().SetTrigger("FadeIn");
        }


        if(SelectedLevel == 0)
        {
            GUIController.SetPageIndex(1);
            GUIController.Next();
        }else if (SelectedLevel == 1)
        {
            GUIController.SetPageIndex(2);
            GUIController.NextAndBack();
        }
        else if (SelectedLevel == 2)
        {
            GUIController.SetPageIndex(3);
            GUIController.Back();
        }
    }


    private void InitGUILevels()
    {//will be called just one time in the start !

        //check level active or not
        GUIController.SetPageIndex(1);
        GUIController.Next();
        Levels[0].GetComponent<GUILevel>().UnlockLevel(SceneHandler.GetInstance().Stages.Level01);

        if (SceneHandler.GetInstance().Stages.isLevelActive("Level02"))
        {
            Levels[1].GetComponent<GUILevel>().UnlockLevel(SceneHandler.GetInstance().Stages.Level02);
        }
        else
        {
            Levels[1].GetComponent<GUILevel>().LockLevel();
        }

        if (SceneHandler.GetInstance().Stages.isLevelActive("Level03"))
        {
            Levels[2].GetComponent<GUILevel>().UnlockLevel(SceneHandler.GetInstance().Stages.Level03);
        }
        else
        {
            Levels[2].GetComponent<GUILevel>().LockLevel();
        }

        
        for (int i = 0; i < MaxLevels; i++)
        {
            if (i == SelectedLevel)
            {
                //selected level (always level 1 selected) !!
                Levels[i].SetActive(true);
                Levels[i].GetComponent<Animator>().SetTrigger("FadeIn");
            }
            else
            {
                //others levels
                Levels[i].SetActive(false);
            }
        }


    }
   

    public void ShowBuyStageDialog(Stage stage)
    {
        BuyExtraStage.SetDialogActive(true);
        BuyExtraStage.Open(stage.StageName);
    }

    public void ShowBuyCoinsDialog()
    {
        CoinsPack.SetDialogActive(true);
        CoinsPack.Open();
    }
}
