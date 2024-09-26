using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer_Magnet : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Ball.transform.position, Time.deltaTime * 30);

	}
}
