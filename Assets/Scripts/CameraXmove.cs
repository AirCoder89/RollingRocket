using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXmove : MonoBehaviour {
    public float speed;
    public float smooth;
	
	void Update () {
        Vector3 newpos = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newpos, smooth * Time.deltaTime);

    }
}
