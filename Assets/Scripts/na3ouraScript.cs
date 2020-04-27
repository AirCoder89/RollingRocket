using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class na3ouraScript : MonoBehaviour {

    public string Direction;
    private bool isMoved = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShipParent")
        {
            Animator anim = GetComponentInParent<Animator>();
            if (!isMoved)
            {
                anim.SetTrigger(Direction);
                isMoved = true;
            }
        }
    }
}
