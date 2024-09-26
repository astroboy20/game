using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItemPresent : MonoBehaviour {
    public int ID = 0;
    public int price = 100;
    public float scaleCenter = 2;
    public float scaleSpeed = 3;
    public bool isUnlocked = false;
    public bool isPicked = false;

    float scale;

    // Update is called once per frame
    void Update()
    {
        if (ShopControllerUI.Instance.currentBallPresent.ID == ID)
        {
            scale += scaleSpeed * Time.deltaTime;
        }
        else
            scale -= scaleSpeed * Time.deltaTime;

        scale = Mathf.Clamp(scale, 1, scaleCenter);

        transform.localScale = new Vector3(scale, scale, scale);
        if (!isUnlocked)
            isUnlocked = GlobalValue.isBallUnlocked(ID);

        isPicked = ID == GlobalValue.ballPickedID;



    }
}
