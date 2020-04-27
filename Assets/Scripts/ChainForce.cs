using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainForce : MonoBehaviour {
    public float Force;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Force, 0), ForceMode2D.Impulse);
	}
	
	
}
