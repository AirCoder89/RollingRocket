using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowFuelScript : MonoBehaviour {

    
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
   
	public void LowFuel()
    {
        anim.enabled = true;
        anim.SetTrigger("low");
    }

    public void FullFuel()
    {
        anim.SetTrigger("full");
    }
}
