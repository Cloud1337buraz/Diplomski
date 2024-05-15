using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyIk : MonoBehaviour
{
    public Animator animator;
    public Transform leftArmTarget;
    public Transform rightArmTarget;
    public Transform leftArmHint;
    public Transform rightArmHint;
    public Transform player;
    public Transform gunHolder;
    public Transform shoulderBone;
    public Transform enemy;

    private Vector3 IK_lookPos;
    private float lerpRate = 15;
    private GameObject helper;
    void Start()
    {
        helper = new GameObject();
        helper.name = "Gascina";
    }

    void Update()
    {
        gunHolder.LookAt(player.position);

        Vector3 shoulderPos = shoulderBone.TransformPoint(Vector3.zero);
        helper.transform.position = shoulderPos;
        helper.transform.parent = transform;

        gunHolder.position = helper.transform.position;

        if(player.position.x > transform.position.x)
        {
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.Euler(0, 90, 0), 10 * Time.deltaTime);
        } else
        {
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.Euler(0, 90+180, 0), 10 * Time.deltaTime);
        }
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

        IK_lookPos = Vector3.Lerp(IK_lookPos, player.position, Time.deltaTime * lerpRate);
        animator.SetLookAtWeight(1, 0.6f, 1, 1, 0);
        animator.SetLookAtPosition(IK_lookPos);
    }
}
