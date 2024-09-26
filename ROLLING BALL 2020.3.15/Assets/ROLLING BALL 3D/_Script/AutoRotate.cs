using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {
	public float speed = 100;
    float alpha = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        alpha += speed * Time.deltaTime;

        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = alpha % 360;
        transform.rotation = Quaternion.Euler(rot);
	}
}
