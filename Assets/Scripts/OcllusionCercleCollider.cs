using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllusionCercleCollider : MonoBehaviour {
    private float distance;
    private float timeSinceLastCalled;
    private float delay = 2f;

    [SerializeField] private bool SpriteInChild = true;
    void Start()
    {
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 35f;
    }
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
                GetComponent<CircleCollider2D>().enabled = isActive;


                //-- unComment this after setup the sprite Renderer !!
                /*if (SpriteInChild)
                {
                    GetComponentInChildren<SpriteRenderer>().enabled = isActive;
                }
                else
                {
                    GetComponent<SpriteRenderer>().enabled = isActive;
                }*/

                if (transform.position.x < ShipController.Instance.GetPosition().x && !isActive)
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}
