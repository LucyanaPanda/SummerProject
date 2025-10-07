using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private bool canMove = false;

    [Header("Camera")]
    private CinemachineCamera cameraFollowScript;
    private Transform orientation;
    private bool activatedRotation;

    private Vector3 direction;

    private void Start()
    {
        orientation = GetComponentInChildren<Transform>();
        cameraFollowScript = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            Rotate();
        }
    }

    public void SwtichedToConstructionMode(InputAction.CallbackContext context)
    {
        canMove = !canMove;
    }

    #region Camera Movement
    public void OnCameraMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector3>();
    }

    public void Move()
    {
        Vector3 playerForward = orientation.forward;
        Vector3 playerRight = orientation.right;
        Vector3 playerUp = orientation.up;

        playerForward.Normalize();
        playerRight.Normalize();
        playerUp.Normalize();

        Vector3 moveDirection = playerForward * direction.z + playerRight * direction.x + playerUp * direction.y;
        moveDirection.Normalize();

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    
    public void Rotate()
    {
        Vector3 viewDir = transform.position - new Vector3(cameraFollowScript.transform.position.x, transform.position.y, cameraFollowScript.transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = direction.x;
        float verticalInput = direction.z;

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (inputDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotateSpeed);
        }
    }
    #endregion

    #region CameraRotation

    public void ActivateCameraRotation(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            activatedRotation = true;
        }
        else if (context.canceled)
        {
            activatedRotation = false;
        }
    }

    public void OnCameraRotation(InputAction.CallbackContext context)
    {
        print(context.ReadValue<Quaternion>());
    }

    public void RotateCamera()
    {

    }
    #endregion
}
