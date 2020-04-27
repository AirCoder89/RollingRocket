using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour {

    //public Vector3 OutPos;
    public Transform OutPos;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            ShipController.Instance.SetPosition(OutPos.position);
            ObjectPooler.Instance.SpawnFromPool("TravelFX", OutPos.position, Quaternion.identity);
        }
    }

}
