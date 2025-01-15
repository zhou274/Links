using GameTemplate.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Links.Scripts
{
    [RequireComponent(typeof(Button))]
    public class RequiresCoinsButtonScript : MonoBehaviour
    {
        public int requiredCoins = 50;

        public void OnEnable()
        {
            if (CoinsManager.instance.coins < requiredCoins)
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }
}