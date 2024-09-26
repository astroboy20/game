using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEndPoint : MonoBehaviour {

    public GameObject parentBulding;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 playerPos = other.gameObject.transform.position;
            playerPos.x = 0;
            playerPos += new Vector3(0, -240, 210);
            parentBulding.transform.position = playerPos;
        }
    }
}
