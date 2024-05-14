using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        Vector3 dir2 = new Vector3(dir.x, dir.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, Vector3.Normalize(dir2), out hit, Mathf.Infinity, layerMask))
            {
                hit.transform.gameObject.GetComponent<EnemyHitScript>().Damage(25);
                Debug.Log(hit.transform.gameObject.GetComponent<EnemyHitScript>().health);
            }
            else
            {
                Debug.Log("Did not Hit");
            }
        }
        Debug.DrawRay(transform.position, Vector3.Normalize(dir2) * 1000, Color.red);
    }
}
