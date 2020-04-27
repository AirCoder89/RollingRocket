using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationComplete : MonoBehaviour {

	public void onAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
