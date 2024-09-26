using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGameValue : MonoBehaviour {
    public static SetupGameValue Instance;

    [Header("UNITY VIDEO AD")]
    public int rewardedGemWatchAd = 200;
    public int showAdWhenGameoverTimes = 3;

	// Use this for initialization
	void Start () {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
