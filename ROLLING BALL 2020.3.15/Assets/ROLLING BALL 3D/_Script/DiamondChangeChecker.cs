using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondChangeChecker : MonoBehaviour {
    int oldValue;
    public Animator anim;
	// Use this for initialization
	void Start () {
        oldValue = GlobalValue.StoreDiamond;

    }
	
	// Update is called once per frame
	void Update () {
		if(oldValue != GlobalValue.StoreDiamond)
        {
            oldValue = GlobalValue.StoreDiamond;
            anim.SetTrigger("collect");
        }
	}
}
