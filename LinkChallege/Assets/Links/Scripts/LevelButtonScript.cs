using GameTemplate.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Links.Scripts
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class LevelButtonScript : GameTemplate.Scripts.LevelButtonScript
    {
        public Image star1;
        public Image star2;
        public Image star3;

        public Sprite goldStar;
        public Sprite silverStar;

        public Image lockImage;
        public Text levelName;

        public override void SetCompletedState()
        {
            var levelCompletionState = (LevelCompletionState) PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex));

            var button = GetComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;

            var image = GetComponent<Image>();

            lockImage = lockImage ?? gameObject.transform.Find("Lock").GetComponentInChildren<Image>();
            levelName = levelName ?? GetComponentInChildren<Text>();

            levelName.color = ThemeManager.instance.levelTextColor;

            switch (levelCompletionState)
            {
                case LevelCompletionState.NotAvailable:
                    {
                        if (ThemeManager.instance.levelNotAvailable != null)
                        {
                            image.sprite = ThemeManager.instance.levelNotAvailable;
                        }
                        else
                        {
                            image.color = Color.red;
                        }

                        if (!ThemeManager.instance.showNotAvailableLevelName)
                        {
                            levelName.gameObject.SetActive(false);
                        }
                        lockImage.gameObject.SetActive(true);
                        button.interactable = false;

                        if (ThemeManager.instance.levelNotAvailableColors != null)
                        {
                            button.colors = ThemeManager.instance.levelNotAvailableColors;
                        }
                        break;
                    }
                case LevelCompletionState.Available:
                    {
                        if (ThemeManager.instance.levelAvailable != null)
                        {
                            image.sprite = ThemeManager.instance.levelAvailable;
                        }
                        else
                        {
                            image.color = Color.yellow;
                        }
                        lockImage.gameObject.SetActive(false);
                        button.interactable = true;

                        if (ThemeManager.instance.levelAvailableColors != null)
                        {
                            button.colors = ThemeManager.instance.levelAvailableColors;
                        }
                        break;
                    }
                case LevelCompletionState.Completed:
                    {
                        if (ThemeManager.instance.levelCompleted != null)
                        {
                            image.sprite = ThemeManager.instance.levelCompleted;
                        }
                        else
                        {
                            image.color = Color.green;
                        }
                        lockImage.gameObject.SetActive(false);
                        button.interactable = true;

                        if (ThemeManager.instance.levelCompletedColors != null)
                        {
                            button.colors = ThemeManager.instance.levelCompletedColors;
                        }
                        break;
                    }
            }

            if (goldStar != null &&
                silverStar != null &&
                star1 != null &&
                star2 != null &&
                star3 != null && 
                levelCompletionState != LevelCompletionState.NotAvailable)
            {
                var stars = PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.Stars.FormattedWith(collectionIndex, levelIndex), 0);
                SetStar(star1, stars, 1);
                SetStar(star2, stars, 2);
                SetStar(star3, stars, 3);
            }
            else
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
        }

        private void SetStar(Image star, int stars, int requiredStars)
        {
            star.gameObject.SetActive(true);
            if (stars >= requiredStars)
            {
                star.sprite = goldStar;
            }
            else
            {
                star.sprite = silverStar;
            }
        }
    }
}