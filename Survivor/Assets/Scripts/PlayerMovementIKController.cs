using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementIKController : MonoBehaviour
{
    public LayerMask LayerMaskToIgnore;
    [Range(0f, 2f)]
    public float DistanceToGround;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

        RaycastHit hit;
        Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, LayerMaskToIgnore))
        {
            if (hit.transform.gameObject.layer == 6) // Walkable
            {
                Vector3 leftFootPosition = hit.point;
                leftFootPosition.y += DistanceToGround;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition);
            }
        }

        ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, LayerMaskToIgnore))
        {
            if (hit.transform.gameObject.layer == 6) // Walkable
            {
                Vector3 leftFootPosition = hit.point;
                leftFootPosition.y += DistanceToGround;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, leftFootPosition);
            }
        }
    }
}