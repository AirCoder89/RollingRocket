
using UnityEngine;
using System.Collections;
public class KillExploseFX : MonoBehaviour,IPooledObject {

    IEnumerator REUSE()
    {
        yield return new WaitForSeconds(GetComponentInChildren<ParticleSystem>().main.duration + GetComponentInChildren<ParticleSystem>().main.startLifetime.constantMax + 0.5f);
        gameObject.SetActive(false);
        // print("OBJ RESTORED");
    }
   

    public void OnObjectSpawn()
    {
        // print("FX Spawned");
        StartCoroutine("REUSE");
    }

}
