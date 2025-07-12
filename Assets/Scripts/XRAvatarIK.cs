using UnityEngine;

public class XRAvatarIK : MonoBehaviour
{
    public Animator animator;

    public Transform leftHandTarget;
    public Transform rightHandTarget;

    public bool enableIK = true;

    void OnAnimatorIK(int layerIndex)
    {
        if (animator && enableIK)
        {
            // Left Hand
            if (leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }

            // Right Hand
            if (rightHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            }
        }
    }
}
