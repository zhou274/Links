using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CloseButtonScript : MonoBehaviour
    {
        public void Start()
        {
            var imageObject = this.gameObject.transform.Find("Image");
            if (imageObject != null)
            {
                var image = imageObject.GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = ThemeManager.instance.closeButtonSprite;
                }
            }
        }
    }
}