using UnityEngine;
using UnityEngine.EventSystems;

namespace Lucyana.DragSystem
{
    public abstract class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static bool isDragging = false;
        
        protected abstract bool CanDrag();
        
        private int indexSibling;
        private Camera mainCamera;
        private Plane dragPlane;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isDragging) return;
            
            isDragging = true;
            indexSibling = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
            
            dragPlane = new Plane(Vector3.up, transform.position + Vector3.up * 0.1f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragPlane.Raycast(mainCamera.ScreenPointToRay(eventData.position), out float enter))
            {
                transform.position = mainCamera.ScreenPointToRay(eventData.position).GetPoint(enter);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;

            transform.SetSiblingIndex(indexSibling);

            if (!Physics.Raycast(mainCamera.ScreenPointToRay(eventData.position), out RaycastHit hit)) return;

            if (!hit.transform.TryGetComponent(out DropUIReceiver dropReceiver)) return;
            
            if (dropReceiver.Drop(this)) return;
        }
    }
}
