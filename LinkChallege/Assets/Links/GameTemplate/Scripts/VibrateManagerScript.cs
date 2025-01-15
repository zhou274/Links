using UnityEngine;

namespace GameTemplate.Scripts
{
    public class VibrateManagerScript : MonoBehaviour
    {
        public static VibrateManagerScript instance;

        private bool vibrate;

        private void Awake()
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

        public void SetVibrate(bool isVibrateEnabled)
        {
            vibrate = isVibrateEnabled;
        }

        public void Vibrate()
        {
            if (vibrate)
            {
#if UNITY_ANDROID
                Handheld.Vibrate();
#endif
            }
        }
    }
}