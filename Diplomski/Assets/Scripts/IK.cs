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

    private float weight;
    private float bodyWeight;
    private PlayerStats playerStatsScript;


    void Start()
    {
        weight = 1f;
        bodyWeight = 0.6f;
        playerStatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
           
    }

    private void OnAnimatorIK()
    {
        if(playerStatsScript.isDead)
        {
            weight = 0f;
            bodyWeight = 0f;
        }
        else
        {
            weight = 1f;
            bodyWeight = 0.6f;
        }

        //Weight
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight); 
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, weight);
        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);

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
        if (distancerlomPlayer > 0.5)
            targetPos = lookPos;

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);
        animator.SetLookAtWeight(weight, bodyWeight, weight, weight, 0);
        animator.SetLookAtPosition(IK_lookPos);
    }
}
