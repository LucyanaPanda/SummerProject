using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private CapsuleCollider playerCollider;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
    }
}
