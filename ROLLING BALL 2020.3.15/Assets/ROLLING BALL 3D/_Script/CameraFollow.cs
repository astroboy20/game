using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IListener {
    public Vector3 beginPos, beginRot;
    Vector3 offset;
    Vector3 originalRotation;

	// Use this for initialization
	void Start () {
        transform.position = beginPos;
        transform.rotation = Quaternion.Euler(beginRot);

        originalRotation = transform.rotation.eulerAngles;
        offset = transform.position - GameManager.Instance.Ball.transform.position;
	}

    public void MoveToShop(Vector3 pos, Vector3 rot, float speed, bool moveToShop)
    {
        if (moveToShop)
            StartCoroutine(MoveToShopCo(pos, rot, speed));
        else
            StartCoroutine(MoveBackFromShopCo(pos, rot, speed));
    }

    Vector3 startPos;
    Vector3 startRot;
    IEnumerator MoveToShopCo(Vector3 pos, Vector3 rot, float speed)
    {
        float percent = 0;
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;

        while (percent < 1)
        {
            percent += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pos, percent);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, rot, percent));
            yield return null;
        }
    }

    IEnumerator MoveBackFromShopCo(Vector3 pos, Vector3 rot, float speed)
    {
        float percent = 0;
        //startPos = transform.position;
        //startRot = transform.rotation.eulerAngles;

        while (percent < 1)
        {
            percent += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(pos, startPos, percent);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(rot, startRot, percent));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
		if(GameManager.Instance.state == GameManager.State.Falling)
        {
            transform.LookAt(GameManager.Instance.Ball.transform);
        }
	}

    private void FixedUpdate()
    {
        //Debug.LogError("CAM");
        //if (GameManager.Instance.state != GameManager.State.Playing)
        //    return;

        //transform.position = GameManager.Instance.Ball.transform.position + offset;
    }

    //Called by Ball to prevent error if FixedUpdate of Camera excuse before ball's
    public void FollowBall()
    {
        transform.position = GameManager.Instance.Ball.transform.position + offset;
    }

    public void IPlay()
    {
        //throw new System.NotImplementedException();
    }

    public void ISuccess()
    {
        //throw new System.NotImplementedException();
    }

    public void IPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IUnPause()
    {
        //throw new System.NotImplementedException();
    }

    public void IGameOver()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnRespawn()
    {
        transform.rotation = Quaternion.Euler(originalRotation);
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOn()
    {
        //throw new System.NotImplementedException();
    }

    public void IOnStopMovingOff()
    {
        //throw new System.NotImplementedException();
    }
}
//xxx
