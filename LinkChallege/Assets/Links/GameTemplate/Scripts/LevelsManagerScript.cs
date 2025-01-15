using UnityEngine;

namespace GameTemplate.Scripts
{
    public class LevelsManagerScript : MonoBehaviour
    {
        public static LevelsManagerScript instance;
       
        public bool enableFirstLevelPerCollection;
        public Collection[] collections;

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

            var collectionsLength = enableFirstLevelPerCollection ? collections.Length : 1;
            for (int index = 0; index < collectionsLength; index++)
            {
                if (PlayerPrefs.GetInt(PlayerPrefsConsts.Collections.State.FormattedWith(index)) == (int) LevelCompletionState.NotAvailable)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.Collections.State.FormattedWith(index), (int) LevelCompletionState.Available);
                }

                if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(index, 0)) == (int) LevelCompletionState.NotAvailable)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.State.FormattedWith(index, 0), (int) LevelCompletionState.Available);
                }
            }
        }

        public void OnLevelCompleted(int collectionIndex, int levelIndex, int stars)
        {
            // Set the stars for the completed level
            if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.Stars.FormattedWith(collectionIndex, levelIndex), 0) < stars)
            {
                PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.Stars.FormattedWith(collectionIndex, levelIndex), stars);
            }

            // Mark the level as completed
            if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex)) != (int) LevelCompletionState.Completed)
            {
                PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex), (int) LevelCompletionState.Completed);
                // Awars coins for level completed for the first time
                CoinsManager.instance.AwardCoinsForLevelCompleted();
            }

            // Mark next level as available
            if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex + 1)) == (int)LevelCompletionState.NotAvailable)
            {
                PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex + 1), (int)LevelCompletionState.Available);
            }

            // If completed last level in the collection, ensure the next collection is available
            if ((levelIndex + 1) == collections[collectionIndex].numLevels)
            {
                // Notify that the collection is completed
                MenuManagerScript.instance.CollectionCompleted(collectionIndex);
                // Mark the next collection as available
                if (PlayerPrefs.GetInt(PlayerPrefsConsts.Collections.State.FormattedWith(collectionIndex + 1)) == (int)LevelCompletionState.NotAvailable)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.Collections.State.FormattedWith(collectionIndex + 1), (int)LevelCompletionState.Available);
                }
                // Mark the first level of the next collection as available
                if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex + 1, 0)) == (int)LevelCompletionState.NotAvailable)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex + 1, 0), (int)LevelCompletionState.Available);
                }
            }
        }

        public void UnlockAllLevels()
        {
            for (int collectionIndex = 0; collectionIndex < collections.Length; collectionIndex++)
            {
                var collection = collections[collectionIndex];
                for (int levelIndex = 0; levelIndex < collection.numLevels; levelIndex++)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.Collections.State.FormattedWith(collectionIndex), (int)LevelCompletionState.Available);
                    if (PlayerPrefs.GetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex)) == (int)LevelCompletionState.NotAvailable)
                    {
                        PlayerPrefs.SetInt(PlayerPrefsConsts.Levels.State.FormattedWith(collectionIndex, levelIndex), (int)LevelCompletionState.Available);
                    }
                }
            }
        }
    }
}