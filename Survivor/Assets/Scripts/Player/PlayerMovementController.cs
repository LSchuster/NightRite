using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float PlayerSpeed = 80f;
    public float PlayerSprintSpeedMultiplier = 2f;
    public float JumpForce = 4;
    public float JumpCooldowsInSeconds;
    public Animator animator;
    public LayerMask layermask;

    private float lerpedVerticalAxis = 0f;
    private float lerpedHorizontalAxis = 0f;
    private bool isGrounded;
    private bool canJump = true;

    private void FixedUpdate()
    {
        IsGrounded();
        Jump();
        //Turn();
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

        animator.SetFloat("Vertical", lerpedVerticalAxis);
        animator.SetFloat("Horizontal", lerpedHorizontalAxis);

        float speedMultiplier = 1f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = PlayerSprintSpeedMultiplier;
            animator.SetBool("Floating", true);
        }
        else
        {
            speedMultiplier = 1f;
            animator.SetBool("Floating", false);
        }

        Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
        movement.Normalize();

        transform.position += movement * 0.04f * PlayerSpeed * speedMultiplier * Time.deltaTime;
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

    private float idleRotation;
    private float idleRotationSpeed = 0.3f;


    private void Turn()
    {
        var mouseX = Input.GetAxis("Mouse X");
        ConsoleUtiltiies.ClearLogConsole();
        print(mouseX);
        print(idleRotation);

        if (mouseX == 0)
        {
            // Nichts
            idleRotation = Mathf.Lerp(idleRotation, 0.5f, idleRotationSpeed * Time.deltaTime);
            animator.SetFloat("MouseHorizontal", idleRotation);
        }
        else if (mouseX < 0)
        {
            // Links
            idleRotation = Mathf.Lerp(idleRotation, 0f, idleRotationSpeed * 5 * Time.deltaTime);
            animator.SetFloat("MouseHorizontal", 0f);
        }
        else
        {
            // Rechts
            idleRotation = Mathf.Lerp(idleRotation, 0f, idleRotationSpeed * 5 * Time.deltaTime);
            animator.SetFloat("MouseHorizontal", 1f);
        }
    }

    private void IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 0.1f + 1f))
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