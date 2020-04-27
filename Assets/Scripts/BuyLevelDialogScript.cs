using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyLevelDialogScript : MonoBehaviour {

    public static bool isOpen = false;
    [SerializeField] private TextMeshProUGUI DialogPriceText;
    [SerializeField] private Button OKBtn;
    [SerializeField] private Button CancelBtn;
    [SerializeField] private Button CloseBtn;
    private string StageName;
   
    public void Show(string price,string stagename)
    {
        if(!isOpen)
        {
            GetComponent<Animator>().SetTrigger("open");
            this.StageName = stagename;
            isOpen = true;
            DialogPriceText.text = price;
            OKBtn.onClick.AddListener(onClickOKey);
            CancelBtn.onClick.AddListener(CloseMe);
            CloseBtn.onClick.AddListener(CloseMe);
        }
        
    }

    public void onClickOKey()
    {
        switch(this.StageName)
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
        CloseMe();
    }

    public void CloseMe()
    {
        GetComponent<Animator>().SetTrigger("close");
    }
    public void onExit()
    {
        if(isOpen)
        {
            isOpen = false;
            this.gameObject.SetActive(false);
        }
    }
}
