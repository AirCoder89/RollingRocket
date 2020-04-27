
using UnityEngine;

public class SlowMotionScript : MonoBehaviour {
    //private bool isOnSlowMotion = false;
    [SerializeField] private bool MoveOffset = false;
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    public void UpdateMe(bool Status)
    {
        if (P1 != null && P2 != null)
        {
            P1.SetActive(Status);
            P2.SetActive(Status);
        }
        else
        {
            GetComponentInChildren<OcllusionZones>().StopOcllusion();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            KillParticleLine();
            ObjectPooler.Instance.SpawnFromPool("SlowMotionFX", ShipController.Instance.GetTransform().position, ShipController.Instance.GetTransform().rotation);
            LevelManager.Instance.StartSlowMotion(MoveOffset);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            LevelManager.Instance.EndSlowMotion();
            Destroy(gameObject);
        }
    }

    private void KillParticleLine()
    {
        //P1.SetActive(false);
       // P2.SetActive(false);
        Destroy(P1);
        Destroy(P2);
    }
}
