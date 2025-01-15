using System;
#if EASY_MOBILE_PRO
//using EasyMobile;
#endif
using UnityEngine;

namespace GameTemplate.Scripts
{
    public class FeaturesManager : MonoBehaviour
    {
        public static FeaturesManager instance;

        public RectTransform mainContainer;

        public GameOptions gameOptions;
        public CollectionsAndLevelsOptions collectionsAndLevelsOptions;
        public InAppPurchasesOptions inAppPurchasesOptions;
        public GameServicesOptions gameServicesOptions;
        public CoinsOptions coinsOptions;
        public SettingsOptions settings;
        public AdsOptions adsOptions;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        public void Start()
        {
            if (!gameOptions.supportDirectPlay)
            {
                DestroyImmediate(gameOptions.playButton);
            }

            if (!collectionsAndLevelsOptions.supportCollections)
            {
                DestroyImmediate(collectionsAndLevelsOptions.collectionsButton);
                DestroyImmediate(collectionsAndLevelsOptions.collectionsPopup);
            }
            else
            {
                // Enabling Collections disables Levels
                DestroyImmediate(collectionsAndLevelsOptions.levelsButton);
            }

            if (!collectionsAndLevelsOptions.supportLevels)
            {
                DestroyImmediate(collectionsAndLevelsOptions.levelsButton);
                DestroyImmediate(collectionsAndLevelsOptions.levelsPopup);
            }

//            if (!inAppPurchasesOptions.supportStore
//#if EASY_MOBILE_PRO
//                || InAppPurchasing.IsProductOwned(IAPConstants.Product_Unlock_everything)
//#endif
//                )
//            {
//                DestroyImmediate(inAppPurchasesOptions.storeButton);
//                DestroyImmediate(inAppPurchasesOptions.storePopup);
//            }
//            else
//            {
//#if EASY_MOBILE_PRO
//                if (Advertising.IsAdRemoved())
//                {
//                    DestroyImmediate(inAppPurchasesOptions.removeAdsButton);
//                }
//#endif
//            }

            if (!gameServicesOptions.supportLeaderboard)
            {
                DestroyImmediate(gameServicesOptions.leaderboardButton);
            }

            if (!gameServicesOptions.supportAchievements)
            {
                DestroyImmediate(gameServicesOptions.achievementsButton);
            }

            if (!gameServicesOptions.supportSharing)
            {
                DestroyImmediate(gameServicesOptions.shareButton);
            }

            if (!coinsOptions.supportCoins)
            {
                DestroyImmediate(coinsOptions.coinsCounter);
                coinsOptions.supportDailyRewards = false;
            }

            if (!coinsOptions.supportDailyRewards)
            {
                DestroyImmediate(coinsOptions.dailyRewardsButton);
                DestroyImmediate(coinsOptions.dailyRewardsPopup);
            }

            if (!settings.supportSettings)
            {
                DestroyImmediate(settings.settingsButton);
                DestroyImmediate(settings.settingsInGameButton);
                DestroyImmediate(settings.settingsPopup);
            }

            if (!settings.supportSettingsSounds)
            {
                DestroyImmediate(settings.settingsSoundsToggle);
            }
            if (!settings.supportSettingsMusic)
            {
                DestroyImmediate(settings.settingsMusicToggle);
            }
            if (!settings.supportSettingsVibrate)
            {
                DestroyImmediate(settings.settingsVibrateToggle);
            }
        }
    }

    [Serializable]
    public class GameOptions
    {
        [Header("Play")]
        public bool supportDirectPlay;
        public GameObject playButton;
    }

    [Serializable]
    public class CollectionsAndLevelsOptions
    {
        [Header("Collections & Levels")]
        public bool supportCollections;
        public GameObject collectionsButton;
        public GameObject collectionsPopup;
        public bool supportLevels;
        public GameObject levelsButton;
        public GameObject levelsPopup;
    }

    [Serializable]
    public class InAppPurchasesOptions
    {
        [Header("Store")]
        public bool supportStore;
        public GameObject storeButton;
        public GameObject storePopup;

        public GameObject removeAdsButton;
    }

    [Serializable]
    public class GameServicesOptions
    {
        [Header("Leaderboard")]
        public bool supportLeaderboard;
        public GameObject leaderboardButton;

        [Header("Achievements")]
        public bool supportAchievements;
        public GameObject achievementsButton;

        [Header("Reviews")]
        public bool supportReviews;

        [Header("Sharing")]
        public bool supportSharing;
        public GameObject shareButton;
        public string sharingMessage;
    }

    [Serializable]
    public class CoinsOptions
    {
        [Header("Coins")]
        public bool supportCoins;
        public GameObject coinsCounter;

        [Header("Coins\\Daily Rewards")]
        public bool supportDailyRewards;
        public GameObject dailyRewardsButton;
        public GameObject dailyRewardsPopup;
    }

    [Serializable]
    public class SettingsOptions
    {
        [Header("Settings")]
        public bool supportSettings;
        public GameObject settingsButton;
        public GameObject settingsInGameButton;
        public GameObject settingsPopup;

        [Header("Settings/Sounds")]
        public bool supportSettingsSounds;
        public GameObject settingsSoundsToggle;
        [Header("Settings/Music")]
        public bool supportSettingsMusic;
        public GameObject settingsMusicToggle;
        [Header("Settings/Vibrate")]
        public bool supportSettingsVibrate;
        public GameObject settingsVibrateToggle;
    }

    [Serializable]
    public class AdsOptions
    {
        [Header("Banner")]
        public bool supportBanner;

        //public BannerAdPosition bannerPosition = BannerAdPosition.Bottom;

        [Header("Interstitial")]
        public bool supportInterstitials;
    }
}