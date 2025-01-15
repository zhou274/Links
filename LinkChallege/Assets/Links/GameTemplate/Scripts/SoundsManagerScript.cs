using UnityEngine;
using Random = System.Random;

namespace GameTemplate.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundsManagerScript : MonoBehaviour
    {
        public static SoundsManagerScript instance;
      
        private readonly Random m_random = new Random();
        private AudioSource m_audioSource;
        private AudioSource m_audioSourceMusic;

        public AudioClip LevelCompleted;
        public AudioClip[] BackgroundMusics;

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

            // Get audio source component
            var audioSources = GetComponents<AudioSource>();
            m_audioSource = audioSources[0];
            m_audioSourceMusic = audioSources[1];
        }

        private void Update()
        {
            if (m_audioSourceMusic != null &&
                !m_audioSourceMusic.isPlaying &&
                !m_audioSourceMusic.mute)
            {
                PlayRandomBackgroundMusic();
            }
        }

        public void PlaySound(AudioClip sound)
        {
            if (sound == null)
            {
                return;
            }
            Debug.Log($"Playing sound {sound.name}");
            if (m_audioSource != null)
            {
                m_audioSource.PlayOneShot(sound);
            }
        }

        /// <summary>
        /// Plays the given sound with option to progressively scale down volume of multiple copies of same sound playing at
        /// the same time to eliminate the issue that sound amplitude adds up and becomes too loud.
        /// </summary>
        /// <param name="sound">Sound.</param>
        /// <param name="autoScaleVolume">If set to <c>true</c> auto scale down volume of same sounds played together.</param>
        /// <param name="maxVolumeScale">Max volume scale before scaling down.</param>
        public void PlaySound(AudioClip sound, bool autoScaleVolume = true, float maxVolumeScale = 1f)
        {
            if (sound == null)
            {
                return;
            }
            if (m_audioSource != null)
            {
                m_audioSource.PlayOneShot(sound, maxVolumeScale);
            }
        }


        public void PlayRandomBackgroundMusic()
        {
            if (BackgroundMusics == null || BackgroundMusics.Length == 0)
            {
                return;
            }

            var randomIndex = m_random.Next(BackgroundMusics.Length);
            var music = BackgroundMusics[randomIndex];
            if (m_audioSourceMusic != null)
            {
                m_audioSourceMusic.clip = music;
                m_audioSourceMusic.Play();
            }

        }

        /// <summary>
        /// Stop music.
        /// </summary>
        public void Stop()
        {
            if (m_audioSource != null)
            {
                m_audioSource.Stop();
            }
        }

        public void SetSounds(bool isEnabled)
        {
            if (m_audioSource != null)
            {
                m_audioSource.mute = !isEnabled;
            }
        }

        public void SetMusic(bool isEnabled)
        {
            if (m_audioSourceMusic != null)
            {
                m_audioSourceMusic.mute = !isEnabled;
                if (isEnabled)
                {
                    m_audioSourceMusic.Play();
                }
                else
                {
                    m_audioSourceMusic.Stop();
                }
            }
        }

        public void SetVolume(float value)
        {
            if (m_audioSourceMusic != null)
            {
                m_audioSourceMusic.volume = value;
            }
            if (m_audioSource != null)
            {
                m_audioSource.volume = value;
            }
        }
    }
}