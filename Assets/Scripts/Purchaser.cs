
#if UNITY_PURCHASING
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{
    public static Purchaser Instance { set; get; }
#if UNITY_ANDROID
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
#endif 
    public static string PRODUCT_NOADS = "noads";

    public static string PRODUCT_500COINS = "fivehundredcoins";
    public static string PRODUCT_1KCOINS = "onekcoins";
    public static string PRODUCT_2KCOINS = "twokcoins";
    public static string PRODUCT_3KCOINS = "threekcoins";
    public static string PRODUCT_5KCOINS = "fivekcoins";
    public static string PRODUCT_10KCOINS = "tenkcoins";

    public static string PRODUCT_LEVEL01STAGE05 = "level01_stage05";
    public static string PRODUCT_LEVEL01STAGE06 = "level01_stage06";
    public static string PRODUCT_LEVEL01STAGE07 = "level01_stage07";
    
    public static string PRODUCT_LEVEL02STAGE05 = "level02_stage05";
    public static string PRODUCT_LEVEL02STAGE06 = "level02_stage06";
    public static string PRODUCT_LEVEL02STAGE07 = "level02_stage07";

    public static string PRODUCT_LEVEL03STAGE05 = "level03_stage05";
    public static string PRODUCT_LEVEL03STAGE06 = "level03_stage06";
    public static string PRODUCT_LEVEL03STAGE07 = "level03_stage07";

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
#if UNITY_ANDROID
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
#endif
    }

    public void InitializePurchasing()
    {
#if UNITY_ANDROID
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(PRODUCT_500COINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_1KCOINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_2KCOINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_3KCOINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_5KCOINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_10KCOINS, ProductType.Consumable);
        
        builder.AddProduct(PRODUCT_NOADS, ProductType.NonConsumable);

        builder.AddProduct(PRODUCT_LEVEL01STAGE05, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL01STAGE06, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL01STAGE07, ProductType.NonConsumable);

        builder.AddProduct(PRODUCT_LEVEL02STAGE05, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL02STAGE06, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL02STAGE07, ProductType.NonConsumable);

        builder.AddProduct(PRODUCT_LEVEL03STAGE05, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL03STAGE06, ProductType.NonConsumable);
        builder.AddProduct(PRODUCT_LEVEL03STAGE07, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
#endif
    }


    private bool IsInitialized()
    {
#if UNITY_ANDROID
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
#endif
    }


    public void BuyLevel01_Stage05()
    {
        BuyProductID(PRODUCT_LEVEL01STAGE05);
    }

    public void BuyLevel01_Stage06()
    {
        BuyProductID(PRODUCT_LEVEL01STAGE06);
    }

    public void BuyLevel01_Stage07()
    {
        BuyProductID(PRODUCT_LEVEL01STAGE07);
    }


    public void BuyLevel02_Stage05()
    {
        BuyProductID(PRODUCT_LEVEL02STAGE05);
    }
    public void BuyLevel02_Stage06()
    {
        BuyProductID(PRODUCT_LEVEL02STAGE06);
    }
    public void BuyLevel02_Stage07()
    {
        BuyProductID(PRODUCT_LEVEL02STAGE07);
    }


    public void BuyLevel03_Stage05()
    {
        BuyProductID(PRODUCT_LEVEL03STAGE05);
    }
    public void BuyLevel03_Stage06()
    {
        BuyProductID(PRODUCT_LEVEL03STAGE06);
    }
    public void BuyLevel03_Stage07()
    {
        BuyProductID(PRODUCT_LEVEL03STAGE07);
    }


    public void BuyNoAds()
    {
        BuyProductID(PRODUCT_NOADS);
    }
    public void Buy500Coins()
    {
        BuyProductID(PRODUCT_500COINS);
    }
   
    public void Buy1KCoins()
    {
        BuyProductID(PRODUCT_1KCOINS);
    }
    public void Buy2KCoins()
    {
        BuyProductID(PRODUCT_2KCOINS);
    }
    public void Buy3KCoins()
    {
        BuyProductID(PRODUCT_3KCOINS);
    }
    public void Buy5KCoins()
    {
        BuyProductID(PRODUCT_5KCOINS);
    }

    public void Buy10KCoins()
    {
        BuyProductID(PRODUCT_10KCOINS);
    }
    void BuyProductID(string productId)
    {
#if UNITY_ANDROID
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
#endif
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
#if UNITY_ANDROID
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
#endif
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
#if UNITY_ANDROID
        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_NOADS, StringComparison.Ordinal))
        {
            Debug.Log("you buy no ads");
            EventHandler.PurchaseDone_TR("NO_ADS");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL01STAGE05, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 01 - Stage 05");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level01Stage05");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL01STAGE06, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 01 - Stage 06");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level01Stage06");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL01STAGE07, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 01 - Stage 07");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level01Stage07");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL02STAGE05, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 02 - Stage 05");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level02Stage05");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL02STAGE06, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 02 - Stage 06");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level02Stage06");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL02STAGE07, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 02 - Stage 07");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level02Stage07");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL03STAGE05, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 03 - Stage 05");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level03Stage05");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL03STAGE06, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 03 - Stage 06");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level03Stage06");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_LEVEL03STAGE07, StringComparison.Ordinal))
        {
            Debug.Log("you Unlock Level 03 - Stage 07");
            SceneHandler.GetInstance().Stages.BuyStageByName("Level03Stage07");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_500COINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 500 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(500);
            EventHandler.BuyCoins_TR();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_1KCOINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 1K - 1000 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(1000);
            EventHandler.BuyCoins_TR();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_2KCOINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 2K - 2000 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(2000);
            EventHandler.BuyCoins_TR();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_3KCOINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 3K - 3000 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(3000);
            EventHandler.BuyCoins_TR();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_5KCOINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 5K - 5000 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(5000);
            EventHandler.BuyCoins_TR();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_10KCOINS, StringComparison.Ordinal))
        {
            Debug.Log("you Buy 10K - 10000 Coins");
            SceneHandler.GetInstance().AddToTotalCoins(10000);
            EventHandler.BuyCoins_TR();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        
        return PurchaseProcessingResult.Complete;
#endif
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
#endif