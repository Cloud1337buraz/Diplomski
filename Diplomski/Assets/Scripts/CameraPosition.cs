using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = cameraPosition.position;
        transform.position = Vector3.Lerp(transform.position, cameraPosition.position, 3 * Time.deltaTime);
        transform.rotation = cameraPosition.rotation;
    }
}
