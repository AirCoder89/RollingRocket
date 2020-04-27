﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhaseScript : MonoBehaviour {

   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
         {
            EventHandler.GeneratePhase_TR();
        }
    }
}
