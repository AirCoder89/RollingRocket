using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour {
    public GameObject sceneHandler;
    // Use this for initialization
    void Start () {
        Instantiate(sceneHandler, Vector3.zero, Quaternion.identity);
        StartCoroutine(Loading());
    }
    
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(2f);
        LoadingBarScript.Instance.LoadScene("MainMenu", "MainMenu");
    }
}
