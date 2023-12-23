using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float YHightAbovePlayer = 2f;
    public float ZDistanceToPlayer = -5f;
    public float CameraFollowSpeed = 2;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }
    
    void Update()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, CalculateWantedPostion(), CameraFollowSpeed * Time.deltaTime);
    }

    private Vector3 CalculateWantedPostion()
    {
        return new Vector3(
            transform.position.x,
            transform.position.y + YHightAbovePlayer,
            transform.position.z + ZDistanceToPlayer);
    }
}