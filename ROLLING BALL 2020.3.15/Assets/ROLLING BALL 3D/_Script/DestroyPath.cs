using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPath : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Path")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
