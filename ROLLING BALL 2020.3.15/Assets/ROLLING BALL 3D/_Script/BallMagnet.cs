using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dinamond")
        {
            if (other.GetComponent<MoveToPlayer_Magnet>() == null)
                other.gameObject.AddComponent<MoveToPlayer_Magnet>();
        }
    }
}
