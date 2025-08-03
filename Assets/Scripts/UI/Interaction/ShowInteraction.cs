using UnityEngine;
using UnityEngine.Rendering;

public class ShowInteraction : MonoBehaviour
{
    public PlayerInteract playerInteract;
    public GameObject interactionCanvas;

    void Start()
    {
        playerInteract.OnNearestInteractable.AddListener(ShowInteractionMessage);
        playerInteract.OnNotNearestInteractable.AddListener(HideInteractionMessage);
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
