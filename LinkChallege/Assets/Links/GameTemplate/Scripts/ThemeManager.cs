using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public class ThemeManager : MonoBehaviour
    {
        public static ThemeManager instance;

        public Sprite coinSprite;
        public Sprite menuBackground;


        [Header("Levels")]
        public bool showNotAvailableLevelName;
        public Color levelTextColor;
        [Header("Levels\\NotAvailable")]
        public Sprite levelNotAvailable;
        public ColorBlock levelNotAvailableColors;
        [Header("Levels\\Available")]
        public Sprite levelAvailable;
        public ColorBlock levelAvailableColors;
        [Header("Levels\\Completed")]
        public Sprite levelCompleted;
        public ColorBlock levelCompletedColors;

        [Header("Collections")]
        public bool showNotAvailableCollectionName;
        public Color collectionTextColor;
        [Header("Collections\\NotAvailable")]
        public Sprite collectionNotAvailable;
        public ColorBlock collectionNotAvailableColors;
        [Header("Collections\\Available")]
        public Sprite collectionAvailable;
        public ColorBlock collectionAvailableColors;
        [Header("Collections\\Completed")]
        public Sprite collectionCompleted;
        public ColorBlock collectionCompletedColors;

        [Header("Buttons")]
        public Sprite buttonSprite;
        //public float pixelsPerUnit;
        public Color buttonTextColor;
        
        [Header("Buttons\\Icons")]
        public Sprite closeButtonSprite;

        [Header("Texts")]
        public Font textFont;

        [Header("Animations")]
        public RuntimeAnimatorController popupAnimationClip;

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
            var texts = GameObject.FindObjectsOfType<Text>(true);
            foreach (var text in texts)
            {
                text.font = textFont;
            }
        }
    }
}