
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Analytics;
public class CheckPointScript : MonoBehaviour {

    private AnalyticsTracker Tracker; 
    [SerializeField] private SpriteAtlas atlas;
    private SpriteRenderer myRenderer;
	public float XCheckPoint;
	public float YCheckPoint;
    private void Start()
    {
        Tracker = GetComponent<AnalyticsTracker>();
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sprite = atlas.GetSprite("CheckPointLine");
    }
    private void CheckedMe()
    {
        if (SceneHandler.GetInstance().UseAnalytics) Tracker.TriggerEvent();
        myRenderer.sprite = atlas.GetSprite("CheckPointGreen");
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ship")
        {
            //save Player X and Y
            SceneHandler.GetInstance().SetCheckPoint(this.gameObject.name, XCheckPoint, YCheckPoint);
            ObjectPooler.Instance.SpawnFromPool("CheckPtFX", new Vector3(transform.position.x, -0.15f,0), Quaternion.identity);
            CheckedMe();
        }
    }

}
