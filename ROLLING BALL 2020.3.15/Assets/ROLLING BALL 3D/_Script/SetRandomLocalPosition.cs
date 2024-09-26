using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomLocalPosition : MonoBehaviour {
    public Vector3[] localPositions;
    public bool X = true, Y = false, Z = false;

	// Use this for initialization
	void Awake () {
        Vector3 newPos = localPositions[Random.Range(0, localPositions.Length)];
        Vector3 currentPos = transform.localPosition;
        if (X)
            currentPos.x = newPos.x;
        if (Y)
            currentPos.y = newPos.y;
        if (Z)
            currentPos.z = newPos.z;

        transform.localPosition = currentPos;

    }
}
