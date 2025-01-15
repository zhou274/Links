#if EASY_MOBILE_PRO || EASY_MOBILE
//using EasyMobile;
#endif
using UnityEngine;

namespace GameTemplate.Scripts
{
    public class EasyMobileProManagerScript : MonoBehaviour
    {
        private string m_path;

        void Awake()
        {
#if EASY_MOBILE_PRO || EASY_MOBILE
//            if (!RuntimeManager.IsInitialized())
//            {
//                RuntimeManager.Init();

//#if EASY_MOBILE_PRO
//                if ((FeaturesManager.instance.adsOptions.supportBanner ||
//                     FeaturesManager.instance.adsOptions.supportInterstitials) &&
//                    !Advertising.AdMobClient.IsInitialized)
//                {
//                    if (Advertising.AdMobClient.IsSdkAvail)
//                    {
//                        Debug.Log("Initializing AdMob client");
//                        Advertising.AdMobClient.Init();
//                    }
//                }

//                if (FeaturesManager.instance.gameServicesOptions.supportAchievements ||
//                    FeaturesManager.instance.gameServicesOptions.supportLeaderboard)
//                {
//                    //if (!GameServices.IsInitialized())
//                    {
//                        GameServices.UserLoginFailed += GameServices_UserLoginFailed;
//                        GameServices.UserLoginSucceeded += GameServices_UserLoginSucceeded;
//                        //GameServices.ManagedInit();
//                    }
//                }
//#endif
//            }
#endif
        }

        private void GameServices_UserLoginSucceeded()
        {
            Debug.Log("User login succeeded");
        }

        private void GameServices_UserLoginFailed()
        {
            Debug.LogError("User login failed");
        }

        public void ShowAchievementsUI()
        {
            if (FeaturesManager.instance.gameServicesOptions.supportAchievements)
            {
//#if EASY_MOBILE_PRO
//                if (GameServices.IsInitialized())
//                {
//                    GameServices.ShowAchievementsUI();
//                }
//                else
//                {
//#if UNITY_ANDROID
//                    GameServices.ManagedInit();    // start a new initialization process
//#elif UNITY_IOS
//                    Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
//#endif
//                }
//#endif
            }
        }

        public void RequestReview()
        {
            if (FeaturesManager.instance.gameServicesOptions.supportReviews)
            {
#if EASY_MOBILE_PRO
                //EasyMobile.StoreReview.RequestRating();
#endif
            }
        }

        public void CaptureScreen()
        {
            if (FeaturesManager.instance.gameServicesOptions.supportSharing)
            {
#if EASY_MOBILE
                //m_path = Sharing.SaveScreenshot("screenshot");
#endif
            }
        }

        public void ShareScreen()
        {
            if (FeaturesManager.instance.gameServicesOptions.supportSharing)
            {
#if EASY_MOBILE
                // Share the image with the path, a sample message and an empty subject
                //Sharing.ShareImage(m_path, FeaturesManager.instance.gameServicesOptions.sharingMessage);
#endif
            }
        }
    }
}