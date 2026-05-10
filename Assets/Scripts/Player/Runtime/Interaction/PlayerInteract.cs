using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Lucyana.InteractionSystem;
using Lucyana.Utilities;

namespace Lucyana.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerInteract : Singleton<PlayerInteract>
    {
        [Header("Interaction Settings")] [SerializeField]
        private float interactionRange = 1.5f;

        private CapsuleCollider playerCollider;
        private Vector3 center;
        private Vector3 point1;
        private Vector3 point2;

        private Interactable nearestInteractable;
        float nearestDistance = Mathf.Infinity;

        public event Action onNearestInteractableFound;
        public event Action onNearestInteractableNotFound;

        public override void Awake()
        {
            base.Awake();
            playerCollider = GetComponent<CapsuleCollider>();
        }

        private void FixedUpdate()
        {
            center = transform.position + playerCollider.center;

            point1 = center + Vector3.up * (playerCollider.height * interactionRange / 2 - playerCollider.radius);
            point2 = center - Vector3.up * (playerCollider.height * interactionRange / 2 - playerCollider.radius);

            Collider[] colliders = Physics.OverlapCapsule(point1, point2, playerCollider.radius * interactionRange);
            nearestInteractable = GetNearestInteractable(colliders);
            
            if (nearestInteractable != null && nearestDistance <= interactionRange)
                onNearestInteractableFound?.Invoke();
            else
                onNearestInteractableNotFound?.Invoke();
        }

        private Interactable GetNearestInteractable(Collider[] colliders)
        {
            Interactable nearestInteractable = null;
            nearestDistance = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Interactable interactable))
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
                nearestInteractable?.Interact();
            }
        }
    }
}

