using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    [RequireComponent(typeof(Image))]
    public class BackgroundScript : MonoBehaviour
    {
        public void Start()
        {
            var image = GetComponent<Image>();
            if (ThemeManager.instance.menuBackground != null)
            {
                image.sprite = ThemeManager.instance.menuBackground;
                image.color = Color.white;
            }
        }
    }
}