using UnityEngine;

namespace Lucyana.Player.UI
{
    public class ShowInteraction : MonoBehaviour
    {
        public PlayerInteract playerInteract;
        public GameObject interactionCanvas;

        void Start()
        {
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
