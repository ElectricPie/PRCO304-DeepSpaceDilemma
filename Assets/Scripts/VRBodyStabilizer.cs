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
        KeepUpright();
        KeepUnderCamera();
    }

    private void KeepUpright()
    {
        //Keeps the boyd upright
        Vector3 uprightRotation = this.transform.rotation.eulerAngles;

        uprightRotation.x = 0;
        uprightRotation.z = 0;

        this.transform.rotation = Quaternion.Euler(uprightRotation);
    }

    private void KeepUnderCamera()
    {
        //Get the position of the camera
        Vector3 belowPosition = this.transform.parent.position;

        //Moves the body down by half its hight
        belowPosition.y -= this.transform.localScale.y;

        //Set the new position
        this.transform.position = belowPosition;
    }
}
