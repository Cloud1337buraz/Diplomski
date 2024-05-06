using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    public Animator CharacterAnimator;

    public bool isUnderSomething;
    public bool isGrounded;
    public bool isCrouching;
    public bool isSPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
            return;

        playerController.rb.velocity = Vector3.zero;    
        playerController.SetGrounded(true);
        isGrounded = true;
        if (CharacterAnimator.GetBool("Jump") == true)
        {
            CharacterAnimator.SetBool("Jump", false);
            CharacterAnimator.SetBool("JumpLand", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
            return;

        playerController.SetGrounded(false);
        isGrounded = false;

        CharacterAnimator.SetBool("Jump", true);
        CharacterAnimator.SetBool("JumpLand", false);
        CharacterAnimator.SetBool("CrouchIdle", false);
        CharacterAnimator.SetBool("RunForward", false);
        CharacterAnimator.SetBool("RunBackward", false);
        CharacterAnimator.SetBool("CrouchBackward", false);
        CharacterAnimator.SetBool("CrouchForward", false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
            return;

        playerController.SetGrounded(true);
        isGrounded = true;

        if (CharacterAnimator.GetBool("Jump") == true)
        {
            CharacterAnimator.SetBool("Jump", false);
            CharacterAnimator.SetBool("JumpLand", true);
        }
    }
}
