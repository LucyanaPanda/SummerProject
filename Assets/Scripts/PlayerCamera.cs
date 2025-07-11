using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float minZoom = -3f;
    [SerializeField] private float maxZoom = -6f;

    [Header("Transform Camera")]
    [SerializeField] private Transform cameraTransform;

    private Vector2 rotation;

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        cameraTransform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotation = context.ReadValue<Vector2>();

            print(rotation);
        }
    }
}
