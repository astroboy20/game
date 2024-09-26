using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//xxx
public class GameManager : MonoBehaviour {
    public enum State { Menu, Playing, Falling, Pause, GameOver}
    [HideInInspector]
    public State state;
    public static GameManager Instance;
    [HideInInspector]
    public GameObject ballObj;
    [HideInInspector]
    public Vector3 vecBallHitPath;
    [HideInInspector]
    public float rateRotate = 15;
    [HideInInspector]
    public float speedBallAllow = 15;
    [HideInInspector]
    public float moveXMin, moveXMax;

    [HideInInspector]
    public BallController Ball;
    [HideInInspector]
    public CameraFollow cameraFollow;
    [HideInInspector]
    public bool isUseRespawn = false;
    [HideInInspector]
    public List<IListener> listeners;

    public int currentLevel { get; set; }

    //add listener called by late actived object
    public void AddListener(IListener _listener)
    {
        if (!listeners.Contains(_listener))     //check if this added or not
            listeners.Add(_listener);
    }
    //remove listener when Die or Disable
    public void RemoveListener(IListener _listener)
    {
        if (listeners.Contains(_listener))      //check if this added or not
            listeners.Remove(_listener);
    }

    

    public int ballDistance { get; set; }

    

    // Use this for initialization
    void Awake () {
        Instance = this;
        listeners = new List<IListener>();
        Ball = FindObjectOfType<BallController>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        currentLevel = 0;
    }

    public void StartGame()
    {
        state = State.Playing;

        //Get all objects that have IListener

        var listener_ = FindObjectsOfType<MonoBehaviour>().OfType<IListener>();
        foreach (var _listener in listener_)
        {
            listeners.Add(_listener);
        }

        foreach (var item in listeners)
        {
            item.IPlay();
        }
    }

    public void Gamepause()
    {
        state = State.Pause;
        foreach (var item in listeners)
            item.IPause();
    }

    public void UnPause()
    {
        state = State.Playing;
        foreach (var item in listeners)
            item.IUnPause();
    }

    public void BallFalling()
    {
        //Debug.LogError("BallFalling");
        state = State.Falling;

        Invoke("GameOver", 1);
    }

    public void GameOverDelay(float delay)
    {
        Invoke("GameOver", delay);
    }

    public void GameOver()
    {
        //Debug.LogError("GameOver");
        if (state == State.GameOver)
            return;

        state = State.GameOver;

        if (ballDistance > GlobalValue.BestDistance)
        {
            GlobalValue.BestDistance = ballDistance;
        }

        foreach (var item in listeners)
            item.IGameOver();

        
    }

    public void Continue()
    {
        state = State.Playing;
        foreach (var item in listeners)
            item.IOnRespawn();
    }
}
