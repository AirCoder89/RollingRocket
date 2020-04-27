using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {

    [SerializeField] private BigHandDoor bigHand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            GetComponent<Animator>().SetTrigger("open");
            bigHand.Open();
        }
    }
   
}
