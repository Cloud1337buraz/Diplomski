using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public Rigidbody rb;
    public AnimationEvent animationEventScript;

    private GroundCheck groundCheck;
    public float speed, maxForce, jumpForce;
    private Vector2 move;
    private Vector2 fakeMove;
    private Vector2 crouch;
    public bool grounded;
    public int direction;

    public Animator CharacterAnimator;

    public Transform PlayerModel;

    public float crouchSpeed;
    public float currentSpeed;

    public float hitboxHeight;
    public float hitboxY;
    public float heightPrecentage = 100f;
    public float startingY;
    private float lerp1;
    private float lerp2;
    public float lerpRate;

    public bool isUnderSomething;
    public bool isCrouching;
    public bool isSPressed;
    public CeilingCheck ceilingCheckScript;

    private float colliderHeight;
    public Vector3 lookPos;
    private Vector3 targetPos;

    private GameObject helper;
    public Transform shoulderObj;
    public Transform shoulderTrans;
    public GameObject cameraPosition;
    public LayerMask backgroundLayer;
    public PlayerStats playerStatsScript;

    public GameObject door;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!(isSPressed || isPlaying("Crouch To Stand") || isPlaying("Jump - Land") || isPlaying("Stand To Crouch") || isPlaying("Idle Crouching") || playerStatsScript.isDead))
        {
            Jump();
        }  
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        crouch = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!(isPlaying("Crouch To Stand") || isPlaying("Stand To Crouch") || playerStatsScript.isDead))
        {
            Move();
        }
        if(!playerStatsScript.isDead)
        {
            Crouch();
        }
    }

    void Jump()
    {
            Vector3 jumpForces = Vector3.zero;

            if (grounded && !isCrouching)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                jumpForces = Vector3.up * jumpForce;
                isCrouching = false;
                rb.AddForce(jumpForces, ForceMode.VelocityChange);
                grounded = false;
            }
    }

    private void Move()
    {
            Vector3 currentVelocity = rb.velocity;
            fakeMove = move;
            fakeMove.x *= direction;
            Vector3 targetVelocity = new Vector3(fakeMove.x, 0, 0);
            targetVelocity *= currentSpeed;

            //Debug.Log(fakeMove + "  " + direction);

            targetVelocity = transform.TransformDirection(targetVelocity);

            Vector3 velocityChange = (targetVelocity - currentVelocity);
            velocityChange = new Vector3(velocityChange.x, 0, 0);
        
            Vector3.ClampMagnitude(velocityChange, maxForce);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (fakeMove.x > 0 && grounded && !isCrouching)
            {
                CharacterAnimator.SetBool("RunForward", true);
            }
            else
            {
                CharacterAnimator.SetBool("RunForward", false);
            }
            if (fakeMove.x < 0 && grounded && !isCrouching)
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
        if (Input.GetKey(KeyCode.LeftControl) && grounded)
        {
            isSPressed = true;
        }
        else
        {
            isSPressed = false;
        }

        isUnderSomething = false;

        if (isSPressed)
        {
            isCrouching = true;
        }
        else if (isCrouching)
        {
            isUnderSomething = ceilingCheckScript.isUnderSomething;
        }
        if (!isSPressed)
        {
            isCrouching = false;
        }
        if (isUnderSomething)
        {
            isCrouching = true;
        }

        if (fakeMove.x > 0 && grounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchForward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchForward", false);
        }
        if (fakeMove.x < 0 && grounded && isCrouching)
        {
            CharacterAnimator.SetBool("CrouchBackward", true);
        }
        else
        {
            CharacterAnimator.SetBool("CrouchBackward", false);
        }
        if (isCrouching && grounded)
        {
            if(animationEventScript.isCrouched)
            {
                hitboxHeight = colliderHeight * heightPrecentage / 100;
                hitboxY = startingY - ((colliderHeight - hitboxHeight) / 2);
                playerCollider.height = hitboxHeight;
                playerCollider.center = new Vector3(0, hitboxY, 0);
            }
            CharacterAnimator.SetBool("CrouchIdle", true);
            currentSpeed = crouchSpeed;
        }
        if (!isCrouching && grounded)
        {
            if (!animationEventScript.isCrouched)
            {
                hitboxHeight = colliderHeight;
                hitboxY = startingY;
                playerCollider.height = hitboxHeight;
                playerCollider.center = new Vector3(0, hitboxY, 0);
            }
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

    void PlayerRotationHandler()
    {
        if(lookPos.x > transform.position.x)
        {
            cameraPosition.transform.parent = null;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, 180, 0)), Quaternion.Euler(new Vector3(0, 0, 0)), lerp1);
            lerp1 += Time.deltaTime * lerpRate;
            lerp2 = 0f;
            direction = -1;
            cameraPosition.transform.parent = transform;
        }
        else
        {
            cameraPosition.transform.parent = null;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, 0, 0)), Quaternion.Euler(new Vector3(0, 180, 0)), lerp2);
            lerp2 += Time.deltaTime * lerpRate;
            lerp1 = 0f;
            direction = 1;
            cameraPosition.transform.parent = transform;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        helper = new GameObject();

        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        colliderHeight = playerCollider.height;

        currentSpeed = speed;
        crouchSpeed = speed / 2;

        startingY = playerCollider.center.y;

        CharacterAnimator = PlayerModel.GetComponent<Animator>();
        CharacterAnimator.SetBool("Jump", false);
        CharacterAnimator.SetBool("JumpLand", false);
        CharacterAnimator.SetBool("RunForward", false);
        CharacterAnimator.SetBool("RunBackward", false);
        CharacterAnimator.SetBool("CrouchForward", false);
        CharacterAnimator.SetBool("CrouchBackward", false);
        CharacterAnimator.SetBool("CrouchIdle", false);

        lerp1 = 0f;
        lerp2 = 0f;
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 1;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, backgroundLayer))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            if(Vector3.Distance(lookP, transform.position) > 2)
            {
                lookPos = lookP;
            }
        }

        //Arm IK rotation
        if (Math.Abs(transform.position.x - lookPos.x) > 0.5)
            targetPos = lookPos;

        shoulderObj.LookAt(targetPos);

        Vector3 shoulderPos = shoulderTrans.TransformPoint(Vector3.zero);
        helper.transform.position = shoulderPos;
        helper.transform.parent = transform;

        shoulderObj.position = helper.transform.position;
        if(!playerStatsScript.isDead)
        {
            PlayerRotationHandler();
        }
    }

    public void SetGrounded(bool state)
    {
        grounded = state;
    }
}
