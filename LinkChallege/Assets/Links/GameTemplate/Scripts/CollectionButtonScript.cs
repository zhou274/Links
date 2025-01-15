using UnityEngine;

namespace GameTemplate.Scripts
{
    public abstract class CollectionButtonScript : MonoBehaviour
    {
        public int collectionIndex;
        public LevelCompletionState collectionCompletionState;

        public abstract void SetCompletedState(LevelCompletionState collectionCompletionState);
    }
}