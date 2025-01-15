using DG.Tweening;
using UnityEngine;

namespace GameTemplate.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PopupPanelScript : MonoBehaviour
    {
        public float delay = 0f;
        public float duration = 0.3f;

        public void OnEnable()
        {
            transform.localScale = Vector3.zero;

            var seq = DOTween.Sequence();

            if (delay > 0)
            {
                seq.AppendInterval(delay);
            }
            seq.Append(transform.DOScale(Vector3.one, duration));
        }
    }
}