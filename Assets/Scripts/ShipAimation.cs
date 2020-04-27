using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAimation : MonoBehaviour {

    public float Xspeed;
    public float Frequence;
    public float RotationSpeed;
    private float angle;
    private Quaternion rotation;
    // Update is called once per frame
    void Update () {
        Vector3 oldPos = transform.position;
        oldPos.x += (Xspeed*Time.deltaTime);
        float Yspeed = Mathf.Cos(oldPos.x * 0.1f) * Frequence ;
        oldPos.y += (Yspeed * Time.deltaTime);
        transform.position = oldPos;

        angle = Mathf.Atan2(Yspeed, Xspeed) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

    }
}
