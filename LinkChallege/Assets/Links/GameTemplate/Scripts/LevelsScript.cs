using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public abstract class LevelsScript<TLevel, TLevelScript> : MonoBehaviour
        where TLevel : IClonable<TLevel>
        where TLevelScript : ILevelScript<TLevel>
    {
        public GameObject levelButtonPrefab;
        public ScrollRect scrollRect;
        public GameObject levelsParent;
        public MenuManagerScript menuManager;

        public TLevelScript levelScript;

        private List<TLevel> levels = new List<TLevel>();
        private int currentCollection;
        private int currentLevel;
        private bool levelLoaded;

        public void LoadCollection(int collectionIndex)
        {
            currentCollection = collectionIndex;

            var levelsInCollection = LevelsManagerScript.instance.collections[collectionIndex].numLevels;
            levels.Clear();
            for (int levelIndex = 0; levelIndex < levelsInCollection; levelIndex++)
            {
                var level = PrepareLevel(collectionIndex, levelIndex);
                levels.Add(level);
            }

            var parentRect = levelsParent.transform;
            var levelButtons = parentRect.GetComponentsInChildren<LevelButtonScript>();

            foreach (var levelButton in levelButtons)
            {
                DestroyImmediate(levelButton.gameObject);
            }

            for (int levelIndex = 0; levelIndex < levels.Count; levelIndex++)
            {
                var levelObject = GameObject.Instantiate(levelButtonPrefab, parentRect);
                levelObject.name = "关卡" + (levelIndex + 1);

                var button = levelObject.GetComponent<Button>();
                var localLevelIndex = levelIndex;
                var localCollectionIndex = collectionIndex;
                button.onClick.AddListener(() => { LoadLevel(localLevelIndex); });

                var text = levelObject.GetComponentInChildren<Text>();
                text.text = (levelIndex + 1).ToString();

                var levelButton = levelObject.GetComponent<LevelButtonScript>();
                levelButton.levelIndex = levelIndex;
                levelButton.collectionIndex = collectionIndex;
                levelButton.SetCompletedState();
            }
       
            levelLoaded = true;
        }

        public void OnEnable()
        {
            var parentRect = levelsParent.transform;
            var levelButtons = parentRect.GetComponentsInChildren<LevelButtonScript>();

            foreach (var levelButton in levelButtons)
            {
                levelButton.SetCompletedState(/*(LevelCompletionState)PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(currentCollection, levelButton.levelIndex))*/);
            }
            levelLoaded = true;
        }

        public void Update()
        {
            if (levelLoaded)
            {
                Debug.Log("normalizedPosition:" + scrollRect.normalizedPosition);
                Debug.Log("levelLoaded:" + levelLoaded);
                scrollRect.normalizedPosition = new Vector2(1, 1);
                if (scrollRect.normalizedPosition == new Vector2(1, 1))
                {
                    levelLoaded = false;
                }
            }
        }

        public abstract TLevel PrepareLevel(int collectionIndex, int levelIndex);

        public void LoadLevel(int levelIndex)
        {
            // If completed last level of collection
            if (levelIndex >= levels.Count)
            {
                // If this is the last collection, complete the game
                if (currentCollection == LevelsManagerScript.instance.collections.Length)
                {
                    menuManager.GameCompleted();
                    return;
                }

                currentCollection++;
                LoadCollection(currentCollection);
                levelIndex = 0;
            }

            Debug.Log($"Loading level {levelIndex} from collection {currentCollection}");
            currentLevel = levelIndex;
            levelScript.level = levels[levelIndex].Clone();


            menuManager.StartGame();

            levelScript.LoadLevel();
        }

        public void LoadNextLevel()
        {
            LoadLevel(currentLevel + 1);
        }
    }
}