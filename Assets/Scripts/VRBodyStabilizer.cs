using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBodyStabilizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 uprightRotation = this.transform.rotation.eulerAngles;

        uprightRotation.x = 0;
        uprightRotation.z = 0;

        this.transform.rotation = Quaternion.Euler(uprightRotation);
    }
}
