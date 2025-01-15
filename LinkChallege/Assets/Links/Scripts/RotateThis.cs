using UnityEngine;

namespace Links.Scripts
{
    public class RotateThis : MonoBehaviour
    {
        public float speed = -1.5f;

        private Quaternion rotation;

        public void Awake()
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, speed));
        }

        // Update is called once per frame
        public void Update()
        {
            transform.rotation *= rotation;
        }

        public void OnDisable()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}