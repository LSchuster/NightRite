using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementController playerMovementController;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Update()
    {
        animator.SetBool("isRunning", playerMovementController.IsRunning);
    }
}
