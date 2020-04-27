using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllusionReverZone : MonoBehaviour {

    private float distance;
    private float timeSinceLastCalled;
    private float delay = 2f;
    private bool ShouldCheck;
    void Start()
    {
        ShouldCheck = true;
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth * 2f, 0, 0)).x + 30f;
    }
    void Update()
    {
        if (ShipController.Instance == null || !ShouldCheck)
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
                GetComponentInParent<ReverseZoneScript>().UpdateMe(isActive);
            }
        }
    }

    public void StopOcllusion()
    {
        this.ShouldCheck = false;
    }
}
