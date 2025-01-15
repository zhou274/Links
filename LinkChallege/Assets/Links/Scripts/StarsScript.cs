using UnityEngine;
using UnityEngine.UI;

namespace Links.Scripts
{
    public class StarsScript : MonoBehaviour
    {
        public Image star1;
        public Image star2;
        public Image star3;

        public Sprite goldStar;
        public Sprite silverStar;

        public void SetStars(int collection, int level, int stars)
        {
            SetStar(star3, stars, 3);
            SetStar(star2, stars, 2);
            SetStar(star1, stars, 1);
        }

        private void SetStar(Image star, int stars, int requiredStars)
        {
            if (stars >= requiredStars)
            {
                star.sprite = goldStar;
                star.gameObject.GetComponent<RotateThis>().enabled = true;
            }
            else
            {
                star.sprite = silverStar;
                star.gameObject.GetComponent<RotateThis>().enabled = false;
            }
        }
    }
}