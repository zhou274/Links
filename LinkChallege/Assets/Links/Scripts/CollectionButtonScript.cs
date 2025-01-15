using GameTemplate.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Links.Scripts
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class CollectionButtonScript : GameTemplate.Scripts.CollectionButtonScript
    {
        public override void SetCompletedState(LevelCompletionState collectionCompletionState)
        {
            var button = GetComponent<Button>();
            button.transition = Selectable.Transition.ColorTint;
            
            var image = GetComponent<Image>();
            var lockImage = gameObject.transform.Find("Lock").GetComponentInChildren<Image>();
            var text = GetComponentInChildren<TextMeshProUGUI>();

            text.color = ThemeManager.instance.collectionTextColor;

            switch (collectionCompletionState)
            {
                case LevelCompletionState.NotAvailable:
                    {
                        if (ThemeManager.instance.collectionNotAvailable != null)
                        {
                            image.sprite = ThemeManager.instance.collectionNotAvailable;
                        }
                        else
                        {
                            image.color = Color.red;
                        }

                        if (!ThemeManager.instance.showNotAvailableCollectionName)
                        {
                            text.gameObject.SetActive(false);
                        }
                        lockImage.gameObject.SetActive(true);
                        button.interactable = false;

                        if (ThemeManager.instance.collectionNotAvailableColors != null)
                        {
                            button.colors = ThemeManager.instance.collectionNotAvailableColors;
                        }
                        break;
                    }
                case LevelCompletionState.Available:
                    {
                        if (ThemeManager.instance.collectionAvailable != null)
                        {
                            image.sprite = ThemeManager.instance.collectionAvailable;
                        }
                        else
                        {
                            image.color = Color.yellow;
                        }
                        lockImage.gameObject.SetActive(false);
                        button.interactable = true;

                        if (ThemeManager.instance.collectionAvailableColors != null)
                        {
                            button.colors = ThemeManager.instance.collectionAvailableColors;
                        }
                        break;
                    }
                case LevelCompletionState.Completed:
                    {
                        if (ThemeManager.instance.collectionCompleted != null)
                        {
                            image.sprite = ThemeManager.instance.collectionCompleted;
                        }
                        else
                        {
                            image.color = Color.green;
                        }
                        lockImage.gameObject.SetActive(false);
                        button.interactable = true;

                        if (ThemeManager.instance.collectionCompletedColors != null)
                        {
                            button.colors = ThemeManager.instance.collectionCompletedColors;
                        }
                        break;
                    }
            }
        }
    }
}