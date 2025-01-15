using UnityEngine;

namespace GameTemplate.Scripts
{
    public abstract class LevelButtonScript : MonoBehaviour
    {
        public int levelIndex;
        public int collectionIndex;
        public LevelCompletionState levelCompletionState;

        public abstract void SetCompletedState();
    }
}