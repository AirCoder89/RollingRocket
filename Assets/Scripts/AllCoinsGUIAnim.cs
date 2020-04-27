using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCoinsGUIAnim : MonoBehaviour {

	public void PlayAnimation()
    {
        Animator[] coinsAnim = GetComponentsInChildren<Animator>();
        foreach (Animator anim in coinsAnim)
        {
            anim.SetTrigger("play");
        }
    }
}
