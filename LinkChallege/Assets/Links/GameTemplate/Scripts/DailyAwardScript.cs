using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    [RequireComponent(typeof(Button))]
    public class DailyAwardScript : MonoBehaviour
    {
        public Sprite disabled;
        public Sprite enabled;

        public Image giftImage;
        public Text remainintTimeText;

        public int coinsToGrant;
        public TimeSpan timeBetweenGrants = TimeSpan.FromHours(12);

        private DateTime lastTimeGranted = DateTime.UtcNow;
        private Button button;
        private Animation giftAnimation;
        private bool giftAvailable;
        private DateTime m_earliestNextGrant;
        private TimeSpan lastAvailableIn;

        public void Start()
        {
            button = GetComponent<Button>();
            giftAnimation = GetComponent<Animation>();

            button.onClick.AddListener(OnClick);

            var defaultValue = DateTime.MinValue.ToString();
            var lastTimeGrantedGiftStr = PlayerPrefs.GetString(PlayerPrefsConsts.Player.LastDailyAward, defaultValue);
            if (lastTimeGrantedGiftStr == defaultValue)
            {
                PlayerPrefs.SetString(PlayerPrefsConsts.Player.LastDailyAward, lastTimeGrantedGiftStr);
            }

            lastTimeGranted = DateTime.Parse(lastTimeGrantedGiftStr);

            m_earliestNextGrant = lastTimeGranted.Add(timeBetweenGrants);

        }

        public void Update()
        {
            if (m_earliestNextGrant < DateTime.UtcNow)
            {
                if (!giftAvailable)
                {
                    if (lastTimeGranted == DateTime.MinValue)
                    {
                        PlayerPrefs.SetString(PlayerPrefsConsts.Player.LastDailyAward, DateTime.MinValue.ToString());
                    }
                    else
                    {
                        PlayerPrefs.SetString(PlayerPrefsConsts.Player.LastDailyAward, (lastTimeGranted - timeBetweenGrants).ToString());
                    }
                    giftAvailable = true;
                }

                // We can grant gift
                //giftImage.sprite = enabled;
                if (remainintTimeText != null)
                {
                    remainintTimeText.text = "Collect your reward";
                    //remainintTimeText.gameObject.SetActive(false);
                }
                button.interactable = true;

                if (giftAnimation != null && !giftAnimation.isPlaying)
                {
                    giftAnimation.Play();
                }

            }
            else
            {
                var availableIn = m_earliestNextGrant - DateTime.UtcNow;

                if (lastAvailableIn.TotalSeconds != availableIn.TotalSeconds)
                {
                    lastAvailableIn = availableIn;
                    //giftImage.sprite = enabled;
                    if (!remainintTimeText.gameObject.activeSelf)
                    {
                        remainintTimeText.gameObject.SetActive(true);
                    }

                    remainintTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", availableIn.Hours, availableIn.Minutes, availableIn.Seconds);
                    if (button.interactable)
                    {
                        button.interactable = false;

                        if (giftAnimation != null && giftAnimation.isPlaying)
                        {
                            giftAnimation.Stop();
                        }
                    }
                }
            }
        }

        public void OnClick()
        {
            lastTimeGranted = DateTime.UtcNow;
            m_earliestNextGrant = lastTimeGranted.Add(timeBetweenGrants);

            PlayerPrefs.SetString(PlayerPrefsConsts.Player.LastDailyAward, lastTimeGranted.ToString());
            giftAvailable = false;
        }
    }
}