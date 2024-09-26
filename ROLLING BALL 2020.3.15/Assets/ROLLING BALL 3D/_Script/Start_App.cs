using UnityEngine;
using System.Collections;
using StartApp;

public class Start_App : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	//void OnGUI () {
	//	#if UNITY_ANDROID
	//	StartAppWrapper.showSplash();
	//	#endif
	//}

	void ShowInterstital(){
		#if UNITY_ANDROID
		StartAppWrapper.showAd();
		StartAppWrapper.loadAd();
		#endif
	}
	
	void LoadInterstital(){
		#if UNITY_ANDROID
		StartAppWrapper.init();
		StartAppWrapper.loadAd();
		#endif
	}
}
