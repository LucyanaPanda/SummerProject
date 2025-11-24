using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class GridSystem : MonoBehaviour
{
    [Header("Grid System Settings")]
    [SerializeField] private float gridSizeCell = 1f;
    [SerializeField] private GameObject gridOverlayCellPrefab;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask constructableLayer;
    private bool isOnGridMode = false;
    private GameObject gridOverlayCell;

    [Header("Player Component")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cinemachineCamera;
    private PlayerMovement playerMovement;

    [Header("Camera Component")]
    private CinemachineCamera cinemachineCameraScript;
    

    private void Start()
    {
        gridOverlayCell = Instantiate(gridOverlayCellPrefab, transform.position, Quaternion.identity);
        gridOverlayCell.SetActive(false);

        playerMovement = player.GetComponent<PlayerMovement>();
        cinemachineCameraScript = cinemachineCamera.GetComponent<CinemachineCamera>();
    }

    public void OnGridSystemMode(InputAction.CallbackContext context)
    {
        isOnGridMode = !isOnGridMode;

        if (!isOnGridMode)
        {
            gridOverlayCell.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerMovement.enabled = true;
            gridOverlayCell.SetActive(false);
            cinemachineCameraScript.Follow = player.transform;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            playerMovement.enabled = false;
            gridOverlayCell.SetActive(true);
            cinemachineCameraScript.Follow = null;
        }
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        if (isOnGridMode)
        {
            gridOverlayCell.transform.position = GetCellPosition();
        }
    }

    public Vector3 GetCellPosition()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseScreenPosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        Physics.Raycast(ray, out RaycastHit hitInfos, rayDistance, constructableLayer);
        Vector3 cellPosition = new Vector3
        (
            Mathf.Round(hitInfos.point.x / gridSizeCell) * gridSizeCell,
            Mathf.Round(hitInfos.point.y / gridSizeCell) * gridSizeCell,
            Mathf.Round(hitInfos.point.z / gridSizeCell) * gridSizeCell
        );
        return cellPosition;
    }
}
