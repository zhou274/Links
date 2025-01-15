using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public class MenuManagerScript : MonoBehaviour
    {
        public static MenuManagerScript instance;
       
        public UnityEvent OnStartGame = new UnityEvent();
        public UnityEvent<int> OnCollectionLoaded = new UnityEvent<int>();
        public UnityEvent<int, int> OnLevelStarted = new UnityEvent<int, int>();
        public UnityEvent<int, int, int> OnLevelCompleted = new UnityEvent<int, int, int>();
        public UnityEvent<int> OnCollectionCompleted = new UnityEvent<int>();
        public UnityEvent OnGameCompleted = new UnityEvent();

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

        public void StartGame()
        {
            OnStartGame.Invoke();
        }

        public void CollectionLoaded(int collectionIndex)
        {
            OnCollectionLoaded.Invoke(collectionIndex);
        }

        public void LevelStarted(int collectionIndex, int levelIndex)
        {
            OnLevelStarted.Invoke(collectionIndex, levelIndex);
        }

        public void CollectionCompleted(int collectionIndex)
        {
            OnCollectionCompleted.Invoke(collectionIndex);
        }

        public void LevelCompleted(int collectionIndex, int levelIndex, int stars)
        {
            OnLevelCompleted.Invoke(collectionIndex, levelIndex, stars);
        }

        public void GameCompleted()
        {
            OnGameCompleted.Invoke();
        }

        public void DisableAllChildButtons(GameObject parent)
        {
            var buttons = parent.GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        public void EnableAllChildButtons(GameObject parent)
        {
            var buttons = parent.GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }

        public void AddCoins()
        {
            CoinsManager.instance.AwardCoins(100);
        }

    }
}