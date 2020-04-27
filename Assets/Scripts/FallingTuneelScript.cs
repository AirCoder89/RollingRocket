using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTuneelScript : MonoBehaviour {

    //[SerializeField] private Rigidbody2D RBTunnel;
    [SerializeField] private GameObject Falling;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            //if(!RBTunnel.simulated) RBTunnel.simulated = true;
            Falling.AddComponent<Rigidbody2D>();
            Falling.GetComponent<Rigidbody2D>().gravityScale = 5;
        }
    }
}
