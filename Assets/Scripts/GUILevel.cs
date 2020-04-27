using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILevel : MonoBehaviour {

    [SerializeField] private string LevelName;
    public int UnlockPrice;
    [SerializeField] private Button Level01Stage01Btn;
    [SerializeField] private Button Level01Stage02Btn;
    [SerializeField] private Button Level01Stage03Btn;
    [SerializeField] private Button Level01Stage04Btn;
    [SerializeField] private Button Level01Stage05Btn;
    [SerializeField] private Button Level01Stage06Btn;
    [SerializeField] private Button Level01Stage07Btn;

    [SerializeField] private Sprite SPExtra05;
    [SerializeField] private Sprite SPExtra06;
    [SerializeField] private Sprite SPExtra07;

    [SerializeField] private GameObject LockedPanel;
    [SerializeField] private Sprite LockedBackground;

    [SerializeField] private GameObject UnlockedPanel;
    [SerializeField] private Sprite Unlockedackground;

    [SerializeField] private Button UnlockLevelBtn;

    [HideInInspector] public bool isLocked;
    private bool isOpened;
    private Level level;
    private int TOTAL_COINS;
    private void Start()
    {
        isOpened = SceneHandler.GetInstance().Stages.isLevelActive(LevelName);
        TOTAL_COINS = SceneHandler.GetInstance().GetTotalCoins();
        //SceneHandler.GetInstance().Stages.Level02.Stages[5].SetNotBuyed();
    }
    public void FadeOutComplete()
    {
        gameObject.SetActive(false);
    }

    public void LockLevel()
    {
        isLocked = true;
        LockedPanel.SetActive(true);
        UnlockedPanel.SetActive(false);
        GetComponent<Image>().sprite = LockedBackground;
        //------ btn management
        UnlockLevelBtn.onClick.AddListener(UnlockLevelRequest);
    }
    private void UnlockLevelRequest()
    {
        TOTAL_COINS = SceneHandler.GetInstance().GetTotalCoins();

        if (TOTAL_COINS >= UnlockPrice && isLocked)
        {
            SceneHandler.GetInstance().buySomeThing(UnlockPrice);
           
            SceneHandler.GetInstance().Stages.UnlockLevelByName(LevelName, true);
            //init level panel
            switch(LevelName)
            {
                case "Level02":
                    UnlockLevel(SceneHandler.GetInstance().Stages.Level02); break;
                case "Level03":
                    UnlockLevel(SceneHandler.GetInstance().Stages.Level03); break;

            }

            //add some animation !! weleey !
            EventHandler.BuyCoins_TR();
        }
        else
        {
            LevelSelectGUIHandler.Instance.ShowBuyCoinsDialog();
        }
    }

    public void UnlockLevel(Level L)
    {
        isLocked = false;
        LockedPanel.SetActive(false);
        UnlockedPanel.SetActive(true);
        GetComponent<Image>().sprite = Unlockedackground;
        //------ btn management
        this.level = L;
        
        UpdateStages();
    }

    public void UpdateStages()
    {
        Level01Stage01Btn.interactable = !level.Stages[0].GetIsLocked();
        Level01Stage01Btn.onClick.AddListener(delegate { PlayStage(level.Stages[0]); });

        Level01Stage02Btn.interactable = !level.Stages[1].GetIsLocked();
        Level01Stage02Btn.onClick.AddListener(delegate { PlayStage(level.Stages[1]); });

        Level01Stage03Btn.interactable = !level.Stages[2].GetIsLocked();
        Level01Stage03Btn.onClick.AddListener(delegate { PlayStage(level.Stages[2]); });

        Level01Stage04Btn.interactable = !level.Stages[3].GetIsLocked();
        Level01Stage04Btn.onClick.AddListener(delegate { PlayStage(level.Stages[3]); });

        //E X T R A
        if (level.Stages[4].GetIsExtra() && level.Stages[4].GetIsBuyed())
        {
            Level01Stage05Btn.GetComponent<Image>().sprite = SPExtra05;
        }
        Level01Stage05Btn.interactable = !level.Stages[4].GetIsLocked();
        Level01Stage05Btn.onClick.AddListener(delegate { PlayStage(level.Stages[4]); });


        if (level.Stages[5].GetIsExtra() && level.Stages[5].GetIsBuyed())
        {
            Level01Stage06Btn.GetComponent<Image>().sprite = SPExtra06;
        }
        Level01Stage06Btn.interactable = !level.Stages[5].GetIsLocked();
        Level01Stage06Btn.onClick.AddListener(delegate { PlayStage(level.Stages[5]); });

        if (level.Stages[6].GetIsExtra() && level.Stages[6].GetIsBuyed())
        {
            Level01Stage07Btn.GetComponent<Image>().sprite = SPExtra07;
        }
        Level01Stage07Btn.interactable = !level.Stages[6].GetIsLocked();
        Level01Stage07Btn.onClick.AddListener(delegate { PlayStage(level.Stages[6]); });
    }

    public void PlayStage(Stage stage)
    {
        print(stage.StageName);
        if (!stage.GetIsExtra())
        {
            LoadingBarScript.Instance.LoadScene(stage.StageName, "level");
        }
        else
        {
            //Extra stage
            if (stage.GetIsBuyed())
            {
                LoadingBarScript.Instance.LoadScene(stage.StageName, "level");
            }
            else
            {
                //extra stage not buyed yet
                //show Dialog buy
               LevelSelectGUIHandler.Instance.ShowBuyStageDialog(stage);
            }
        }
    }
}
