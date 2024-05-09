using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    public Transform leftArmTarget;
    public Transform rightArmTarget;
    public Transform leftArmHint;
    public Transform rightArmHint;
    Vector3 lookPos;
    Vector3 IK_lookPos;
    Vector3 targetPos;
    float lerpRate = 15;

    void Start()
    {
        /*leftArmTarget.localPosition = new Vector3(5.00299978f, 1.66100001f, 10.5380001f);
        leftArmTarget.rotation = Quaternion.Euler(new Vector3(57.7307472f, 171.033356f, 183.30809f));

        rightArmTarget.localPosition = new Vector3(5.00299978f, 1.63300002f, 10.6129999f);
        rightArmTarget.rotation = Quaternion.Euler(new Vector3(281.419922f, 178.315475f, 2.633955f));*/
    }

    void Update()
    {
    
    }

    private void OnAnimatorIK()
    {
        //Weight
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1); 
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        //Pos
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightArmTarget.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftArmTarget.position);

        //Rot
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightArmTarget.rotation); 
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftArmTarget.rotation);

        //Hint pos
        animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightArmHint.position); 
        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftArmHint.position);

        this.lookPos = playerController.lookPos;
        lookPos.z = transform.position.z;
        float distancerlomPlayer = Vector3.Distance(lookPos, transform.position);
        if (distancerlomPlayer > 0)
            targetPos = lookPos;

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);
        animator.SetLookAtWeight(1, 0.6f, 1, 1, 0);
        animator.SetLookAtPosition(IK_lookPos);
    }
}
