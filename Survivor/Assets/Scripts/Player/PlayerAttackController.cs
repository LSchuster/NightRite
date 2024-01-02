using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float PrimaryAttackCooldownIsSeconds = 1.5f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack01");
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack02");
        }
    }
}