using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorScript : MonoBehaviour {

    private Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //open door
        if (collision.gameObject.tag == "Ship") anim.SetTrigger("open");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //close door
        if (collision.gameObject.tag == "Ship") anim.SetTrigger("close");

    }
}
