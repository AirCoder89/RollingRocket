using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllusionCoins : MonoBehaviour {
    private float timeSinceLastCalled;
    private float delay = 2f;

    private float distance;
    
    // Use this for initialization
    void Start()
    {
       // Player = GameObject.FindGameObjectWithTag("ShipParent");
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 35f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipController.Instance == null)
        {
            return;
        }
        else
        {
            timeSinceLastCalled += Time.deltaTime;
            if (timeSinceLastCalled > delay)
            {
                timeSinceLastCalled = 0f;

                bool isActive = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(ShipController.Instance.GetTransform().position.x)) < distance; ;
                GetComponent<CapsuleCollider2D>().enabled = isActive;

                GetComponentInChildren<SpriteRenderer>().enabled = isActive;

                if (transform.position.x < ShipController.Instance.GetPosition().x && !isActive)
                {
                    Destroy(gameObject);
                }
            }
        }
           
    }
}
