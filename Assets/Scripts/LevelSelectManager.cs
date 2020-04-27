using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectManager : MonoBehaviour {
    public static LevelSelectManager Instance;

    [SerializeField] private TextMeshProUGUI TotalCoinsTxt;

    [SerializeField] private Button BackBtn;

    [SerializeField] private Button BuyLevel2Btn;
    [SerializeField] private Button BuyLevel3Btn;

    [SerializeField] private Button Level01Btn;
    [SerializeField] private Button Level02Btn;
    [SerializeField] private Button Level03Btn;

    [SerializeField] private SelectStagePanel Level01Panel;
    [SerializeField] private SelectStagePanel Level02Panel;
    [SerializeField] private SelectStagePanel Level03Panel;

    [SerializeField] private Sprite SelectedLevel01SP;
    [SerializeField] private Sprite SelectedLevel02SP;
    [SerializeField] private Sprite SelectedLevel03SP;

    [SerializeField] private Sprite originalLevel01SP;
    [SerializeField] private Sprite originalLevel02SP;
    [SerializeField] private Sprite originalLevel03SP;


    [SerializeField] private GameObject BuyLevelDialog;
    [SerializeField] private GameObject BuyCoinsDialog;

    private string currentLevel = "Level00";
    private int TOTAL_COINS;
    private void Start()
    {
        EventHandler.onBuyCoinsEvent += onBuyCoinsComplete;
        currentLevel = "Level00";
        Instance = this;
        initGUI();
    }
    
    private void initGUI()
    {
        //init total coins
        TOTAL_COINS = SceneHandler.GetInstance().GetTotalCoins();
        UpdateTotalCoinsText();
        //init level buttons
        BackBtn.onClick.AddListener(onClickBACK);
        if (SceneHandler.GetInstance().Stages.isLevelActive("Level01"))
        {
            Level01Btn.interactable = true;
            ClickToLevel01();
        }
        Level01Btn.onClick.AddListener(ClickToLevel01);

        if (SceneHandler.GetInstance().Stages.isLevelActive("Level02"))
        {
            Level02Btn.interactable = true;
            BuyLevel2Btn.gameObject.SetActive(false);
        }
        else
        {
            BuyLevel2Btn.onClick.AddListener(onClickBuyLevel2);
        }
        Level02Btn.onClick.AddListener(ClickToLevel02);

        if (SceneHandler.GetInstance().Stages.isLevelActive("Level03"))
        {
            Level03Btn.interactable = true;
            BuyLevel3Btn.gameObject.SetActive(false);
        }
        else
        {
            BuyLevel3Btn.onClick.AddListener(onClickBuyLevel3);
        }
        Level03Btn.onClick.AddListener(ClickToLevel03);
        //init level panel
        Level01Panel.SetLevel(SceneHandler.GetInstance().Stages.Level01);
        Level02Panel.SetLevel(SceneHandler.GetInstance().Stages.Level02);
        Level03Panel.SetLevel(SceneHandler.GetInstance().Stages.Level03);
    }

    public void onClickBACK()
    {
        LoadingBarScript.Instance.LoadScene("MainMenu", "win");
    }
    private void UpdateTotalCoinsText()
    {
        if(TOTAL_COINS < 10)
        {
            TotalCoinsTxt.text = "000" + TOTAL_COINS.ToString();
        }else if(TOTAL_COINS >= 10 && TOTAL_COINS < 100)
        {
            TotalCoinsTxt.text = "00" + TOTAL_COINS.ToString();
        }else if(TOTAL_COINS >= 100 && TOTAL_COINS < 1000)
        {
            TotalCoinsTxt.text = "0" + TOTAL_COINS.ToString();
        }
        else
        {
            TotalCoinsTxt.text = TOTAL_COINS.ToString();
        }
    }
    public void onClickBuyLevel2()
    {
        if(TOTAL_COINS >= 1200)
        {
            TOTAL_COINS -= 1200;
            SceneHandler.GetInstance().buySomeThing(1200);
            UpdateTotalCoinsText();
            SceneHandler.GetInstance().Stages.UnlockLevelByName("Level02",true);
            //init level panel
            Level01Panel.SetLevel(SceneHandler.GetInstance().Stages.Level01);
            Level02Panel.SetLevel(SceneHandler.GetInstance().Stages.Level02);
            Level03Panel.SetLevel(SceneHandler.GetInstance().Stages.Level03);
            Level02Btn.interactable = true;
            BuyLevel2Btn.gameObject.SetActive(false);
            ClickToLevel02();
        }
        else
        {
            ShowBuyCoinsDialog();
        }
    }

    public void onClickBuyLevel3()
    {
        if (TOTAL_COINS >= 2000)
        {
            TOTAL_COINS -= 2000;
            SceneHandler.GetInstance().buySomeThing(2000);
            UpdateTotalCoinsText();
            SceneHandler.GetInstance().Stages.UnlockLevelByName("Level03",true);
            //init level panel
            Level01Panel.SetLevel(SceneHandler.GetInstance().Stages.Level01);
            Level02Panel.SetLevel(SceneHandler.GetInstance().Stages.Level02);
            Level03Panel.SetLevel(SceneHandler.GetInstance().Stages.Level03);
            Level03Btn.interactable = true;
            BuyLevel3Btn.gameObject.SetActive(false);
            ClickToLevel03();
        }
        else
        {
            ShowBuyCoinsDialog();
        }
    }
    public void ClickToLevel01()
    {
        if (currentLevel != "Level01")
        {
            currentLevel = "Level01";
            Level01Panel.OpenPanel();
            Level02Panel.ClosePanel();
            Level03Panel.ClosePanel();

            //Gui handler sprite
            Level01Btn.GetComponent<Image>().sprite = SelectedLevel01SP;
            Level02Btn.GetComponent<Image>().sprite = originalLevel02SP;
            Level03Btn.GetComponent<Image>().sprite = originalLevel03SP;
        }
        
    }

    public void ClickToLevel02()
    {
        if (currentLevel != "Level02")
        {
            currentLevel = "Level02";
            Level02Panel.OpenPanel();
            Level01Panel.ClosePanel();
            Level03Panel.ClosePanel();

            //Gui handler sprite
            Level02Btn.GetComponent<Image>().sprite = SelectedLevel02SP;
            Level01Btn.GetComponent<Image>().sprite = originalLevel01SP;
            Level03Btn.GetComponent<Image>().sprite = originalLevel03SP;
        }
    }

    public void ClickToLevel03()
    {
        if (currentLevel != "Level03")
        {
            currentLevel = "Level03";
            Level03Panel.OpenPanel();
            Level01Panel.ClosePanel();
            Level02Panel.ClosePanel();

            //Gui handler sprite
            Level03Btn.GetComponent<Image>().sprite = SelectedLevel03SP;
            Level01Btn.GetComponent<Image>().sprite = originalLevel01SP;
            Level02Btn.GetComponent<Image>().sprite = originalLevel02SP;
        }
    }
    public void ShowBuyCoinsDialog()
    {
       /* if(!BuyCoinsDialogScript.isOpen)
        {
            BuyCoinsDialog.SetActive(true);
            BuyCoinsDialog.GetComponent<BuyCoinsDialogScript>().Show();
        }*/
    }
    public void ShowBuyStageDialog(Stage stage)
    {
       if(!BuyLevelDialogScript.isOpen)
        {
            string priceTxt = "STAGE undifined !!";
            if (stage.StageName == "Level01Stage05" || stage.StageName == "Level02Stage05" || stage.StageName == "Level03Stage05")
            {
                priceTxt = "Stage > Stage 05\nPrice > 0.99 $";
            }
            else if (stage.StageName == "Level01Stage06" || stage.StageName == "Level02Stage06" || stage.StageName == "Level03Stage06")
            {
                priceTxt = "Stage > Stage 06\nPrice > 0.99 $";
            }
            else if (stage.StageName == "Level01Stage07" || stage.StageName == "Level02Stage07" || stage.StageName == "Level03Stage07")
            {
                priceTxt = "Stage > Stage 07\nPrice > 0.99 $";
            }
            BuyLevelDialog.SetActive(true);
            BuyLevelDialog.GetComponent<BuyLevelDialogScript>().Show(priceTxt, stage.StageName);
        }
    }

    public void onBuyCoinsComplete()
    {
        //you can make here some effect 
        TOTAL_COINS = SceneHandler.GetInstance().GetTotalCoins();
        UpdateTotalCoinsText();
    }
}
