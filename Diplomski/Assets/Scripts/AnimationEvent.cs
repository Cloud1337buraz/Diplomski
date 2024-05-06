using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public PlayerController controllerScript;
    public bool isCrouched = false;

    public void animationEventFreeze()
    {
        controllerScript.rb.velocity = Vector3.zero;
        controllerScript.rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void animationEventUnfreeze()
    {
        controllerScript.rb.constraints = RigidbodyConstraints.None;
        controllerScript.rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void animationCrouchedDown()
    {
        isCrouched = true;
    }

    public void animationStoodUp()
    {
        isCrouched = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
