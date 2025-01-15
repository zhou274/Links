#if EASY_MOBILE_PRO
//using EasyMobile;
#endif
using UnityEngine;

namespace GameTemplate.Scripts
{
    public class InAppPurchasesManagerScript : MonoBehaviour
    {
        public static InAppPurchasesManagerScript Instance { get; private set; }

        public void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void OnEnable()
        {            
#if EASY_MOBILE_PRO
            //InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
            //InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
#endif
        }

        public void OnDisable()
        {            
#if EASY_MOBILE_PRO
            // Unsubscribe when the game object is disabled
            //InAppPurchasing.PurchaseCompleted -= PurchaseCompletedHandler;
            //InAppPurchasing.PurchaseFailed -= PurchaseFailedHandler;
#endif
        }

        public void RemoveAds()
        {
#if EASY_MOBILE_PRO
            //InAppPurchasing.Purchase(IAPConstants.Product_Remove_Ads);
#endif
        }

        public void Purchase1000Gems()
        {
#if EASY_MOBILE_PRO
            //InAppPurchasing.Purchase(IAPConstants.Product_Buy_1000_Gems);
#endif
        }

        public void Purchase5000Gems()
        {
#if EASY_MOBILE_PRO
            //InAppPurchasing.Purchase(IAPConstants.Product_Buy_5000_Gems);
#endif
        }

        public void Purchase10KGems()
        {
#if EASY_MOBILE_PRO
            //InAppPurchasing.Purchase(IAPConstants.Product_Buy_10K_Gems);
#endif
        }

        public void UnlockEverything()
        {
#if EASY_MOBILE_PRO
            //InAppPurchasing.Purchase(IAPConstants.Product_Unlock_everything);
#endif
        }

#if EASY_MOBILE_PRO
        // Successful purchase handler
        //private void PurchaseCompletedHandler(IAPProduct product)
        //{
        //    Debug.Log("The purchase of product " + product.Name + " has completed.");

        //    // Compare product name to the generated name constants to determine which product was bought
        //    switch (product.Name)
        //    {
        //        case IAPConstants.Product_Remove_Ads:
        //            {
        //                AdsManagerScript.Instance.RemoveAds();
        //                DestroyImmediate(FeaturesManager.instance.inAppPurchasesOptions.removeAdsButton);
        //                break;
        //            }
        //        case IAPConstants.Product_Buy_1000_Gems:
        //            {
        //                CoinsManager.instance.AwardCoins(1000);
        //                break;
        //            }
        //        case IAPConstants.Product_Buy_5000_Gems:
        //            {
        //                CoinsManager.instance.AwardCoins(5000);
        //                break;
        //            }
        //        case IAPConstants.Product_Buy_10K_Gems:
        //            {
        //                CoinsManager.instance.AwardCoins(10000);
        //                break;
        //            }
        //        case IAPConstants.Product_Unlock_everything:
        //            {
        //                AdsManagerScript.Instance.RemoveAds();
        //                LevelsManagerScript.instance.UnlockAllLevels();

        //                // Remove the RemoveAds
        //                //DestroyImmediate(FeaturesManager.instance.inAppPurchasesOptions.removeAdsButton);

        //                // Remove all traces of the store from the UI
        //                DestroyImmediate(FeaturesManager.instance.inAppPurchasesOptions.storeButton);
        //                DestroyImmediate(FeaturesManager.instance.inAppPurchasesOptions.storePopup);

        //                break;
        //            }
        //    }
        }

        // Failed purchase handler
        //void PurchaseFailedHandler(IAPProduct product, string failureReason)
        //{
        //    Debug.Log("The purchase of product " + product.Name + " has failed: " + failureReason);
        //}
#endif
    }
