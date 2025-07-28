using UnityEngine;
using Unity.Cinemachine;

public class LockCamera : MonoBehaviour
{
    [Header("Player Transform")]
    [SerializeField] private Transform playerTransform;
    
    [Header("Camera Settings")]
    [SerializeField] private CinemachineOrbitalFollow cameraFollowScript;
    [SerializeField] private float maxRadius = 5f;

    private void Update()
    {
        Vector3 direction = cameraFollowScript.transform.position - playerTransform.position;
        Physics.Raycast(playerTransform.position, direction.normalized, out RaycastHit hit, maxRadius);

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.name);
            LockCamera script = hit.collider.GetComponent<LockCamera>();
            if (script != null)
            {
                float distance = Vector3.Magnitude(hit.point - playerTransform.position);
                distance = Mathf.Max(distance, maxRadius);
                cameraFollowScript.Radius = distance;
            }
        }
    }
}