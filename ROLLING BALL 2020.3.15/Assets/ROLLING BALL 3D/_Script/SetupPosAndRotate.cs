using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPosAndRotate : MonoBehaviour {
    public bool setLocalPosX = true;
    public bool checkRotate = false;

    [Header("Rotation")]
    public float speed = 35;
    public float maxAngle = 20;

    float angle;

    bool allowRotatePingPong = false;
	// Use this for initialization
	void Start () {
        if (GameManager.Instance)
        {
            if (setLocalPosX)
            {
                float randX = Random.Range(GameManager.Instance.moveXMin, GameManager.Instance.moveXMax);
                Vector3 pos = transform.localPosition;
                pos.x = randX;
                transform.localPosition = pos;
            }

            if (checkRotate)
            {
                float rate = Random.Range(0, 100);
                if (rate <= GameManager.Instance.rateRotate)
                    allowRotatePingPong = true;


            }
        }else if(checkRotate)
            allowRotatePingPong = true;

        if (!allowRotatePingPong)
            Destroy(this);
	}

    private void Update()
    {
        if (allowRotatePingPong)
        {
            transform.localEulerAngles = new Vector3(0, Mathf.PingPong(Time.time * speed, maxAngle * 2) - maxAngle, 0);
        }
    }
}
