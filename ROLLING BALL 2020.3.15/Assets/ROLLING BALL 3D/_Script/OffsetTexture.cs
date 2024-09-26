using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTexture : MonoBehaviour {
    public float speed = 1;
    Material mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.LogError((Time.time * speed) % 1);
        mat.mainTextureOffset = new Vector2((Time.time * speed) % 1, 0);
    }
}
