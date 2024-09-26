using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IListener {
    public static MenuManager Instance;

    public GameObject UI, StartMenu, AskUI, GameOverUI, ShopUI, AskWatchRewardedVideo;
    public GameObject ShopContainBall;
    public Button freeDiamondBut;
    public Text timeRespawnTxt;
    public Text runningDistance;
    public Text levelTxt;
    [Header("GameOver Panel")]
    public Text GameOverDistanceTxt;
    public Text GameOverBestDistanceTxt;

    [Header("Rate link")]
    public string androidLink = "";
    public string iosLink = "";

    public Text[] bestDistanceTxt;
    public Text[] diamondTxt;
    public Text[] rewardedAdTxt;

    private void Awake()
    {
        Instance = this;
        UI.SetActive(false);
        StartMenu.SetActive(true);
        AskUI.SetActive(false);
        GameOverUI.SetActive(false);
        ShopUI.SetActive(false);
        ShopContainBall.SetActive(false);
        AskWatchRewardedVideo.SetActive(false);
        timeRespawnTxt.gameObject.SetActive(false);
    }

    public void Play()
    {
        SoundManager.Click();
        GameManager.Instance.StartGame();
        UI.SetActive(true);
        StartMenu.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        if (GlobalValue.restartAndStartGameImmediately)
        {
            Play();
        }

        //if (SetupGameValue.Instance)
        
    }

    public void OpenRateURL()
    {
        SoundManager.Click();
#if UNITY_ANDROID
        Application.OpenURL(androidLink);
#elif UNITY_IOS
        Application.OpenURL(iosLink);
#endif
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var txt in diamondTxt)
        {
            txt.text = GlobalValue.StoreDiamond.ToString();
        }

        foreach (var txt in bestDistanceTxt)
        {
            txt.text = "BEST: " + GlobalValue.BestDistance + "M";
        }

        foreach (var txt in rewardedAdTxt)
        {
            if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
                txt.text = "FREE " + SetupGameValue.Instance.rewardedGemWatchAd;
            else
                txt.text = "No ad available now!";
        }

        freeDiamondBut.interactable = AdsManager.Instance && AdsManager.Instance.isRewardedAdReady();
        runningDistance.text = GameManager.Instance.ballDistance + "M";
        levelTxt.text = "LEVEL " + GameManager.Instance.currentLevel;
    }

    public void Restart()
    {
        SoundManager.Click();
        GlobalValue.restartAndStartGameImmediately = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void WatchVideoAd()
    {
        SoundManager.Click();
        if (AdsManager.Instance)
        {
            AdsManager.AdResult += AdsManager_AdResult;
            AdsManager.Instance.ShowRewardedAds();
        }
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        AdsManager.AdResult -= AdsManager_AdResult;
        //throw new System.NotImplementedException();
        if (isSuccess)
        {
            GlobalValue.StoreDiamond += AdsManager.Instance.getRewarded;
        }
    }

    public void ShowGameOverUI()
    {
        AskUI.SetActive(false);
        GameOverUI.SetActive(true);

        GameOverDistanceTxt.text = GameManager.Instance.ballDistance + "M";

        if (AdsManager.Instance)
            AdsManager.Instance.ShowNormalAd();

    }

    public void ShowAskWatchRewardedVideo()
    {
        if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
        {
            SoundManager.Click();
            AskWatchRewardedVideo.SetActive(true);
        }
    }

    public void HideAskWatchRewardedVideo()
    {
        SoundManager.Click();
        AskWatchRewardedVideo.SetActive(false);
    }

    public void OpenShop()
    {
        SoundManager.Click();
        StartMenu.SetActive(false);
        GameOverUI.SetActive(false);
        ShopUI.SetActive(true);
        ShopContainBall.SetActive(true);
    }

    public void CloseShop()
    {
        SoundManager.Click();
        if (GameManager.Instance.state == GameManager.State.Menu)
            StartMenu.SetActive(true);
        if (GameManager.Instance.state == GameManager.State.GameOver)
            GameOverUI.SetActive(true);
        ShopUI.SetActive(false);
        ShopContainBall.SetActive(false);
    }

    public void IPlay()
    {
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
        UI.SetActive(false);
        if (!GameManager.Instance.isUseRespawn)
        {
            GameManager.Instance.isUseRespawn = true;
            AskUI.SetActive(true);
        }
        else
        {
            ShowGameOverUI();
        }
    }

    public void IOnRespawn()
    {
        StartCoroutine(RespawnCo());
        //throw new System.NotImplementedException();
    }

    IEnumerator RespawnCo()
    {
        UI.SetActive(true);
        AskUI.SetActive(false);
        GameOverUI.SetActive(false);

        timeRespawnTxt.gameObject.SetActive(true);
        timeRespawnTxt.text = "3";
        yield return new WaitForSeconds(1);
        timeRespawnTxt.text = "2";
        yield return new WaitForSeconds(1);
        timeRespawnTxt.text = "1";
        yield return new WaitForSeconds(1);
        timeRespawnTxt.gameObject.SetActive(false);

        GameManager.Instance.Ball.IPlay();
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
