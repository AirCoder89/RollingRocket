using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour {

    //GUI Components
    [SerializeField] private TextMeshProUGUI TotalCoinsTxt;
    [SerializeField] private GameObject BuyCoinsDialog;
    [SerializeField] private Button BackBtn;

    [SerializeField] private TextMeshProUGUI RotationSpeedPriceTxt;
    [SerializeField] private Button UpgradeRotationSpeedBtn;
    [SerializeField] private Image RotationSpeedCoinsIcon;
    [SerializeField] private Image RotationSpeedPBar;

    [SerializeField] private TextMeshProUGUI FuelConusmePriceTxt;
    [SerializeField] private Button UpgradeFuelConusmeBtn;
    [SerializeField] private Image FuelConusmeCoinsIcon;
    [SerializeField] private Image FuelConusmePBar;

    [SerializeField] private TextMeshProUGUI FuelTankPriceTxt;
    [SerializeField] private Button UpgradeFuelTankBtn;
    [SerializeField] private Image FuelTankCoinsIcon;
    [SerializeField] private Image FuelTankPBar;

    [SerializeField] private TextMeshProUGUI ThrustPriceTxt;
    [SerializeField] private Button UpgradeThrustBtn;
    [SerializeField] private Image ThrustCoinsIcon;
    [SerializeField] private Image ThrustPBar;

    [SerializeField] private TextMeshProUGUI MagnetPriceTxt;
    [SerializeField] private Button UpgradeMagnetBtn;
    [SerializeField] private Image MagnetCoinsIcon;
    [SerializeField] private Image MagnetPBar;

    [SerializeField] private TextMeshProUGUI SenstivityPriceTxt;
    [SerializeField] private Button UpgradeSenstivityBtn;
    [SerializeField] private Image SenstivityCoinsIcon;
    [SerializeField] private Image SenstivityPBar;

    [SerializeField] private Sprite PBarLevel2;
    [SerializeField] private Sprite PBarLevel3;
    [SerializeField] private Sprite PBarLevel4;

    public CoinsPacksScript CoinsPacks;
    //Private Declare
    private ShipData ship;
    private int TotalCoins;
    private int Price_RotationSpeed;
    private int Price_Thrust;
    private int Price_FuelConsume;
    private int Price_FuelTank;
    private int Price_Magnet;
    private int Price_Senstivity;
	void Start () {
        ship = new ShipData();
        CoinsPacks.SetDialogActive(false);
        EventHandler.onBuyCoinsEvent += UpdateTotalCoins;
        BackBtn.onClick.AddListener(onClickBACK);
        UpdateTotalCoins();
        UpdateGui();
	}
	
    private void BtnSoundFX()
    {

    }
    public void onClickBACK()
    {
        LoadingBarScript.Instance.LoadScene("MainMenu", "win");
    }
    private void UpdateTotalCoins()
    {
        TotalCoins = SceneHandler.GetInstance().GetTotalCoins();
        if(TotalCoins < 10)
        {
            TotalCoinsTxt.text = "000" + TotalCoins.ToString();
        }
        else if (TotalCoins >= 10 && TotalCoins < 100)
        {
            TotalCoinsTxt.text = "00" + TotalCoins.ToString();
        }
        else if (TotalCoins >= 100 && TotalCoins < 1000)
        {
            TotalCoinsTxt.text = "0" + TotalCoins.ToString();
        }
        else
        {
            TotalCoinsTxt.text = TotalCoins.ToString();
        }
    }

    private void UpdateGui()
    {
        UpdateSpeedRotationGUI();
        UpdateFuelConsumptionGUI();
        UpdateFuelTankGUI();
        UpdateThrustGUI();
        UpdateMagnetGUI();
        UpdateSenstivityGUI();
    }

    //-------------------- SENSTIVITY
    private void UpdateSenstivityGUI()
    {
        //current level
        int currentLevel = ship.GetLevelSenstivity(); //start from 0 to 3
        if (currentLevel == 1)
        {
            SenstivityPBar.sprite = PBarLevel2;
            this.UpgradeSenstivityBtn.onClick.AddListener(UpgradeSenstivity);
        }
        else if (currentLevel == 2)
        {
            SenstivityPBar.sprite = PBarLevel3;
            this.UpgradeSenstivityBtn.onClick.AddListener(UpgradeSenstivity);
        }
        else if (currentLevel == 3)
        {
            SenstivityPBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeSenstivityBtn.interactable = false;
            this.SenstivityCoinsIcon.enabled = false;
            this.SenstivityPriceTxt.enabled = false;
        }

        if (SenstivityPriceTxt.enabled)
        {
            SetCostSenstivity(currentLevel + 1);
        }
    }
    private void SetCostSenstivity(int Nextlevel)
    {
        switch (Nextlevel)
        {
            case 1: Price_Senstivity = 220; break; //level 02
            case 2: Price_Senstivity = 430; break; //level 03
            case 3: Price_Senstivity = 622; break; //level 04
        }

        this.SenstivityPriceTxt.text = Price_Senstivity.ToString();
    }

    public void UpgradeSenstivity()
    {
        SceneHandler.GetInstance().DebugLog("Click Senstivity");
        
        BtnSoundFX();
        if (TotalCoins >= Price_Senstivity)
        {
            int NextLevel = ship.GetLevelSenstivity() + 1;
            if (NextLevel <= 3)
            {
                ship.SetSenstivity(NextLevel);
                SceneHandler.GetInstance().buySomeThing(Price_Senstivity);
                UpdateTotalCoins();
                UpdateSenstivityGUI();
                UpgradeAnimation();
            }
        }
        else
        {
            MORE_COINS_DIALOG();
        }
    }
    //-------------------- MAGNET
    private void UpdateMagnetGUI()
    {
        //current level
        int currentLevel = ship.GetLevelMagnet(); //start from 0 to 3
        if (currentLevel == 1)
        {
            MagnetPBar.sprite = PBarLevel2;
            this.UpgradeMagnetBtn.onClick.AddListener(UpgradeMagnet);
        }
        else if (currentLevel == 2)
        {
            MagnetPBar.sprite = PBarLevel3;
            this.UpgradeMagnetBtn.onClick.AddListener(UpgradeMagnet);
        }
        else if (currentLevel == 3)
        {
            MagnetPBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeMagnetBtn.interactable = false;
            this.MagnetCoinsIcon.enabled = false;
            this.MagnetPriceTxt.enabled = false;
        }

        if (MagnetPriceTxt.enabled)
        {
            SetCostMagnet(currentLevel + 1);
        }

    }
    private void SetCostMagnet(int Nextlevel)
    {
        switch (Nextlevel)
        {
            case 1: Price_Magnet = 370; break; //level 02
            case 2: Price_Magnet = 599; break; //level 03
            case 3: Price_Magnet = 825; break; //level 04
        }

        this.MagnetPriceTxt.text = Price_Magnet.ToString();
    }

    private void UpgradeMagnet()
    {
        SceneHandler.GetInstance().DebugLog("Click Magnet");
       
        BtnSoundFX();
        if (TotalCoins >= Price_Magnet)
        {
            int NextLevel = ship.GetLevelMagnet() + 1;
            if (NextLevel <= 3)
            {
                ship.SetMagnet(NextLevel);
                SceneHandler.GetInstance().buySomeThing(Price_Magnet);
                UpdateTotalCoins();
                UpdateMagnetGUI();
                UpgradeAnimation();
            }
        }
        else
        {
            MORE_COINS_DIALOG();
        }
    }
    //-------------------- THRUST
    private void UpdateThrustGUI()
     {
         //current level
        int currentLevel = ship.GetLevelThrust(); //start from 0 to 3
        if (currentLevel == 1)
        {
            ThrustPBar.sprite = PBarLevel2;
            this.UpgradeThrustBtn.onClick.AddListener(UpgradeThrust);
        }
        else if (currentLevel == 2)
        {
            ThrustPBar.sprite = PBarLevel3;
            this.UpgradeThrustBtn.onClick.AddListener(UpgradeThrust);
        }
        else if (currentLevel == 3)
        {
            ThrustPBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeThrustBtn.interactable = false;
            this.ThrustPriceTxt.enabled = false;
            this.ThrustCoinsIcon.enabled = false;
            
        }
        if (ThrustPriceTxt.enabled)
        {
            SetCostThrust(currentLevel + 1);
        }

    }
     private void SetCostThrust(int Nextlevel)
     {
         switch (Nextlevel)
         {
             case 1: Price_Thrust = 415; break; //level 02
             case 2: Price_Thrust = 674; break; //level 03
             case 3: Price_Thrust = 939; break; //level 04
         }

         this.ThrustPriceTxt.text = Price_Thrust.ToString();
     }

     private void UpgradeThrust()
     {
        SceneHandler.GetInstance().DebugLog("Click Thrust");
        BtnSoundFX();
         if (TotalCoins >= Price_Thrust)
         {
             int NextLevel = ship.GetLevelThrust() + 1;
             if (NextLevel <= 3)
             {
                 ship.SetThrust(NextLevel);
                 SceneHandler.GetInstance().buySomeThing(Price_Thrust);
                 UpdateTotalCoins();
                 UpdateThrustGUI();
                 UpgradeAnimation();
             }
         }
         else
         {
             MORE_COINS_DIALOG();
         }
     }
     //-------------------- FUEL TANK
     private void UpdateFuelTankGUI()
     {
         //current level
         int currentLevel = ship.GetLevelFuelTank(); //start from 0 to 3
        if (currentLevel == 1)
        {
            FuelTankPBar.sprite = PBarLevel2;
            this.UpgradeFuelTankBtn.onClick.AddListener(UpgradeFuelTank);
        }
        else if (currentLevel == 2)
        {
            FuelTankPBar.sprite = PBarLevel3;
            this.UpgradeFuelTankBtn.onClick.AddListener(UpgradeFuelTank);
        }
        else if (currentLevel == 3)
        {
            FuelTankPBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeFuelTankBtn.interactable = false;
            this.FuelTankCoinsIcon.enabled = false;
            this.FuelTankPriceTxt.enabled = false;
        }

        if (FuelTankPriceTxt.enabled)
        {
            SetCostFuelTank(currentLevel + 1);
        }

    }
     private void SetCostFuelTank(int Nextlevel)
     {
         switch (Nextlevel)
         {
             case 1: Price_FuelTank = 320; break; //level 02
             case 2: Price_FuelTank = 550; break; //level 03
             case 3: Price_FuelTank = 899; break; //level 04
         }

         this.FuelTankPriceTxt.text = Price_FuelTank.ToString();
     }

     private void UpgradeFuelTank()
     {
        SceneHandler.GetInstance().DebugLog("Click FuelTank");
        
        BtnSoundFX();
         if (TotalCoins >= Price_FuelTank)
         {
             int NextLevel = ship.GetLevelFuelTank() + 1;
             if (NextLevel <= 3)
             {
                 ship.SetFuelTank(NextLevel);
                 SceneHandler.GetInstance().buySomeThing(Price_FuelTank);
                 UpdateTotalCoins();
                 UpdateFuelTankGUI();
                 UpgradeAnimation();
             }
         }
         else
         {
             MORE_COINS_DIALOG();
         }
     }
     //-------------------- FUEL CONSUMPTION
     private void UpdateFuelConsumptionGUI()
     {
         //current level
         int currentLevel = ship.GetLevelFuelConsume(); //start from 0 to 3
        if (currentLevel == 1)
        {
            FuelConusmePBar.sprite = PBarLevel2;
            this.UpgradeFuelConusmeBtn.onClick.AddListener(UpgradeFuelConsume);
        }
        else if (currentLevel == 2)
        {
            FuelConusmePBar.sprite = PBarLevel3;
            this.UpgradeFuelConusmeBtn.onClick.AddListener(UpgradeFuelConsume);
        }
        else if (currentLevel == 3)
        {
            FuelConusmePBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeFuelConusmeBtn.interactable = false;
            this.FuelConusmeCoinsIcon.enabled = false;
            this.FuelConusmePriceTxt.enabled = false;
        }

        if (FuelConusmePriceTxt.enabled)
        {
            SetCostFuelConsumption(currentLevel + 1);
        }
    }

     private void SetCostFuelConsumption(int Nextlevel)
     {
         switch (Nextlevel)
         {
             case 1: Price_FuelConsume = 350; break; //level 02
             case 2: Price_FuelConsume = 700; break; //level 03
             case 3: Price_FuelConsume = 950; break; //level 04
         }

         this.FuelConusmePriceTxt.text = Price_FuelConsume.ToString();
     }

     private void UpgradeFuelConsume()
     {
        SceneHandler.GetInstance().DebugLog("Click FuelConsume");
        
        BtnSoundFX();
         if (TotalCoins >= Price_FuelConsume)
         {
             int NextLevel = ship.GetLevelFuelConsume() + 1;
             if (NextLevel <= 3)
             {
                 ship.SetFuelConsume(NextLevel);
                 SceneHandler.GetInstance().buySomeThing(Price_FuelConsume);
                 UpdateTotalCoins();
                 UpdateFuelConsumptionGUI();
                 UpgradeAnimation();
             }
         }
         else
         {
             this.UpgradeFuelConusmeBtn.onClick.RemoveAllListeners();
             MORE_COINS_DIALOG();
         }
     }
     //-------------------- SPEED ROTATION
     private void UpdateSpeedRotationGUI()
     {
        //current level
        int currentLevel = ship.GetLevelSpeedRotation(); //start from 0 to 3
        
        if (currentLevel == 1)
        {
            RotationSpeedPBar.sprite = PBarLevel2;
            this.UpgradeRotationSpeedBtn.onClick.AddListener(UpgradeRotationSpeed);
        }
        else if (currentLevel == 2)
        {
            RotationSpeedPBar.sprite = PBarLevel3;
            this.UpgradeRotationSpeedBtn.onClick.AddListener(UpgradeRotationSpeed);
        }
        else if (currentLevel == 3)
        {
            RotationSpeedPBar.sprite = PBarLevel4;
            //Disable Upgrade btn
            this.UpgradeRotationSpeedBtn.interactable = false;
            this.RotationSpeedCoinsIcon.enabled = false;
            this.RotationSpeedPriceTxt.enabled = false;
        }

        if (RotationSpeedPriceTxt.enabled)
        {
            SetCostRotationSpeed(currentLevel + 1);
        }

    }

    private void SetCostRotationSpeed(int Nextlevel)
     {
         switch(Nextlevel)
         {
             case 1: Price_RotationSpeed = 350; break; //level 02
             case 2: Price_RotationSpeed = 640; break; //level 03
             case 3: Price_RotationSpeed = 862; break; //level 04
         }

         this.RotationSpeedPriceTxt.text = Price_RotationSpeed.ToString();
     }

     private void UpgradeRotationSpeed()
     {
        SceneHandler.GetInstance().DebugLog("Click RotationSpeed");
        BtnSoundFX();
         if (TotalCoins >= Price_RotationSpeed)
         {
             int NextLevel = ship.GetLevelSpeedRotation() + 1;
             if(NextLevel <= 3)
             {
                 ship.SetSpeedRotation(NextLevel);
                 SceneHandler.GetInstance().buySomeThing(Price_RotationSpeed);
                 UpdateTotalCoins();
                 UpdateSpeedRotationGUI();
                 UpgradeAnimation();
             }
         }
         else
         {
             MORE_COINS_DIALOG();
         }
     }

  
    //------------------------------------------------------------------------
    //------------------------------------------------------------------------
    private void UpgradeAnimation()
    {

    }
    public void MORE_COINS_DIALOG()
    {
        CoinsPacks.SetDialogActive(true);
        CoinsPacks.Open();
    }
}
