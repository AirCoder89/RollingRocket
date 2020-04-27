
using UnityEngine;

public class ScaleBigScript : MonoBehaviour {

	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            LevelManager.Instance.HitBigScaleBonus();
            ObjectPooler.Instance.SpawnFromPool("ScaleFX", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
