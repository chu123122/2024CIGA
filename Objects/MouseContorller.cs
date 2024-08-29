using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace QFramework.Objects
{
    public class MouseContorller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GameObject objectToSpawn; // 需要在游戏场景中生成的物体
        public Transform parent; // UI物体的父物体
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 开始拖拽时
            _originalPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // 更新UI物体位置
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 结束拖拽时，将UI物体转换为游戏物体
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position,
                    eventData.pressEventCamera, out var worldPosition))
            {
                Vector2 position = new Vector2(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
                if (GlobalGame.Instance.PointMap.GetPointInMap(new Point((int)position.x, (int)position.y),true)!=null)
                {
                    GameObject apple = Instantiate(objectToSpawn,position, Quaternion.identity,parent);
                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.Log("Position out of bounds, resetting position");
                    transform.position = _originalPosition;
                }
            }
            else
            {
                Debug.Log("Failed to convert screen point to world point, resetting position");
                transform.position = _originalPosition;
            }

        }
    }
}