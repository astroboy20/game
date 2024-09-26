using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControllerUI : MonoBehaviour {
    public static ShopControllerUI Instance;

    [Header("MAGNET")]
    public int magnetPrice = 100;
    public Text magnetOwnTxt, magnetPriceTxt;

    [Header("SHIELD")]
    public int shieldPrice = 100;
    public Text shieldOwnTxt, shieldPriceTxt;

    [Header("X2")]
    public int x2Price = 100;
    public Text x2OwnTxt, x2PriceTxt;

    public BallItemPresent[] listBall;
    public BallItemPresent currentBallPresent { get; set; }

    public GameObject ballContainer;
    public float step = 2;
    public float moveSpeed = 2;
    public int balls = 8;
    Vector3 moveTo;
    float moveToX;
    float limitLeft, limitRight;

    public GameObject lockObj;
    public GameObject pickedObj;
    public GameObject gemIcon;
    public Text priceTxt, stateTxt;

    public int currentID = 0;

    public Vector3 shopTargetPos, shopTargetRot;
    public float moveCameraToShopSpeed = 10;
    

    bool isOpened = false;

    public void SetItem(BallItemPresent ball)
    {
        currentBallPresent = ball;
    }

    public void Click()
    {
        if (currentBallPresent.isUnlocked)
            Pick();
        else
            Buy();
    }

     void Buy()
    {
        if (currentBallPresent.price <= GlobalValue.StoreDiamond)
        {
            GlobalValue.StoreDiamond -= currentBallPresent.price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUnlockBall);
            GlobalValue.UnlockBall(currentBallPresent.ID);
            //GlobalValue.PickBall(currentBallPresent.ID);
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNoEnoughGem);
            MenuManager.Instance.ShowAskWatchRewardedVideo();
        }
    }

     void Pick()
    {
        if (currentBallPresent.ID != GlobalValue.ballPickedID)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundPickBall);
            //GlobalValue.PickBall(currentBallPresent.ID);
            GlobalValue.ballPickedID = currentBallPresent.ID;
            GameManager.Instance.Ball.SetBallSkin();
        }
    }

    private void OnEnable()
    {
        if (!isOpened)
            return;

        FindObjectOfType<CameraFollow>().MoveToShop(shopTargetPos, shopTargetRot, moveCameraToShopSpeed, true);
        ballContainer.SetActive(true);
    }

    private void OnDisable()
    {
        if (!isOpened)
            return;

        if (FindObjectOfType<CameraFollow>())
        {
            FindObjectOfType<CameraFollow>().MoveToShop(shopTargetPos, shopTargetRot, moveCameraToShopSpeed, false);
            ballContainer.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        Instance = this;
        currentBallPresent = listBall[currentID];
        moveTo = ballContainer.transform.position;
        moveToX = moveTo.x;
        limitLeft = moveToX - balls * step;
        limitRight = moveToX;

        FindObjectOfType<CameraFollow>().MoveToShop(shopTargetPos, shopTargetRot, moveCameraToShopSpeed, true);
        ballContainer.SetActive(true);
        isOpened = true;
    }
	
	// Update is called once per frame
	void Update () {
        currentBallPresent = listBall[currentID];

        lockObj.SetActive(!currentBallPresent.isUnlocked);
        pickedObj.SetActive(currentBallPresent.isPicked);
        gemIcon.SetActive(!currentBallPresent.isUnlocked);
        priceTxt.gameObject.SetActive(!currentBallPresent.isUnlocked);
        stateTxt.gameObject.SetActive(currentBallPresent.isUnlocked);

        priceTxt.text = currentBallPresent.price.ToString();
        stateTxt.text = currentBallPresent.isPicked ? "PICKED" : "CHOOSE";
        stateTxt.color = currentBallPresent.isPicked ? Color.yellow : Color.white;

        magnetPriceTxt.text = "" + magnetPrice;
        shieldPriceTxt.text = "" + shieldPrice;
        x2PriceTxt.text = "" + x2Price;

        magnetOwnTxt.text = "OWN: " + GlobalValue.StoreMagnet;
        shieldOwnTxt.text = "OWN: " + GlobalValue.StoreShield;
        x2OwnTxt.text = "OWN: " + GlobalValue.StoreX2;

        ballContainer.transform.position = Vector3.MoveTowards(ballContainer.transform.position, moveTo, moveSpeed * Time.deltaTime);
    }

    public void Next()
    {
        SoundManager.Click();
        currentID++;
        currentID = Mathf.Clamp(currentID, 0, listBall.Length - 1);
        moveToX -= step;
        moveToX = Mathf.Clamp(moveToX, limitLeft, limitRight);
        moveTo.x = moveToX;
    }

    public void Pre()
    {
        SoundManager.Click();
        currentID--;
        currentID = Mathf.Clamp(currentID, 0, listBall.Length - 1);
        moveToX += step;
        moveToX = Mathf.Clamp(moveToX, limitLeft, limitRight);
        moveTo.x = moveToX;
    }

    //BUY ITEM
    public void BuyMagnet() {
        if(magnetPrice <= GlobalValue.StoreDiamond)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundBuyItem);
            GlobalValue.StoreDiamond -= magnetPrice;
            GlobalValue.StoreMagnet++;
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNoEnoughGem);
            MenuManager.Instance.ShowAskWatchRewardedVideo();
        }
    }

    public void BuyShield() {
        if (shieldPrice <= GlobalValue.StoreDiamond)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundBuyItem);
            GlobalValue.StoreDiamond -= shieldPrice;
            GlobalValue.StoreShield++;
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNoEnoughGem);
            MenuManager.Instance.ShowAskWatchRewardedVideo();
        }
    }

    public void Buyx2() {
        if (x2Price <= GlobalValue.StoreDiamond)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundBuyItem);
            GlobalValue.StoreDiamond -= x2Price;
            GlobalValue.StoreX2++;
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNoEnoughGem);
            MenuManager.Instance.ShowAskWatchRewardedVideo();
        }
    }
}
