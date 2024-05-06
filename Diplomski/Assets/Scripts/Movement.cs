using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Animator CharacterAnimator;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public Transform PlayerModel;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float ceilingDistance = 0.4f;

    public float walkSpeed = 12f;
    public float crouchSpeed;
    public float currentSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float hitboxHeight;
    public float hitboxY;
    public float heightPrecentage = 100f;
    public float startingHeight;
    public float startingY;

    Vector3 velocity;
    
    public bool isUnderSomething;
    public bool isGrounded;
    public bool isCrouching;
    public bool isSPressed;


    void Start()
    {
        currentSpeed = walkSpeed;
        crouchSpeed = walkSpeed / 2;

        CharacterAnimator = PlayerModel.GetComponent<Animator>();
        CharacterAnimator.SetBool("Jump", false);
        CharacterAnimator.SetBool("JumpLand", false);
        CharacterAnimator.SetBool("RunForward", false);
        CharacterAnimator.SetBool("RunBackward", false);
        CharacterAnimator.SetBool("CrouchForward", false);
        CharacterAnimator.SetBool("CrouchBackward", false);
        CharacterAnimator.SetBool("CrouchIdle", false);

        startingHeight = controller.height;
        startingY = controller.center.y;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Vector3.up * ceilingDistance;
        Gizmos.DrawRay(ceilingCheck.position, direction);
    }
    bool isPlaying(string stateName)
    {
        if (CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        Vector3 move = transform.right * x;
        if(!(isPlaying("Jump - Land") || isPlaying("Stand To Crouch") || isPlaying("Crouch To Stand") || isPlaying("Idle Crouching")))
        {
            controller.Move(move * currentSpeed * Time.deltaTime);
            if (Input.GetKeyDown("w") && isGrounded && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isCrouching = false;
            }
        }
        if(Input.GetKey(KeyCode.D) && isGrounded && !isCrouching)
        {
            CharacterAnimator.SetBool("RunForward", true);
        }
        else
        {
            CharacterAnimator.SetBool("RunForward", false);
        }
        if(Input.GetKey(KeyCode.A) && isGrounded && !isCrouching)
        {
            CharacterAnimator.SetBool("RunBackward", true);
        }
        else
        {
            CharacterAnimator.SetBool("RunBackward", false);
        }
        if (Input.GetKey(KeyCode.D) && isGrounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchForward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchForward", false);
        }
        if (Input.GetKey(KeyCode.A) && isGrounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchBackward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchBackward", false);
        }
        if(isGrounded && CharacterAnimator.GetBool("Jump") == true)
        {
            CharacterAnimator.SetBool("Jump", false);
            CharacterAnimator.SetBool("JumpLand", true);
        }
        if(!isGrounded)
        {
            CharacterAnimator.SetBool("Jump", true);
            CharacterAnimator.SetBool("JumpLand", false);
            CharacterAnimator.SetBool("CrouchIdle", false);
            CharacterAnimator.SetBool("RunForward", false);
            CharacterAnimator.SetBool("RunBackward", false);
            CharacterAnimator.SetBool("CrouchBackward", false);
            CharacterAnimator.SetBool("CrouchForward", false);
        }
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            isSPressed = true;
        }
        else
        {
            isSPressed = false;
        }
        if(isSPressed)
        {
            isCrouching = true;
        }

        isUnderSomething = false;

        if(isSPressed)
        {
            isCrouching = true;
        }
        else if(isCrouching)
        {
            isUnderSomething = Physics.Raycast(ceilingCheck.position, Vector3.up, out RaycastHit hit, ceilingDistance, groundMask);
        }
        if(!isSPressed)
        {
            isCrouching = false;
        }
        if(isUnderSomething)
        {
            isCrouching = true;
        }
        if (isCrouching && isGrounded)
        {
            hitboxHeight = startingHeight * heightPrecentage / 100;
            hitboxY = startingY - ((startingHeight - hitboxHeight) / 2);
            controller.height = hitboxHeight;
            controller.center = new Vector3(0, hitboxY, 0);
            CharacterAnimator.SetBool("CrouchIdle", true);
            currentSpeed = crouchSpeed;
        }
        if(!isCrouching && isGrounded)
        {
            hitboxHeight = startingHeight;
            hitboxY = startingY;
            controller.height = hitboxHeight;
            controller.center = new Vector3(0, hitboxY, 0);
            CharacterAnimator.SetBool("CrouchIdle", false);
            currentSpeed = walkSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}