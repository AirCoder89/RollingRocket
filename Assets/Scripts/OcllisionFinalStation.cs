using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllisionFinalStation : MonoBehaviour {
    public GameObject SystemParticle;
    private float distance;
    private float timeSinceLastCalled;
    private float delay = 2f;
    void Start()
    {
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 100f;
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
                bool isActive = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(ShipController.Instance.GetPosition().x)) < distance;
                GetComponent<BoxCollider2D>().enabled = isActive;
                SystemParticle.SetActive(isActive);

                if (transform.position.x < ShipController.Instance.GetPosition().x && !isActive)
                {
                    Destroy(gameObject);
                }
            }
        }

    }
}
