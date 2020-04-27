using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReuseCoinsFX : MonoBehaviour,IPooledObject {

   
    IEnumerator REUSE()
    {
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration + GetComponentInChildren<ParticleSystem>().main.startLifetime.constantMax);
        gameObject.SetActive(false);
       // print("OBJ RESTORED");
    }
   
    public void OnObjectSpawn() {
       // print("FX Spawned");
        StartCoroutine("REUSE");
    }
   
    
}
