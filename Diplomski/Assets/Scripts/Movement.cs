using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Animator CharacterAnimator;

    bool jumpAnimation;

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
    public float crouchHeight;

    Vector3 velocity;
    Vector3 normalHeight;
    public bool isUnderSomething;
    public bool isGrounded;
    public bool isCrouching;
    public bool isSPressed;
    void Start()
    {
        currentSpeed = walkSpeed;
        crouchSpeed = walkSpeed / 2;
        normalHeight = transform.localScale;

        CharacterAnimator = PlayerModel.GetComponent<Animator>();
        CharacterAnimator.SetBool("Jump", false);
        CharacterAnimator.SetBool("JumpLand", false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = Vector3.up * ceilingDistance;
        Gizmos.DrawRay(ceilingCheck.position, direction);
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if(Input.GetKeyDown("w") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isCrouching = false;
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
        if (isCrouching)
        {
            if(transform.localScale.y != crouchHeight)
            transform.localScale = new Vector3(normalHeight.x, crouchHeight, normalHeight.z);
            currentSpeed = crouchSpeed;
        }
        if(!isCrouching)
        {
            if(transform.localScale.y != normalHeight.y)
            transform.localScale = normalHeight;
            currentSpeed = walkSpeed;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

}