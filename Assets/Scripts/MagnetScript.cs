using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            //EventHandler.onHitMagnet_TR();
            LevelManager.Instance.HitMagnetBonus();
            ObjectPooler.Instance.SpawnFromPool("MagnetFX", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
