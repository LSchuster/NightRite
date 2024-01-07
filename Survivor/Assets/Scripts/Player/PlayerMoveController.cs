using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement Values")]
    [Range(1f, 2f)]
    public float SprintMultiplier = 1.3f;
    [Range(500f, 3000f)]
    public float JumpForce = 1600;
    [Range(0.5f, 4f)]
    public float JumpCooldownInSeconds = 1f;

    [Header("Animation")]
    public float AnimatorWalkDampTime = 0.1f;

    [Header("Key Bindings")]
    public KeyCode CrouchKey = KeyCode.LeftControl;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;

    [Header("Is Grounded Check")]
    public Transform IsGroundedSource;
    public LayerMask GroundLayer;
    [Range(0.1f, 1f)]
    public float IsGroundedRayLenght = 0.2f;
    [Range(0.2f, 2f)]
    public float IsFallingRayLenght = 0.8f;

    [Header("States")]
    public bool IsMoving;
    public bool IsSprinting;
    public bool IsCrouching;
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsFalling;

    private Animator _animator;
    private Rigidbody _rigidBody;
    private bool _canJump = true;
    private bool _lastFrameIsFalling;
    private bool _lastFrameIsGrounded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        SetLastFrameValues();
        CheckIsGrounded();
        CheckIsFalling();
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
            IsCrouching = true;
            _animator.SetBool("Crouching", IsCrouching);
        }

        if (Input.GetKeyUp(CrouchKey))
        {
            IsCrouching = false;
            _animator.SetBool("Crouching", IsCrouching);
        }
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(SprintKey))
        {
            IsSprinting = true;
            _animator.speed = SprintMultiplier;
        }

        if (Input.GetKeyUp(SprintKey))
        {
            IsSprinting = false;
            _animator.speed = 1f;
        }
    }

    private void Jump()
    {
        if (_canJump && Input.GetKeyDown(JumpKey))
        {
            if (IsGrounded)
            {
                IsJumping = true;
                StartCoroutine(JumpCooldown());
                var jumpDirection = Vector3.up;
                if (Mathf.Abs(Input.GetAxis("Vertical") - 0) > 0.2f)
                {
                    jumpDirection = (Vector3.up + (transform.forward * (_rigidBody.velocity.magnitude / 4))).normalized;
                }
                
                _animator.applyRootMotion = false;
                _animator.SetBool("IsJumping", IsJumping);
                _rigidBody.AddForce(jumpDirection * JumpForce, ForceMode.Force);
                _canJump = false;
            }
        }
    }

    private void CheckIsFalling()
    {
        Debug.DrawRay(IsGroundedSource.position, -Vector3.up * IsFallingRayLenght, Color.green);
        if (Physics.Raycast(IsGroundedSource.position, -Vector3.up, IsFallingRayLenght, GroundLayer))
        {
            // Not Falling

            if (_lastFrameIsFalling)
            {
                _animator.applyRootMotion = true;
            }

            IsFalling = false;
        }
        else
        {
            // Falling

            IsFalling = true;
        }

        _animator.SetBool("IsFalling", IsFalling);
    }

    private void CheckIsGrounded()
    {
        Debug.DrawRay(IsGroundedSource.position, -Vector3.up * IsGroundedRayLenght, Color.red);
        if (Physics.Raycast(IsGroundedSource.position, -Vector3.up, IsGroundedRayLenght, GroundLayer))
        {
            // Grounded

            IsGrounded = true;
        }
        else
        {
            // Not Grounded

            if (IsJumping)
            {
                IsJumping = false;
            }

            IsGrounded = false;
        }
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetBool("IsJumping", IsJumping);
    }

    private void SetLastFrameValues()
    {
        _lastFrameIsFalling = IsFalling;
        _lastFrameIsGrounded = IsGrounded;
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(JumpCooldownInSeconds);
        _canJump = true;
    }
}