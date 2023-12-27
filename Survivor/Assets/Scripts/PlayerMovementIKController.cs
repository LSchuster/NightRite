using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementIKController : MonoBehaviour
{
    public Transform RightFootTransform;
    public Transform LeftFootTransform;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, layerIndex);
        //animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, layerIndex);

        //Ray ray = new Ray(RightFootTransform.position + Vector3.up, transform.forward);


    }
}
