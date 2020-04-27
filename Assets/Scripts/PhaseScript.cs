
using UnityEngine;

public class PhaseScript : MonoBehaviour {

    private float timeSinceLastCalled;
    private float delay = 3f;
    public bool isMainPhase;
    // Update is called once per frame
    void Update () {
        timeSinceLastCalled += Time.deltaTime;
        if (timeSinceLastCalled > delay)
        {
            timeSinceLastCalled = 0f;
            if(isMainPhase)
            {
                if (GetComponentInChildren<EndlessPlatform>().GetChildNumber() == 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (gameObject.transform.childCount == 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }

}
