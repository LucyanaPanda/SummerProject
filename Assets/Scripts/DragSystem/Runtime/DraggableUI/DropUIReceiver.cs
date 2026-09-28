using UnityEngine;

namespace Lucyana.DragSystem
{
    public abstract class DropUIReceiver : MonoBehaviour
    {
        private DragUI currentDragUI;
        
        public abstract bool CanDrop();
        
        protected abstract bool OnDrop(DragUI previousDragUI,  DragUI newDragUI);
        
        public bool Drop(DragUI dragUI)
        {
            if (!CanDrop() || dragUI == null || dragUI == currentDragUI)
                return false;
            
            DragUI previous = currentDragUI;
            currentDragUI = dragUI;
            
            return OnDrop(previous, dragUI);
        }
        
        public DragUI GetCurrentDragUI() => currentDragUI;
    }
}