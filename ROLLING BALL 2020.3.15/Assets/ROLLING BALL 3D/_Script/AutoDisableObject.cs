using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableObject : MonoBehaviour {
    public bool destroy = false;
    public float delay = 1;

	// Use this for initialization
	void OnEnable () {
        Invoke("Work", delay);
	}
	
	// Update is called once per frame
	void Work () {
        if (destroy)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
	}
}
