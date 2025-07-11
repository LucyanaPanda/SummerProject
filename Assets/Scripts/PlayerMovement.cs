using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Transform Player")]
    [SerializeField] private Transform playerTransform;

    private Vector3 direction;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 forward = playerTransform.forward;
        Vector3 right = playerTransform.right;

        forward.y = 0; // Ensure movement is horizontal
        right.y = 0; // Ensure movement is horizontal

        forward.Normalize();
        right.Normalize();

        direction = direction.x * forward + direction.y * right;

        direction.Normalize();

        playerTransform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            direction = context.ReadValue<Vector3>();
        }
        else if (context.canceled)
        {
            direction = Vector3.zero;
        }
    }
}
