using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public PlayerMovementController playerMovementController;
    public float Sensivity = 500f;
    public float ClampAngle = 85f;

    private float verticalRotation;
    private float horizontalRotation;

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        Look();
        Debug.DrawLine(transform.position, transform.forward * 2, Color.red);
    }

    private void Look()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += mouseVertical * Sensivity * Time.deltaTime;
        horizontalRotation += mouseHorizontal * Sensivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -ClampAngle, ClampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        playerMovementController.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }
}