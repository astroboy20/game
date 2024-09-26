using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartBoxTrap : MonoBehaviour {
    public GameObject boxLeft, boxRight;
    public Vector3 localLeftPos = new Vector3(-0.425f, 0, 0);
    public Vector3 localRightPos = new Vector3(0.425f, 0, 0);

    public float moveSpeed = 4;
     float detectPlayerDistance = 6.5f;

    Vector3 oriLeftBoxPos, oriRightBoxPos;

    bool isWorking = false;
    float percent = 0;
    // Use this for initialization
    void Start () {
        oriLeftBoxPos = boxLeft.transform.localPosition;
        oriRightBoxPos = boxRight.transform.localPosition;

    }
	
	// Update is called once per frame
	void Update () {
        if (!isWorking)
        {
            if (Vector3.Distance(transform.position, GameManager.Instance.Ball.transform.position) < detectPlayerDistance)
            {
                isWorking = true;

            }
        }
        else
        {
            percent += Time.deltaTime * moveSpeed;
            percent = Mathf.Clamp01(percent);
            boxLeft.transform.localPosition = Vector3.Lerp(oriLeftBoxPos, localLeftPos, percent);
            boxRight.transform.localPosition = Vector3.Lerp(oriRightBoxPos, localRightPos, percent);

            if (percent == 1)
                Destroy(this);

        }
	}
}
