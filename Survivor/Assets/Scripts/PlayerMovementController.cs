using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float PlayerSpeed = 2.0f;
    public float PlayerLerpSpeed = 1f;
    public float RotationSpeed = 10f;
    public float JumpHeight = 1.0f;
    public float GravityValue = -9.81f;
    public bool IsRunning;
    public bool IsGrounded;
    public bool IsStopped;
    public bool IsStopping
    {
        get
        {
            if (isStopping)
            {
                isStopping = false;
                return true;
            }
            return isStopping;
        }
        set { isStopping = value; }
    }
    private bool isStopping = false;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 oldPosition;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        oldPosition = transform.position;
    }

    void Update()
    {
        CalculateSpeed();

        IsGrounded = controller.isGrounded;
        if (IsGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = Vector3.Lerp(gameObject.transform.forward, move, RotationSpeed * Time.deltaTime);
            IsStopped = false;
        }
        else
        {
            IsStopping = true;
        }

        controller.Move(move * Time.deltaTime * PlayerSpeed);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
        }

        playerVelocity.y += GravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void CalculateSpeed()
    {
        var speed = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;

        ConsoleUtiltiies.ClearLogConsole();
        print("speed: " + speed.ToString("F2"));
        print("speed natural: " + speed);

        //if (lerpedPlayerSpeed >= 0.2f)
        //{
        //    IsRunning = true;
        //}
        //else
        //{
        //    IsRunning = false;
        //}
    }
}
