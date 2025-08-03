using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 1.5f;
    [SerializeField] private CapsuleCollider playerCollider;
    private Vector3 center;
    private Vector3 point1;
    private Vector3 point2;


    private Interactable nearestInteractable;
    private float nearestDistance = Mathf.Infinity;

    private readonly UnityEvent _onNearestInteractable = new();

    private void Start()
    {
        center = transform.position + playerCollider.center;

        point1 = center + Vector3.up * (playerCollider.height * interactionRange / 2 - playerCollider.radius);
        point2 = center - Vector3.up * (playerCollider.height * interactionRange / 2 - playerCollider.radius);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapCapsule(point1, point2, playerCollider.radius * interactionRange); 
        GetNearestInteractable(colliders);

        if (nearestInteractable != null && nearestDistance <= interactionRange)
        {
            _onNearestInteractable.Invoke();
        }
        else
        {
            Debug.Log("No interactable in range.");
        }
    }

    private Interactable GetNearestInteractable(Collider[] colliders)
    {
        nearestInteractable = null;
        nearestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Interactable>(out Interactable interactable))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }
        }
        return nearestInteractable;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
    }

    public UnityEvent OnButtonPressed => _onNearestInteractable;
}
