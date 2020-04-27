using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsPacksScript : MonoBehaviour {

    [SerializeField] private CongratsMessage Congrats;
    [SerializeField] private Button CloseMetn;
    [SerializeField] private Button[] CoinsPacks;
    public bool isOpen = false;
    private int SelectedOffre;
	// 500 / 1k / 2k / 3k / 5k / 10k
	public void Open()
    {
        if(!isOpen)
        {
            EventHandler.onBuyCoinsEvent += PurchaseComplete;
            isOpen = true;
            GetComponent<Animator>().SetTrigger("open");
            for (int i = 0; i < CoinsPacks.Length; i++)
            {
                var i2 = i;
                CoinsPacks[i].onClick.AddListener(delegate { BuyCoinsPack(i2); });
            }
            CloseMetn.onClick.AddListener(Close);
        }
    }

    public void PurchaseComplete()
    {
        Congrats.OpenCongratsIAPCoins(SelectedOffre);
    }
    public void BuyCoinsPack(int PackIndex)
    {
       if(PackIndex == 0)
        {
            //500 coins
            SelectedOffre = 500;
            Purchaser.Instance.Buy500Coins();
            Close();
        }
        else if (PackIndex == 1)
        {
            //1k coins
            SelectedOffre = 1000;
            Purchaser.Instance.Buy1KCoins();
            Close();
        }
        else if (PackIndex == 2)
        {
            //2k coins
            SelectedOffre = 2000;
            Purchaser.Instance.Buy2KCoins();
            Close();
        }
        else if (PackIndex == 3)
        {
            //3k coins
            SelectedOffre = 3000;
            Purchaser.Instance.Buy3KCoins();
            Close();
        }
        else if (PackIndex == 4)
        {
            //5k coins
            SelectedOffre = 5000;
            Purchaser.Instance.Buy5KCoins();
            Close();
        }
        else if (PackIndex == 5)
        {
            //10k coins
            SelectedOffre = 10000;
            Purchaser.Instance.Buy10KCoins();
            Close();
        }
    }

    public void SetDialogActive(bool status)
    {
        gameObject.SetActive(status);
    }
    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            for(int i=0; i<CoinsPacks.Length; i++)
            {
                CoinsPacks[i].onClick.RemoveAllListeners();
            }
            GetComponent<Animator>().SetTrigger("close");
        }
    }
    public void FadeOutCompelte()
    {
        gameObject.SetActive(false);
    }
	
}
