using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllusionPlatform : MonoBehaviour {

    private float timeSinceLastCalled;
    private float delay = 2f;
    private float distance;
   // private float distanceToRemove;
    // Use this for initialization
    void Start()
    {
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 50f;
        //distanceToRemove = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x;
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

                //active or disactive
                bool isActive = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(ShipController.Instance.GetPosition().x)) < distance;
                GetComponent<EdgeCollider2D>().enabled = isActive;
                GetComponent<SpriteRenderer>().enabled = isActive;
                if (GetComponent<Animator>())
                {
                    GetComponent<Animator>().enabled = isActive;
                }
                timeSinceLastCalled = 0f;
               /* if(isActive)
                {
                    float width = GetComponent<SpriteRenderer>().bounds.size.x;
                    //destroy
                    if ((transform.position.x + width + width/2) < ShipController.Instance.GetPosition().x)
                    {
                       Destroy(gameObject);
                    }
                }*/
               
            }
        }

    }
}
