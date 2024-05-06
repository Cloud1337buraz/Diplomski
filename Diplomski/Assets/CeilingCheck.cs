using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    public bool isUnderSomething = false;
    private void OnTriggerEnter(Collider other)
    {
        isUnderSomething = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isUnderSomething = false;
    }
    private void OnTriggerStay(Collider other)
    {
        isUnderSomething = true;
    }
}
