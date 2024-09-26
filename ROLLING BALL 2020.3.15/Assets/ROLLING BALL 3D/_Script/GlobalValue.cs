using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour {
    public static bool restartAndStartGameImmediately = false;

    public static int StoreDiamond
    {
        set { PlayerPrefs.SetInt("Diamond", value); }
        get { return PlayerPrefs.GetInt("Diamond", 100); }
    }

    public static int BestDistance
    {
        set { PlayerPrefs.SetInt("BestDistance", value); }
        get { return PlayerPrefs.GetInt("BestDistance", 0); }
    }

    public static int StoreMagnet
    {
        set { PlayerPrefs.SetInt("StoreMagnet", value); }
        get { return PlayerPrefs.GetInt("StoreMagnet", 2); }
    }

    public static int StoreShield
    {
        set { PlayerPrefs.SetInt("StoreShield", value); }
        get { return PlayerPrefs.GetInt("StoreShield", 2); }
    }

    public static int StoreX2
    {
        set { PlayerPrefs.SetInt("StoreX2", value); }
        get { return PlayerPrefs.GetInt("StoreX2", 2); }
    }

    public static void UnlockBall (int id)
    {
        PlayerPrefs.SetInt("UnlockBall" + id, 1); 
    }

    public static bool isBallUnlocked(int id)
    {
        return PlayerPrefs.GetInt("UnlockBall" + id, 0) == 1 ? true : false;
    }

    //public static void PickBall(int id)
    //{
    //    PlayerPrefs.SetInt("PickBall" + id, 1);
    //}

    //public static bool isBallPicked(int id)
    //{
    //    return PlayerPrefs.GetInt("PickBall" + id, 0) == 1 ? true : false;
    //}

    public static int ballPickedID
    {
        set { PlayerPrefs.SetInt("ballPickedID", value); }
        get { return PlayerPrefs.GetInt("ballPickedID", 0); }
    }
}
