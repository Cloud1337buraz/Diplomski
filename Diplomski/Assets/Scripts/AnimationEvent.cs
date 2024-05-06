using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public PlayerController controllerScript;

    public void animationEventFreeze()
    {
        controllerScript.rb.velocity = Vector3.zero;
        controllerScript.rb.constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("freeze");
    }

    public void animationEventUnfreeze()
    {
        controllerScript.rb.constraints = RigidbodyConstraints.None;
        controllerScript.rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("unfreeze");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
