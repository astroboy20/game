using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour {
    public float delayLoadPlayingScene = 1;     //delay some time before go to the Playing scene
    public string loadSceneName = "Playing";
    public Text progressText;   //show percent loaded data

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadAsynchronously(loadSceneName));
    }

    IEnumerator LoadAsynchronously(string name)
    {
        progressText.text = "";

        yield return new WaitForSeconds(delayLoadPlayingScene);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }
}
