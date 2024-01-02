using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform LookAtTransform;
    public float RotationSpeed;

    private PlayerMoveController _moveController;

    private void Awake()
    {
        _moveController = GetComponent<PlayerMoveController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 viewDirection = transform.position - new Vector3(PlayerCamera.transform.position.x, transform.position.y, PlayerCamera.transform.position.z);
        LookAtTransform.forward = viewDirection.normalized;

        if (viewDirection != Vector3.zero && _moveController.IsMoving)
        {
            transform.forward = Vector3.Slerp(transform.forward, viewDirection.normalized, RotationSpeed * Time.deltaTime);
        }
    }
}