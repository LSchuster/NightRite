using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float PlayerSpeed = 80f;
    public float JumpForce = 4;
    public float JumpCooldowsInSeconds;
    public Animator animator;
    public LayerMask layermask;

    private Rigidbody rb;

    private float lerpedVerticalAxis = 0f;
    private float lerpedHorizontalAxis = 0f;
    public bool isGrounded;
    public float Grounde;
    private bool canJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
        Jump();
        Move();
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (verticalAxis == 0)
        {
            lerpedVerticalAxis = Mathf.Lerp(lerpedVerticalAxis, 0, 0.25f);
        }
        else if (verticalAxis > 0)
        {
            lerpedVerticalAxis = Mathf.Lerp(lerpedVerticalAxis, 1, 0.08f);
        }
        else
        {
            lerpedVerticalAxis = Mathf.Lerp(lerpedVerticalAxis, -1, 0.08f);
        }

        if (horizontalAxis == 0)
        {
            lerpedHorizontalAxis = Mathf.Lerp(lerpedHorizontalAxis, 0, 0.25f);
        }
        else if (horizontalAxis > 0)
        {
            lerpedHorizontalAxis = Mathf.Lerp(lerpedHorizontalAxis, 1, 0.08f);
        }
        else
        {
            lerpedHorizontalAxis = Mathf.Lerp(lerpedHorizontalAxis, -1, 0.08f);
        }

        Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
        movement.Normalize();

        transform.position += movement * 0.04f * PlayerSpeed * Time.deltaTime;

        animator.SetFloat("Vertical", lerpedVerticalAxis);
        animator.SetFloat("Horizontal", lerpedHorizontalAxis);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            animator.SetTrigger("Jump");
            canJump = false;
            StartCoroutine(JumpCooldown());
        }
    }

    private void IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Grounde))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Walkable"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(JumpCooldowsInSeconds);
        canJump = true;
    }
}