using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcllusionZones : MonoBehaviour {
    public bool isNitroZone;
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
                if(isNitroZone)
                {
                    //Nitro Zone
                    GetComponentInParent<NitroScript>().UpdateMe(isActive);
                }
                else
                {
                    //Slow motion Zone
                    GetComponentInParent<SlowMotionScript>().UpdateMe(isActive);
                }
            }
        }
    }

    public void StopOcllusion()
    {
        ShouldCheck = false;
    }
}
