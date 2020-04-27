
using UnityEngine;

public class EndOfLevelScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ship")
        {
            EventHandler.LevelInTheEnd_TR();
        }
    }
}
