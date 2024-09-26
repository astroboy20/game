using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IListener {
    [Header("Shield")]
    public Text shieldTxt;
    public Button shieldBut;
    public float shieldCoolDown = 30;
    public RectTransform shieldTimerRec;
    public Image shieldTimerImage;
    public Text shieldTimerTxt;
    float shieldTimer = 0;

    [Header("Magnet")]
    public Text magnetTxt;
    public Button magnetBut;
    public float magnetCoolDown = 30;
    public RectTransform magnetTimerRec;
    public Image magnetTimerImage;
    public Text magnetTimerTxt;
    float magnetTimer = 0;

    [Header("X2")]
    public Text x2Txt;
    public Button x2But;
    public float x2CoolDown = 30;
    public RectTransform x2TimerRec;
    public Image x2TimerImage;
    public Text x2TimerTxt;
    float x2Timer = 0;

    bool cooldownMagnet = false;
    float cooldownMagnetCounter = 0;
    bool cooldownShield = false;
    float cooldownShieldCounter = 0;
    bool cooldownX2 = false;
    float cooldownX2Counter = 0;

    private void Start()
    {
        shieldTimerImage.fillAmount = 0;
        magnetTimerImage.fillAmount = 0;
        x2TimerImage.fillAmount = 0;

        shieldTimerRec.localScale = new Vector3(0, 1, 1);
        magnetTimerRec.localScale = new Vector3(0, 1, 1);
        x2TimerRec.localScale = new Vector3(0, 1, 1);

        GameManager.Instance.AddListener(this);
    }

    // Use this for initialization
    void Update () {
        shieldTxt.text = GlobalValue.StoreShield + "";
        magnetTxt.text = GlobalValue.StoreMagnet + "";
        x2Txt.text = GlobalValue.StoreX2 + "";

        shieldBut.interactable = GlobalValue.StoreShield > 0;
        magnetBut.interactable = GlobalValue.StoreMagnet > 0;
        x2But.interactable = GlobalValue.StoreX2 > 0;

        if (GameManager.Instance.Ball.isUsingShield)
        {
            shieldTimerRec.gameObject.SetActive(true);
            shieldTimer += Time.deltaTime;
            //shieldTimer = Mathf.Clamp01(shieldTimer);
            shieldTimerRec.localScale = new Vector3(1 - shieldTimer / GameManager.Instance.Ball.shieldTime, 1, 1);
            //shieldTimerImage.fillAmount = 1 - shieldTimer / GameManager.Instance.Ball.shieldTime;
        }
        else if (cooldownShield)
        {
            shieldTimerRec.gameObject.SetActive(false);
            shieldTimerTxt.gameObject.SetActive(true);
            cooldownShieldCounter += Time.deltaTime;
            shieldTimerImage.fillAmount = 1 - cooldownShieldCounter / shieldCoolDown;

            shieldTimerTxt.text = (int)(shieldCoolDown - cooldownShieldCounter) + "";

            if (cooldownShieldCounter >= shieldCoolDown)
                cooldownShield = false;
        }
        else
        {
            shieldTimerRec.gameObject.SetActive(false);
            shieldTimerImage.fillAmount = 0;
            shieldTimerTxt.gameObject.SetActive(false);
        }

        if (GameManager.Instance.Ball.isUsingMagnet)
        {
            magnetTimerRec.gameObject.SetActive(true);
            magnetTimer += Time.deltaTime;
            //magnetTimer = Mathf.Clamp01(magnetTimer);
            magnetTimerRec.localScale = new Vector3(1 - magnetTimer / GameManager.Instance.Ball.magnetTime, 1, 1);
            //magnetTimerImage.fillAmount = 1 - magnetTimer / GameManager.Instance.Ball.magnetTime;
        }
        else if (cooldownMagnet)
        {
            magnetTimerRec.gameObject.SetActive(false);
            magnetTimerTxt.gameObject.SetActive(true);
            cooldownMagnetCounter += Time.deltaTime;
            magnetTimerImage.fillAmount = 1 - cooldownMagnetCounter / magnetCoolDown;

            magnetTimerTxt.text = (int)(magnetCoolDown - cooldownMagnetCounter) + "";

            if (cooldownMagnetCounter >= magnetCoolDown)
                cooldownMagnet = false;
        }
        else
        {
            magnetTimerRec.gameObject.SetActive(false);
            magnetTimerImage.fillAmount = 0;
            magnetTimerTxt.gameObject.SetActive(false);
        }

        if (GameManager.Instance.Ball.isUsingX2Item)
        {
            x2TimerRec.gameObject.SetActive(true);
            x2Timer += Time.deltaTime;
            //x2Timer = Mathf.Clamp01(x2Timer);
            x2TimerRec.localScale = new Vector3(1 - x2Timer / GameManager.Instance.Ball.x2Time, 1, 1);
            //x2TimerImage.fillAmount = 1 - x2Timer / GameManager.Instance.Ball.x2Time;
        }
        else if (cooldownX2)
        {
            x2TimerRec.gameObject.SetActive(false);
            x2TimerTxt.gameObject.SetActive(true);
            cooldownX2Counter += Time.deltaTime;
            x2TimerImage.fillAmount = 1 - cooldownX2Counter / x2CoolDown;

            x2TimerTxt.text = (int)(x2CoolDown - cooldownX2Counter) + "";

            if (cooldownX2Counter >= x2CoolDown)
                cooldownX2 = false;
        }
        else
        {
            x2TimerRec.gameObject.SetActive(false);
            x2TimerImage.fillAmount = 0;
            x2TimerTxt.gameObject.SetActive(false);
        }
    }

    

    public void ActiveMagnet()
    {
        if (GameManager.Instance.Ball.isUsingMagnet || cooldownMagnet)
            return;

        magnetTimer = 0;
        GlobalValue.StoreMagnet--;
        GameManager.Instance.Ball.ActiveMagnet();
        cooldownMagnet = true;
        cooldownMagnetCounter = 0;
        magnetTimerImage.fillAmount = 1;
    }



    public void ActiveShield()
    {
        if (GameManager.Instance.Ball.isUsingShield || cooldownShield)
            return;

        shieldTimer = 0;
        GlobalValue.StoreShield--;
        GameManager.Instance.Ball.ActiveShield();
        cooldownShield = true;
        cooldownShieldCounter = 0;
        shieldTimerImage.fillAmount = 1;
    }

    public void Activex2()
    {
        if (GameManager.Instance.Ball.isUsingX2Item || cooldownX2)
            return;

        x2Timer = 0;
        GlobalValue.StoreX2--;
        GameManager.Instance.Ball.ActiveX2();
        cooldownX2 = true;
        cooldownX2Counter = 0;
        x2TimerImage.fillAmount = 1;
    }

    public void IPlay()
    {
    }

    public void ISuccess()
    {
    }

    public void IPause()
    {
    }

    public void IUnPause()
    {
    }

    public void IGameOver()
    {
        
    }

    public void IOnRespawn()
    {
        cooldownShield = false;
        cooldownMagnet = false;
        cooldownX2 = false;
    }

    public void IOnStopMovingOn()
    {
    }

    public void IOnStopMovingOff()
    {
    }
}
