
using UnityEngine;

public class BigHandDoor : MonoBehaviour {

	public void Open()
    {
        this.GetComponent<Animator>().SetTrigger("open");
    }
}
