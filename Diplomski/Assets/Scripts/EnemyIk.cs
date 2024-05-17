using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyIk : MonoBehaviour
{
    [Header("IK Components")]
    public Animator animator;
    public Transform leftArmTarget;
    public Transform rightArmTarget;
    public Transform leftArmHint;
    public Transform rightArmHint;
    public Transform gunHolder;
    public Transform shoulderBone;
    public Transform enemy;

    [Space(2)]
    [Header("Enemy vision")]
    public float sightRange = 20f;
    private Transform playerTarget;
    //[HideInInspector] 
    public bool playerInSight;
    private Vector3 lastPlayerPosition;
    public Transform exclamationMark;


    private Transform player;
    private Vector3 IK_lookPos;
    private float lerpRate = 15;
    private GameObject helper;
    void Start()
    {
        helper = new GameObject();
        helper.name = "Enemy Helper";

        player = GameObject.Find("Player").transform;
        if(player == null)
        {
            player = new GameObject().transform;
        }

        playerTarget = GameObject.Find("PlayerHead").transform;
        if(playerTarget == null)
        {
            playerTarget = new GameObject().transform;
        }

        lastPlayerPosition = transform.position + transform.forward * 2;

    }

    private void FixedUpdate()
    {
        CheckIfPlayerInSight();
    }

    void Update()
    {
        exclamationMark.rotation = Quaternion.Euler(0,0,0);
        exclamationMark.LookAt(Vector3.up);
        if(playerInSight)
        {
            gunHolder.LookAt(player.position);
            exclamationMark.gameObject.SetActive(true);
        } else
        {
            exclamationMark.gameObject.SetActive(false);
        }

        Vector3 shoulderPos = shoulderBone.TransformPoint(Vector3.zero);
        helper.transform.position = shoulderPos;
        helper.transform.parent = transform;

        gunHolder.position = helper.transform.position;

        if(player.position.x > transform.position.x && playerInSight)
        {
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.Euler(0, 90, 0), 10 * Time.deltaTime);
        } else if(playerInSight)
        {
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.Euler(0, 90+180, 0), 10 * Time.deltaTime);
        }
    }

    void CheckIfPlayerInSight()
    {
        Debug.DrawRay(shoulderBone.position, playerTarget.position - shoulderBone.position);
        if(Physics.Raycast(shoulderBone.position, playerTarget.position - shoulderBone.position, out RaycastHit hit, sightRange))
        {
            
            if(hit.transform.gameObject.tag == player.tag)
            {
                playerInSight = true;
                lastPlayerPosition = playerTarget.position;
            } else
            {
                playerInSight = false;
            }
        } else
        {
            playerInSight = false;
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

        IK_lookPos = Vector3.Lerp(IK_lookPos, lastPlayerPosition, Time.deltaTime * lerpRate);
        animator.SetLookAtWeight(1, 0.6f, 1, 1, 0);

        animator.SetLookAtPosition(IK_lookPos);
    }
}
