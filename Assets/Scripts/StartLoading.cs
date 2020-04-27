using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoadingBarScript.Instance.LoadScene("MainMenu", "mainMenu");
    }
	
}
