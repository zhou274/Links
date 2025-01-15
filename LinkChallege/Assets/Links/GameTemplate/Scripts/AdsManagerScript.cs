#if EASY_MOBILE_PRO
//using EasyMobile;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace GameTemplate.Scripts
{
    public class AdsManagerScript : MonoBehaviour
    {
        public static AdsManagerScript Instance { get; private set; }

#if EASY_MOBILE_PRO
        //public UnityEvent<InterstitialAdNetwork, AdPlacement> InterstitialAdCompleted = new UnityEvent<InterstitialAdNetwork, AdPlacement>();

        //public UnityEvent<RewardedAdNetwork, AdPlacement> RewardedAdSkipped = new UnityEvent<RewardedAdNetwork, AdPlacement>();

        //public UnityEvent<RewardedAdNetwork, AdPlacement> RewardedAdCompleted = new UnityEvent<RewardedAdNetwork, AdPlacement>();
#endif

        public int gamesPerInterstitial = 3;

        private bool m_bannerShown;
        private int m_gameCount = 0;

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

        void OnEnable()
        {
#if EASY_MOBILE_PRO
            //Advertising.AdsRemoved += AdsRemovedHandler;
#endif
        }

        public void Start()
        {
#if EASY_MOBILE_PRO
            if (!FeaturesManager.instance.adsOptions.supportBanner)
            {
                //m_bannerShown = !Advertising.IsAdRemoved();
            }
            else
            {
#if EM_ADMOB
                //Advertising.AdMobClient.OnBannerAdLoaded += AdMobClient_OnBannerAdLoaded;
                //Advertising.AdMobClient.OnBannerAdFailedToLoad += AdMobClient_OnBannerAdFailedToLoad;
                //Advertising.AdMobClient.OnBannerAdOpening += AdMobClient_OnBannerAdOpening;
#endif
            }

            if (FeaturesManager.instance.adsOptions.supportInterstitials)
            {
                //Advertising.InterstitialAdCompleted += (network, placement) => InterstitialAdCompleted.Invoke(network, placement);
                //Advertising.RewardedAdSkipped += (network, placement) => RewardedAdSkipped.Invoke(network, placement);
                //Advertising.RewardedAdCompleted += (network, placement) => RewardedAdCompleted.Invoke(network, placement);
            }
#endif
        }

        public void Update()
        {
#if EASY_MOBILE_PRO
            //if (!m_bannerShown &&
            //    FeaturesManager.instance.adsOptions.supportBanner &&
            //    Advertising.AdMobClient.IsInitialized)
            //{
            //    m_bannerShown = true;
            //    Advertising.ShowBannerAd(BannerAdPosition.Bottom, BannerAdSize.IABBanner);
            //}
#endif
        }

        public void RemoveAds()
        {
#if EASY_MOBILE_PRO
            //Advertising.RemoveAds();

            FeaturesManager.instance.mainContainer.offsetMin = new Vector2(FeaturesManager.instance.mainContainer.offsetMin.x, 0);
#endif
        }

        public void ShowInterstitialAd()
        {
#if EASY_MOBILE_PRO
            //if (Advertising.IsAdRemoved())
            //{
            //    return;
            //}

            //m_gameCount++;
            //if (m_gameCount >= gamesPerInterstitial)
            //{
            //    m_gameCount = 0;
            //    Advertising.ShowInterstitialAd();
            //}
#endif
        }

        public void ShowRewardedAd()
        {
#if EASY_MOBILE_PRO
            //Advertising.ShowRewardedAd();
#endif
        }

#if EASY_MOBILE_PRO
        private void AdMobClient_OnBannerAdLoaded(object sender, System.EventArgs e)
        {
            Debug.Log("Banner ad loaded");
        }

        //private void AdMobClient_OnBannerAdFailedToLoad(object sender, GoogleMobileAds.Api.AdFailedToLoadEventArgs e)
        //{
        //    Debug.LogError("Failed loading Banner ad: " + e.Message);
        //}

        private void AdMobClient_OnBannerAdOpening(object sender, System.EventArgs e)
        {
            Debug.Log("Banner ad clicked");
        }
#endif

        void AdsRemovedHandler()
        {
            Debug.Log("Ads were removed.");

#if EASY_MOBILE_PRO
            //Advertising.AdsRemoved -= AdsRemovedHandler;
#endif
        }
    }
}