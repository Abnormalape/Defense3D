using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class UI_Base : MonoBehaviour, IUI
    {
        public int sortingOrder
        {
            get => _canvas.sortingOrder;
            set => _canvas.sortingOrder = value;
        }

        public bool inputActionEnabled { get; set; }

        private Canvas _canvas;
        private GraphicRaycaster _raycastModule;
        private EventSystem _eventSystem;
        private List<RaycastResult> _raycasBuffer = new List<RaycastResult>(2);


        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _raycastModule = GetComponent<GraphicRaycaster>();
            _eventSystem = EventSystem.current;
            UIManager.instance.Register(this);
        }

        protected virtual void Update()
        {
            if (inputActionEnabled)
                InputAction();
        }

        protected virtual void InputAction()
        {
            Debug.Log("InputAction Runs");
        }

        public virtual void Hide()
        {
            _canvas.enabled = false;
        }

        public virtual void Show()
        {
            _canvas.enabled = true;
        }

        public bool TryCast<T>(Vector2 pointerPosition, out T result)
            where T : Component
        {
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = pointerPosition;
            _raycasBuffer.Clear();
            _raycastModule.Raycast(pointerEventData, _raycasBuffer);

            if (_raycasBuffer.Count > 0)
            {
                if (_raycasBuffer[0].gameObject.TryGetComponent<T>(out result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
