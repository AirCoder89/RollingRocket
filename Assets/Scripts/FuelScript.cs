
using UnityEngine;

public class FuelScript : MonoBehaviour {
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            LevelManager.Instance.HitFuel();
             ObjectPooler.Instance.SpawnFromPool("FuelFX", transform.position, Quaternion.identity);
           // Instantiate(FuelFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
			
            
        }
    }
}
