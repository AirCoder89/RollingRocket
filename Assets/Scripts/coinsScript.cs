using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsScript : MonoBehaviour {

    private float speed = 8f;
   
    public void tweenTo(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            SceneHandler.GetInstance().HitCoin();
            
                ObjectPooler.Instance.SpawnFromPool("CoinFX", transform.position, Quaternion.identity);
                Destroy(gameObject);
        }
    }
}
