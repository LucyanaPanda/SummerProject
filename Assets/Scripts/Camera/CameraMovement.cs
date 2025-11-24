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
    private bool activatedRotation = false;
    private float XRotation = 0f;
    private float YRotation = 0f;

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
            if (activatedRotation)
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

        playerForward.Normalize();
        playerRight.Normalize();

        Vector3 moveDirection = playerForward * direction.z + playerRight * direction.x;
        moveDirection.Normalize();

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void Rotate()
    {
        transform.localRotation = Quaternion.Euler(-XRotation,  -YRotation, 0f);
    }
    #endregion

    #region CameraRotation

    public void ActivateCameraRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            activatedRotation = true;
        }
        else if (context.canceled)
        {
            activatedRotation = false;
        }
    }

    public void OnCameraRotationX(InputAction.CallbackContext context)
    {
        if (activatedRotation && context.performed)
        {
            XRotation += context.ReadValue<float>() * rotateSpeed;
            XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        }
    }

    public void OnCameraRotationY(InputAction.CallbackContext context)
    {
        if (activatedRotation && context.performed)
        {
            YRotation -= context.ReadValue<float>() * rotateSpeed;
        }
    }

    #endregion
}
