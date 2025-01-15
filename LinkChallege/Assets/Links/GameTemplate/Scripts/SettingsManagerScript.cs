using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public class SettingsManagerScript : MonoBehaviour
    {
        public static SettingsManagerScript instance;
       
        public Toggle soundsToggle;
        public Toggle musicToggle;
        public Slider volumeSlider;
        public Toggle vibrateToggle;

        public SoundsManagerScript soundsManager;
        public VibrateManagerScript vibrateManager;

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

            var isSoundEffectsEnabled = PlayerPrefs.GetInt(PlayerPrefsConsts.Settings.IsSoundEffectsEnabled, PlayerPrefsConsts.Settings.ValueEnabled) == PlayerPrefsConsts.Settings.ValueEnabled;
            var isMusicEnabled = PlayerPrefs.GetInt(PlayerPrefsConsts.Settings.IsMusicEnabled, PlayerPrefsConsts.Settings.ValueEnabled) == PlayerPrefsConsts.Settings.ValueEnabled;
            var volume = PlayerPrefs.GetFloat(PlayerPrefsConsts.Settings.Volume, 0.8f);
            var isVibrateEnabled = PlayerPrefs.GetInt(PlayerPrefsConsts.Settings.IsVibrateEnabled, PlayerPrefsConsts.Settings.ValueEnabled) == PlayerPrefsConsts.Settings.ValueEnabled;

            if (soundsToggle != null)
            {
                soundsToggle.SetIsOnWithoutNotify(isSoundEffectsEnabled);
            }
            if (musicToggle != null)
            {
                musicToggle.SetIsOnWithoutNotify(isMusicEnabled);
            }
            if (volumeSlider != null)
            {
                volumeSlider.SetValueWithoutNotify(volume);
            }
            if (vibrateToggle != null)
            {
                vibrateToggle.SetIsOnWithoutNotify(isVibrateEnabled);
            }

            if (soundsManager != null)
            {
                soundsManager.SetSounds(isSoundEffectsEnabled);
                soundsManager.SetMusic(isMusicEnabled);
                soundsManager.SetVolume(volume);
            }
            if (vibrateManager != null)
            {
                vibrateManager.SetVibrate(isVibrateEnabled);
            }
        }

        public void SetSounds(bool isEnabled)
        {
            Debug.Log(string.Format("Setting sounds to {0}", isEnabled ? "ENABLED" : "DISABLED"));
            PlayerPrefs.SetInt(PlayerPrefsConsts.Settings.IsSoundEffectsEnabled, isEnabled ? PlayerPrefsConsts.Settings.ValueEnabled : PlayerPrefsConsts.Settings.ValueDisabled);

            if (soundsManager != null)
            {
                soundsManager.SetSounds(isEnabled);
            }
        }

        public void SetMusic(bool isEnabled)
        {
            Debug.Log(string.Format("Setting music to {0}", isEnabled ? "ENABLED" : "DISABLED"));
            PlayerPrefs.SetInt(PlayerPrefsConsts.Settings.IsMusicEnabled, isEnabled ? PlayerPrefsConsts.Settings.ValueEnabled : PlayerPrefsConsts.Settings.ValueDisabled);

            if (soundsManager != null)
            {
                soundsManager.SetMusic(isEnabled);
            }
        }

        public void SetVibrate(bool isEnabled)
        {
            Debug.Log(string.Format("Setting vibrate to {0}", isEnabled ? "ENABLED" : "DISABLED"));
            PlayerPrefs.SetInt(PlayerPrefsConsts.Settings.IsVibrateEnabled, isEnabled ? PlayerPrefsConsts.Settings.ValueEnabled : PlayerPrefsConsts.Settings.ValueDisabled);

            if (vibrateManager != null)
            {
                vibrateManager.SetVibrate(isEnabled);
            }
        }

        public void SetVolume(float value)
        {
            Debug.Log(string.Format("Setting volue to {0}", value));
            PlayerPrefs.SetFloat(PlayerPrefsConsts.Settings.Volume, value);

            if (soundsManager != null)
            {
                soundsManager.SetVolume(value);
            }
        }
    }
}