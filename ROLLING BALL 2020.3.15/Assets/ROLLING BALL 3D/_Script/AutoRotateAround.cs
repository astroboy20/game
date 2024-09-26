using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateAround : MonoBehaviour {
    public float speed = 100;
    float alpha = 0;
    public enum AXIS { X, Y, Z }
    public AXIS axis = AXIS.Y;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        alpha += speed * Time.deltaTime;

        Vector3 rot = transform.localEulerAngles;

        switch (axis)
        {
            case AXIS.X:
                rot.x = alpha % 360;
                break;
            case AXIS.Y:
                rot.y = alpha % 360;
                break;
            case AXIS.Z:
                rot.z = alpha % 360;
                break;
            default: break;
        }
        
        transform.localEulerAngles = rot;
    }
}
