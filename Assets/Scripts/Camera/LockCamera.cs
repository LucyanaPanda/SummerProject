using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;

public class LockCamera : MonoBehaviour
{
    [Header("Player Transform")]
    [SerializeField] private Transform playerTransform;
    
    [Header("Camera Settings")]
    [SerializeField] private CinemachineOrbitalFollow cameraFollowScript;
    [SerializeField] private float currentRadius = 5f;
    private bool isColliding = false;

    private void Update()
    {
        Vector3 direction = cameraFollowScript.transform.position - playerTransform.position;

        Physics.Raycast(playerTransform.position, direction.normalized, out RaycastHit hit, currentRadius);

        if (hit.collider != null  && !hit.collider.CompareTag("Player"))
        {
            if(!isColliding)
            {
                isColliding = true;
                currentRadius = cameraFollowScript.Radius;
            }

            float distance = Vector3.Magnitude(hit.point - playerTransform.position);
            distance = Mathf.Min(distance, currentRadius);
            cameraFollowScript.Radius = distance;
        }
        else
        { 
            if(isColliding)
            {
                cameraFollowScript.Radius = currentRadius;
                isColliding = false;
            }
        }
    }
}