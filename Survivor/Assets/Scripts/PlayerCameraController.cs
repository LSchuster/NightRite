using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform Orientation;
    public Transform Player;
    public Transform PlayerObject;

    public float RotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDirection = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = viewDirection.normalized;

        if (viewDirection != Vector3.zero)
        {
            PlayerObject.forward = Vector3.Slerp(PlayerObject.forward, viewDirection.normalized, RotationSpeed * Time.deltaTime);
        }
    }
}