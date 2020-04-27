using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyExtraStageDialog : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private TextMeshProUGUI Distance;
    [SerializeField] private TextMeshProUGUI Coins;
    [SerializeField] private TextMeshProUGUI Nitro;
    [SerializeField] private TextMeshProUGUI SlowMotion;
    [SerializeField] private TextMeshProUGUI Inversion;

    [SerializeField] private Button BuyItBtn;
    [SerializeField] private Button CancelBtn;

    [HideInInspector] public bool isOpen = false;
    private string STAGE_NAME;
    public void Open(string stagename)
    {
        if(!isOpen)
        {
            STAGE_NAME = stagename;
            isOpen = true;
            InitInformations();
            GetComponent<Animator>().SetTrigger("open");
            BuyItBtn.onClick.AddListener(OnBuyStage);
            CancelBtn.onClick.AddListener(Close);
            EventHandler.onBuyExtraStageEvent += PlayStage;
        }
    }

    public void PlayStage()
    {
        EventHandler.onBuyExtraStageEvent -= PlayStage;
        LoadingBarScript.Instance.LoadScene(STAGE_NAME, "level");
    }
    
    private void OnBuyStage()
    {
        switch (STAGE_NAME)
        {
            case "Level01Stage05": Purchaser.Instance.BuyLevel01_Stage05(); break;
            case "Level01Stage06": Purchaser.Instance.BuyLevel01_Stage06(); break;
            case "Level01Stage07": Purchaser.Instance.BuyLevel01_Stage07(); break;

            case "Level02Stage05": Purchaser.Instance.BuyLevel02_Stage05(); break;
            case "Level02Stage06": Purchaser.Instance.BuyLevel02_Stage06(); break;
            case "Level02Stage07": Purchaser.Instance.BuyLevel02_Stage07(); break;

            case "Level03Stage05": Purchaser.Instance.BuyLevel03_Stage05(); break;
            case "Level03Stage06": Purchaser.Instance.BuyLevel03_Stage06(); break;
            case "Level03Stage07": Purchaser.Instance.BuyLevel03_Stage07(); break;

            default: Debug.Log("Buy Stage Dialog > StageName Not found"); break;
        }
        EventHandler.BuyExtraStage_TR();
        Close();
    }
    private void InitInformations()
    {
        stageName.text = ExtraStageInformation.GetName(STAGE_NAME);
        Distance.text = ExtraStageInformation.GetDistance(STAGE_NAME);
        Coins.text = ExtraStageInformation.GetCoins(STAGE_NAME);
        Nitro.text = ExtraStageInformation.GetNitro(STAGE_NAME);
        SlowMotion.text = ExtraStageInformation.GetSlowMotion(STAGE_NAME);
        Inversion.text = ExtraStageInformation.GetInverse(STAGE_NAME);
    }

    public void Close()
    {
        if(isOpen)
        {
            isOpen = false;
            GetComponent<Animator>().SetTrigger("close");
            EventHandler.onBuyExtraStageEvent -= PlayStage;
        }
    }

    public void onFadeOutComplete()
    {
        gameObject.SetActive(false);
    }

    public void SetDialogActive(bool Status)
    {
        gameObject.SetActive(Status);
    }
}







