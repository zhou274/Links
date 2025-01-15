using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts
{
    public class CollectionsScript : MonoBehaviour
    {
        public GameObject collectionButtonPrefab;
        public GameObject collectionsParent;
        public MenuManagerScript menuManager;

        private List<Collection> collections = new List<Collection>();

        public void Start()
        {
            var parentRect = collectionsParent.transform;

            for (int collectionIndex = 0; collectionIndex < LevelsManagerScript.instance.collections.Length; collectionIndex++)
            {
                var collection = LevelsManagerScript.instance.collections[collectionIndex];
                collections.Add(collection);
           
                var collectionObject = GameObject.Instantiate(collectionButtonPrefab, parentRect);
                collectionObject.name = "Collection" + (collectionIndex + 1);

                var button = collectionObject.GetComponent<Button>();
                var localCollectionIndex = collectionIndex;
                button.onClick.AddListener(() => { LoadCollection(localCollectionIndex); });

                var text = collectionObject.GetComponentInChildren<TextMeshProUGUI>();
                text.text = collection.name;

                var collectionButton = collectionObject.GetComponent<CollectionButtonScript>();
                collectionButton.collectionIndex = collectionIndex;
                collectionButton.SetCompletedState((LevelCompletionState) PlayerPrefs.GetInt(PlayerPrefsConsts.Collections.State.FormattedWith(collectionIndex)));
            }
        }

        public void OnEnable()
        {
            var parentRect = collectionsParent.transform;
            var collectionButtons = parentRect.GetComponentsInChildren<CollectionButtonScript>();

            foreach (var collectionButton in collectionButtons)
            {
                collectionButton.SetCompletedState((LevelCompletionState)PlayerPrefs.GetInt(PlayerPrefsConsts.Collections.State.FormattedWith(collectionButton.collectionIndex)));
            }
        }

        public void LoadCollection(int collectionIndex)
        {
            if (collectionIndex >= collections.Count)
            {
                menuManager.GameCompleted();
                return;
            }

            Debug.Log($"Loading collection {collectionIndex}");

            menuManager.CollectionLoaded(collectionIndex);
            
        }
    }
}