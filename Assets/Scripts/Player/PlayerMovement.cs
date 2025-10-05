using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private bool canMove = true;

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpLayerMask;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 5f;
    [HideInInspector] private Rigidbody rb;
    [HideInInspector] private CapsuleCollider playerCollider;

    [Header("Player Component")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform orientation;

    [Header("Camera")]
    [SerializeField] private CinemachineOrbitalFollow cameraFollowScript;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 5f;

    private Vector3 inputVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canMove)
        {
            Rotate();
            Move();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    #region Rotation
    public void Rotate()
    {
        if (canMove)
        {
            // rotate orientation
            Vector3 viewDir = playerTransform.position - new Vector3(cameraFollowScript.transform.position.x, playerTransform.position.y, cameraFollowScript.transform.position.z);
            orientation.forward = viewDir.normalized;

            // rotate player object
            float horizontalInput = inputVector.x;
            float verticalInput = inputVector.z;

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotateSpeed);
            }
        }
    }
    #endregion

    #region Movement
    public void Move()
    {
        Vector3 playerForward = orientation.forward;
        Vector3 playerRight = orientation.right;

        // Flatten the vectors (prevent vertical movement)
        playerForward.y = 0f;
        playerRight.y = 0f;
        playerForward.Normalize();
        playerRight.Normalize();

        // Combine input with camera direction
        Vector3 moveDirection = playerForward * inputVector.z + playerRight * inputVector.x;
        moveDirection.Normalize();

        playerTransform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
         // Capture the initial direction when the action starts
         inputVector = context.ReadValue<Vector3>();
    }

    #endregion

    #region Jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector3 center = transform.position + playerCollider.center;

        Vector3 point1 = center + Vector3.up * (playerCollider.height / 2 - playerCollider.radius);
        Vector3 point2 = center - Vector3.up * (playerCollider.height / 2 - playerCollider.radius);

        return Physics.CapsuleCast(point1, point2, playerCollider.radius, Vector3.down, transform.localScale.y + 0.1f, jumpLayerMask);
    }
    #endregion

    #region Show Cursor
    public void OnShowCursor(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Show the cursor and unlock it when the action starts
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable player movement when the cursor is shown
            canMove = false;

            // Pause the camera follow script 
            if (cameraFollowScript != null)
                cameraFollowScript.enabled = false;
            else
                Debug.LogWarning("Camera follow script is not assigned.");
        }
        else if (context.canceled)
        {
            // Hide the cursor and lock it when the action ends
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable player movement when the cursor is hidden
            canMove = true;

            // Resume the camera follow script
            if (cameraFollowScript != null)
                cameraFollowScript.enabled = true;
            else
                Debug.LogWarning("Camera follow script is not assigned.");
        }
    }
    #endregion

    #region Camera Zoom

    public void Zoom(float zoomValue)
    {
        float currentRadiusCamera = cameraFollowScript.Radius;

        float zoom = currentRadiusCamera + zoomValue * zoomSpeed * Time.deltaTime;

        if (zoom < maxDistance)
        {
            zoom = Mathf.Max(zoom, minDistance);
        }
        else
        {
            zoom = Mathf.Min(zoom, maxDistance);
        }

        cameraFollowScript.Radius = zoom;
        print(currentRadiusCamera + "    " + zoom);
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        Zoom(context.ReadValue<float>());
    }

    #endregion
}
