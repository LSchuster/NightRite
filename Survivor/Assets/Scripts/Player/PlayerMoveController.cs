using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement Speed")]
    [Range(1f, 2f)]
    public float SprintMultiplier = 1.3f;

    [Header("Animation")]
    public float AnimatorWalkDampTime = 0.1f;

    [Header("Key Bindings")]
    public KeyCode CrouchKey = KeyCode.LeftControl;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;

    [Header("States")]
    public bool IsMoving;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Crouch();
        Sprint();
        Jump();
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalAxis) > 0.1f || Mathf.Abs(horizontalAxis) > 0.1f)
        {
            IsMoving = true;
            _animator.SetBool("IsMoving", IsMoving);
        }
        else
        {
            IsMoving = false;
            _animator.SetBool("IsMoving", IsMoving);
        }

        _animator.SetFloat("Vertical", verticalAxis, AnimatorWalkDampTime, Time.deltaTime);
        _animator.SetFloat("Horizontal", horizontalAxis, AnimatorWalkDampTime, Time.deltaTime);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(CrouchKey))
        {
            _animator.SetBool("Crouching", true);
        }

        if (Input.GetKeyUp(CrouchKey))
        {
            _animator.SetBool("Crouching", false);
        }
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(SprintKey))
        {
            _animator.speed = SprintMultiplier;
        }

        if (Input.GetKeyUp(SprintKey))
        {
            _animator.speed = 1f;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            _animator.SetTrigger("Jump");
        }
    }
}