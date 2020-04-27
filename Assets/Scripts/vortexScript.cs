using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vortexScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Transition");
	}
	IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        //save level coins and coins bonus
        SceneHandler.GetInstance().UpdateCoins();
        //goto level win scene
        LoadingBarScript.Instance.LoadScene("LevelWin", "win");

    }

    
}
