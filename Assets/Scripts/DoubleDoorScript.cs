using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorScript : MonoBehaviour {

    public string Direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            GetComponentInParent<DOUBLE_DOOR>().Open(Direction);
        }
    }
}
