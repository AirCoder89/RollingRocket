
using UnityEngine;
using UnityEngine.Analytics;

public class StartLevelTracker : MonoBehaviour {

    private AnalyticsTracker Tracker;
    void Start () {
        Tracker = GetComponent<AnalyticsTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ship")
        {
            if(SceneHandler.GetInstance().UseAnalytics) Tracker.TriggerEvent();

            Destroy(gameObject);
        }
    }
}
