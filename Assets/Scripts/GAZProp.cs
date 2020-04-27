using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class GAZProp : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ShipParent" || collision.gameObject.tag == "RigidObstacle")
        {
            // GameObject.FindGameObjectWithTag("shakeCam").GetComponent<CameraShaker>().ShakeOnceProp(1f, 6f, .1f, 1f);
            // StartCoroutine(Explose());
            print("/--------------- GAS ----------- /");
            //ShipController.Instance.ResetPosition();
            ShipController.Instance.RG.AddForce(Vector2.up * -25f,ForceMode2D.Impulse);
            ShipController.Instance.StartSmoke();
            explose();
        }
    }

    IEnumerator Explose()
    {
        float timeToExplose = Random.Range(0.1f, 0.6f);
        yield return new WaitForSeconds(timeToExplose);
        explose();
    }
    private void explose()
    {
        ObjectPooler.Instance.SpawnFromPool("ExploseFX", transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
