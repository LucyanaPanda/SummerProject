using UnityEngine;

namespace Lucyana.Player.UI
{
    public class InteractionIndicatorUI : MonoBehaviour
    {
        public GameObject interactionCanvas;
        private PlayerInteract playerInteract;

        void Start()
        {
            playerInteract = PlayerInteract.Instance;
            
            playerInteract.onNearestInteractableFound += ShowInteractionMessage;
            playerInteract.onNearestInteractableNotFound += (HideInteractionMessage);
        }

        private void ShowInteractionMessage()
        {
            interactionCanvas.SetActive(true);
        }

        private void HideInteractionMessage()
        {
            interactionCanvas.SetActive(false);
        }
    }
}
