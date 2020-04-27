using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShipFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float Xoffset; //Ship x on the screen
    public float YScale; //lock at scale
    private float Smoothing = 120f;
    private float YOffset;
    public GameObject BuildingBG;
    private float timeSinceLastCalled;
    private float delay = 60f;
    void Start () {
        offset = new Vector3(0f, 0f, -15f);
        YScale = 4f;
    }
	
	// Update is called once per frame
	void LateUpdate() {
        Vector3 _pos = new Vector3(target.position.x + Xoffset, 0, 0) + offset;
        Vector3 smoothedPos = Vector3.Slerp(transform.position, _pos, 120f * Time.deltaTime);
        transform.position = smoothedPos;

        YOffset = target.position.y - (target.position.y / YScale);
        Vector3 lockatPos = new Vector3(target.position.x + Xoffset, target.position.y - YOffset, 0);
        transform.LookAt(lockatPos);

       timeSinceLastCalled += Time.deltaTime;
        if (timeSinceLastCalled > delay)
        {
            timeSinceLastCalled = 0f;
            BuildingBG.transform.position += new Vector3(700, 0, 0) ;
        }
    }
}
