using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace QFramework.Objects
{
    public class MouseContorller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GameObject objectToSpawn; // 需要在游戏场景中生成的物体
        public Transform parent; // UI物体的父物体
        private RectTransform rectTransform;
        private Canvas canvas;
        private Vector3 orginalPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 开始拖拽时
            orginalPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // 更新UI物体位置
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 结束拖拽时，将UI物体转换为游戏物体
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
                    eventData.pressEventCamera, out var worldPosition))
            {
                Vector2 position = new Vector2(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
                if (GlobalGame.Instance.PointMap.GetPointInMap(new Point((int)position.x, (int)position.y),true)!=null)
                {
                    //目标添加
                    // TargetManager.Instance.TargetList.Add(Instantiate(objectToSpawn, position, Quaternion.identity,
                    //     parent));
                    // Destroy(gameObject);
                    // SnakeManager.Instance.InitializeMap();
                    // SnakeManager.Instance.start = true;
                }
                else
                {
                    Debug.Log("Position out of bounds, resetting position");
                    transform.position = orginalPosition;
                }
            }
            else
            {
                Debug.Log("Failed to convert screen point to world point, resetting position");
                transform.position = orginalPosition;
            }

        }
    }
}