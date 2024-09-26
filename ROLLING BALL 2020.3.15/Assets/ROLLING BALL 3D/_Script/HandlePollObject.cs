using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePollObject : MonoBehaviour {
    public static HandlePollObject Instance;
    public GameObject diamondFX;
    public int numberSpawned = 10;

    List<GameObject> listObj = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		for(int i = 0; i < numberSpawned; i++)
        {
            GameObject obj = Instantiate(diamondFX, transform) as GameObject;
            obj.SetActive(false);
            listObj.Add(obj);
        }
	}

    public GameObject GetAvailableObj(bool setActive)
    {
        foreach(var obj in listObj)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(setActive);
                return obj;
            }
        }

        //if no available obj then spawn more
        GameObject newObj = Instantiate(diamondFX, transform) as GameObject;
        newObj.SetActive(setActive);
        listObj.Add(newObj);
        return newObj;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
