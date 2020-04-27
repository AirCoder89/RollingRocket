
using UnityEngine;

public class ScaleSmallScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            LevelManager.Instance.HitSmallScaleBonus();
            ObjectPooler.Instance.SpawnFromPool("ScaleFX", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
