using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocllusionTravel : MonoBehaviour {

    public GameObject SystemParticle;
    private float distance;
    private float timeSinceLastCalled;
    private float delay = 2f;
    public bool isEdgeInChildren;
    void Start () {
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 35f;
    }
	
	// Update is called once per frame
	void Update () {
        if (ShipController.Instance == null)
        {
            return;
        }
        else
        {
            timeSinceLastCalled += Time.deltaTime;
            if (timeSinceLastCalled > delay)
            {
                bool isActive = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(ShipController.Instance.GetPosition().x)) < distance;
                if(isEdgeInChildren)
                {
                    GetComponentInChildren<EdgeCollider2D>().enabled = isActive;
                    GetComponent<CapsuleCollider2D>().enabled = isActive;
                }
                else
                {
                    GetComponent<EdgeCollider2D>().enabled = isActive;
                }
                
                GetComponent<SpriteRenderer>().enabled = isActive;
                GetComponentInChildren<SpriteRenderer>().enabled = isActive;
                GetComponentInChildren<Animator>().enabled = isActive;
                SystemParticle.SetActive(isActive);
            }
        }
    }
}
