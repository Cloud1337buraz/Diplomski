using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public Transform PlayerModel;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float ceilingDistance = 0.4f;
    public LayerMask ceilingMask;

    public float walkSpeed = 12f;
    public float crouchSpeed;
    public float currentSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float crouchHeight;

    Vector3 velocity;
    Vector3 normalHeight;
    bool isUnderSomething;
    bool isGrounded;
    bool isCrouching;
    void Start()
    {
        currentSpeed = walkSpeed;
        crouchSpeed = walkSpeed / 2;
        normalHeight = transform.localScale;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isUnderSomething = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);

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
        if(Input.GetKeyDown("s") && isGrounded)
        {
            isCrouching = true;
        }
        if (Input.GetKeyUp("s") && isGrounded)
        {
            isCrouching = false;
        }
        while(isUnderSomething)
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