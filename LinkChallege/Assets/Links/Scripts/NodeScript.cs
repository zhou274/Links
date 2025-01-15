using System;
using GameTemplate.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Links.Scripts
{
    [RequireComponent(typeof(Button))]
    public class NodeScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public bool isHint;
        public Node node;
        public MapScript map;

        public Sprite links0;
        public Sprite links1;
        public Sprite links2;
        public Sprite links3;
        public Sprite links4;

        private Sprite[] links;

        public Sprite staticImage;
        public Sprite rotatableImage;
        public Sprite moveHorizontalImage;
        public Sprite moveVerticalImage;
        public Sprite moveableImage;
        public Sprite moveableRotatableImage;

        public AudioClip nodeChangedAudioClip;
        public AudioClip nodeRotatedAudioClip;

        private Image iconImage;
        private Image linksTopImage;
        private Image linksRightImage;
        private Image linksBottomImage;
        private Image linksLeftImage;

        private Button button;
        private RectTransform rect;
        private Animator animator;


        public void Start()
        {
            links = new[] { links0, links1, links2, links3, links4 };

            iconImage = gameObject.transform.Find("Icon").GetComponent<Image>();
            linksTopImage = gameObject.transform.Find("Top").GetComponent<Image>();
            linksRightImage = gameObject.transform.Find("Right").GetComponent<Image>();
            linksBottomImage = gameObject.transform.Find("Bottom").GetComponent<Image>();
            linksLeftImage = gameObject.transform.Find("Left").GetComponent<Image>();
            //map = gameObject.transform.parent.GetComponent<MapScript>();

            iconImage.sprite = GetIconImage();
            if (isHint)
            {
                iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0.5f);
            }
            linksTopImage.sprite = node.linksTop == -1 ? links0 : links[node.linksTop];
            linksRightImage.sprite = node.linksRight == -1 ? links0 : links[node.linksRight];
            linksBottomImage.sprite = node.linksBottom == -1 ? links0 : links[node.linksBottom];
            linksLeftImage.sprite = node.linksLeft == -1 ? links0 : links[node.linksLeft];

            button = gameObject.GetComponent<Button>();
            rect = GetComponent<RectTransform>();
            animator = gameObject.GetComponent<Animator>();

            button.onClick.AddListener(HandleClick);

            animator.ResetTrigger("Disappear");
        }

        public void Disappear()
        {
            animator.SetTrigger("Disappear");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private Sprite GetIconImage()
        {
            switch (node.nodeType)
            {
                case NodeType.MoveHorizontal:
                    return moveHorizontalImage;
                case NodeType.Moveable:
                    return moveableImage;
                case NodeType.MoveableRotatable:
                    return moveableRotatableImage;
                case NodeType.MoveVertical:
                    return moveVerticalImage;
                case NodeType.Rotatable:
                    return rotatableImage;

                //case NodeType.Static:
                default:
                    return staticImage;
            }
        }

        public void HandleClick()
        {
            if ((node.nodeType & NodeType.Rotatable) == NodeType.Rotatable)
            {
                SoundsManagerScript.instance?.PlaySound(nodeRotatedAudioClip);

                map.RotateNode(this);

                linksTopImage.sprite = node.linksTop == -1 ? links0 : links[node.linksTop];
                linksRightImage.sprite = node.linksRight == -1 ? links0 : links[node.linksRight];
                linksBottomImage.sprite = node.linksBottom == -1 ? links0 : links[node.linksBottom];
                linksLeftImage.sprite = node.linksLeft == -1 ? links0 : links[node.linksLeft];

                map.playerMoves++;
                map.CheckLevelCompleted();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!button.interactable)
            {
                return;
            }
            button.onClick.RemoveListener(HandleClick);

            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!button.interactable)
            {
                return;
            }

            var deltaX = eventData.delta.x;
            var deltaY = eventData.delta.y;
            if ((node.nodeType & NodeType.MoveHorizontal) != NodeType.MoveHorizontal)
                deltaX = 0;
            if ((node.nodeType & NodeType.MoveVertical) != NodeType.MoveVertical)
                deltaY = 0;

            Vector3 newPosition = rect.position + new Vector3(deltaX, deltaY, 0);
            Vector3 oldPos = rect.position;
            rect.position = newPosition;

            if (!IsRectTransformInsideScreen(rect))
            {
                rect.position = oldPos;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!button.interactable)
            {
                return;
            }

            if ((node.nodeType & NodeType.MoveHorizontal) == NodeType.MoveHorizontal ||
                (node.nodeType & NodeType.MoveVertical) == NodeType.MoveVertical)
            {
                var deltaX = eventData.delta.x;
                var deltaY = eventData.delta.y;
                if ((node.nodeType & NodeType.MoveHorizontal) != NodeType.MoveHorizontal)
                    deltaX = 0;
                if ((node.nodeType & NodeType.MoveVertical) != NodeType.MoveVertical)
                    deltaY = 0;

                Vector3 newPosition = rect.position + new Vector3(deltaX, deltaY, 0);
                Vector3 oldPos = rect.position;
                rect.position = newPosition;

                if (!IsRectTransformInsideScreen(rect))
                {
                    rect.position = oldPos;
                    return;
                }

                var newX = 2 + (int)Math.Round(transform.localPosition.x / 60);
                var newY = 2 - (int)Math.Round(transform.localPosition.y / 60);
                map.SwitchNodes(node.x, node.y, newX, newY, this);

                SoundsManagerScript.instance?.PlaySound(nodeChangedAudioClip);

                map.playerMoves++;
                map.CheckLevelCompleted();
            }

            button.onClick.AddListener(HandleClick);
        }

        private bool IsRectTransformInsideScreen(RectTransform rectTransform)
        {
            if (rectTransform.anchoredPosition.x < 25)
            {
                return false;
            }

            if (rectTransform.anchoredPosition.y > -25)
            {
                return false;
            }

            if ((rectTransform.anchoredPosition.x + rectTransform.rect.width) > 335)
            {
                return false;
            }

            if ((rectTransform.anchoredPosition.y + rectTransform.rect.height) < -235)
            {
                return false;
            }
            return true;
        }
    }
}