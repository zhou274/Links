using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public class CoinsDisplayerScript : MonoBehaviour
    {
        private Text text;
        private Image image;
        private Animator coinsAnimator;
        private int lastCoins;

        // TODO: "Animate" increase in coins count

        private CoinsManager m_coinsManager;

        public void Start()
        {
            m_coinsManager = FindObjectOfType<CoinsManager>();
            text = GetComponentInChildren<Text>();
            image = GetComponentInChildren<Image>();
            coinsAnimator = GetComponent<Animator>();

            if (image != null)
            {
                image.sprite = ThemeManager.instance.coinSprite;
            }
        }

        public void Update()
        {
            if (lastCoins != m_coinsManager.coins)
            {
                text.text = m_coinsManager.coins.ToString();
                lastCoins = m_coinsManager.coins;

                if (coinsAnimator != null)
                {
                    coinsAnimator.Play("NewCoin");
                }
            }
        }
    }
}