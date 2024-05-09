using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public PlayerController playerController;
    public Transform leftArmTarget;
    public Transform rightArmTarget;

    // Start is called before the first frame update
    void Start()
    {
        leftArmTarget.position = new Vector3(5.00299978f, 1.66100001f, 10.5380001f);
        leftArmTarget.rotation = Quaternion.Euler(new Vector3(57.7307472f, 171.033356f, 183.30809f));

        rightArmTarget.position = new Vector3(5.00299978f, 1.63300002f, 10.6129999f);
        rightArmTarget.rotation = Quaternion.Euler(new Vector3(281.419922f, 178.315475f, 2.633955f));
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        //leftArmTarget.Translate(-Vector3.right * 2f * Time.deltaTime, Space.Self);
        leftArmTarget.localPosition = new Vector3(5.00299978f, 1.66100001f, 10.5380001f);
        leftArmTarget.rotation = Quaternion.Euler(new Vector3(57.7307472f, 171.033356f, 183.30809f));

        rightArmTarget.localPosition = new Vector3(5.00299978f, 1.63300002f, 10.6129999f);
        rightArmTarget.rotation = Quaternion.Euler(new Vector3(281.419922f, 178.315475f, 2.633955f));
    }
}
