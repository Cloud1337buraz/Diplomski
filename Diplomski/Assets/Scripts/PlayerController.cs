using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Rigidbody rb;
    private GroundCheck groundCheck;
    public float speed, maxForce, jumpForce;
    private Vector2 move;
    private Vector2 crouch;
    public bool grounded;

    public Animator CharacterAnimator;

    public Transform PlayerModel;

    public float crouchSpeed;
    public float currentSpeed;

    public float hitboxHeight;
    public float hitboxY;
    public float heightPrecentage = 100f;
    public float startingHeight;
    public float startingY;

    public bool isUnderSomething;
    public bool isCrouching;
    public bool isSPressed;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!(isPlaying("Jump - Land") || isPlaying("Stand To Crouch") || isPlaying("Crouch To Stand") || isPlaying("Idle Crouching")))
        {
            Jump();
            Debug.Log("uso");
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        crouch = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        Crouch();
    }

    void Jump()
    {
        Vector3 jumpForces = Vector3.zero;
        if (!(isPlaying("Jump - Land") || isPlaying("Stand To Crouch") || isPlaying("Crouch To Stand") || isPlaying("Idle Crouching")))
        {
            if (grounded && !isCrouching)
            {
                jumpForces = Vector3.up * jumpForce;
                isCrouching = false;
            }
            rb.AddForce(jumpForces, ForceMode.VelocityChange);
        }
    }

    private void Move()
    {
            Vector3 currentVelocity = rb.velocity;
            Vector3 targetVelocity = new Vector3(move.x, 0, 0);
            targetVelocity *= speed;

            targetVelocity = transform.TransformDirection(targetVelocity);

            Vector3 velocityChange = (targetVelocity - currentVelocity);
            velocityChange = new Vector3(velocityChange.x, 0, 0);
        
            Vector3.ClampMagnitude(velocityChange, maxForce);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (move.x > 0 && grounded && !isCrouching)
            {
                CharacterAnimator.SetBool("RunForward", true);
            }
            else
            {
                CharacterAnimator.SetBool("RunForward", false);
            }
            if (move.x < 0 && grounded && !isCrouching)
            {
                CharacterAnimator.SetBool("RunBackward", true);
            }
            else
            {
                CharacterAnimator.SetBool("RunBackward", false);
            }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S) && grounded)
        {
            isSPressed = true;
        }
        else
        {
            isSPressed = false;
        }
        if (isSPressed)
        {
            isCrouching = true;
        }
        if (!isSPressed)
        {
            isCrouching = false;
        }
        if (move.x > 0 && grounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchForward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchForward", false);
        }
        if (move.x < 0 && grounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchBackward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchBackward", false);
        }
        if (isCrouching && grounded)
        {
            //hitboxHeight = startingHeight * heightPrecentage / 100;
            //hitboxY = startingY - ((startingHeight - hitboxHeight) / 2);
            //boxCollider.size = new Vector3(0, hitboxHeight, 0);
            //boxCollider.center = new Vector3(0, hitboxY, 0);
            CharacterAnimator.SetBool("CrouchIdle", true);
            currentSpeed = crouchSpeed;
        }
        if (!isCrouching && grounded)
        {
            //hitboxHeight = startingHeight;
            //hitboxY = startingY;
            //boxCollider.size = new Vector3(0, hitboxHeight, 0);
            //boxCollider.center = new Vector3(0, hitboxY, 0);
            CharacterAnimator.SetBool("CrouchIdle", false);
            currentSpeed = speed;
        }

    }
    bool isPlaying(string stateName)
    {
        if (CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        currentSpeed = speed;
        crouchSpeed = speed / 2;

        CharacterAnimator = PlayerModel.GetComponent<Animator>();
        CharacterAnimator.SetBool("Jump", false);
        CharacterAnimator.SetBool("JumpLand", false);
        CharacterAnimator.SetBool("RunForward", false);
        CharacterAnimator.SetBool("RunBackward", false);
        CharacterAnimator.SetBool("CrouchForward", false);
        CharacterAnimator.SetBool("CrouchBackward", false);
        CharacterAnimator.SetBool("CrouchIdle", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrounded(bool state)
    {
        grounded = state;
    }
}
