using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskingContinueUI : MonoBehaviour {
    public Transform timerScale;
    public Button rewardedButton;
    public Button useGemButton;
    public float time = 3;
    float counting;
    public int diamondCost = 500;
    public Text diamondCostTxt;


    public Text diamondBonusTxt;

    // Use this for initialization
    void Start () {
        counting = time;
        diamondCostTxt.text = diamondCost + "";

        useGemButton.interactable = diamondCost <= GlobalValue.StoreDiamond;

        if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
            rewardedButton.interactable = true;
        else
            rewardedButton.interactable = false;

        if (!useGemButton.interactable && !rewardedButton.interactable)
            Skip();
    }

    // Update is called once per frame
    void Update()
    {
        if(AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
        {
            diamondBonusTxt.text = "Bonus +" + AdsManager.Instance.getBonusWatchAdWhenGameover;
        }else
            diamondBonusTxt.text = "NO VIDEO AD NOW!";

        if (counting > 0)
        {
            counting -= Time.deltaTime;
            counting = Mathf.Clamp(counting, 0, time);
            timerScale.localScale = new Vector3(counting / time, 1, 1);
        }
        else
            MenuManager.Instance.ShowGameOverUI();

        if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
            rewardedButton.interactable = true;
        else
            rewardedButton.interactable = false;
    }

    public void UseDiamond()
    {
        if(GlobalValue.StoreDiamond >= diamondCost)
        {
            SoundManager.Click();
            GlobalValue.StoreDiamond -= diamondCost;
            Continue();
        }
    }

    public void Continue()
    {
        GameManager.Instance.Continue();
    }

    public void Skip()
    {
        SoundManager.Click();
        MenuManager.Instance.ShowGameOverUI();
    }


    public void ShowRewardedAd()
    {
        AdsManager.AdResult += AdsManager_AdResult;
        AdsManager.Instance.ShowRewardedAds();
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        AdsManager.AdResult -= AdsManager_AdResult;
        if (isSuccess)
        {
            Debug.Log("The ad was successfully shown.");
            Continue();
            GlobalValue.StoreDiamond += AdsManager.Instance.getBonusWatchAdWhenGameover;
            SoundManager.PlaySfx(SoundManager.Instance.soundRewarded);
        }
        else
        {
            Debug.Log("The ad was skipped before reaching the end.");
        }
    }
}
