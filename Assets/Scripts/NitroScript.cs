using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroScript : MonoBehaviour {
    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    public void UpdateMe(bool Status)
    {
        if(P1 != null && P2 != null)
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
            print("SHIP ANGLE = " + ShipController.Instance.GetAngle());
            KillParticleLine();
            ObjectPooler.Instance.SpawnFromPool("NitroFX", ShipController.Instance.GetTransform().position, ShipController.Instance.GetTransform().rotation);
            LevelManager.Instance.StartNitro();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            LevelManager.Instance.StopNitro();
            Destroy(gameObject);
        }
    }

    private void KillParticleLine()
    {
       // P1.SetActive(false);
       // P2.SetActive(false);
        Destroy(P1);
        Destroy(P2);
    }
}
