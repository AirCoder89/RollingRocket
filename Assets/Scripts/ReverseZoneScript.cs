using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseZoneScript : MonoBehaviour {

    [SerializeField] private GameObject StartP1;
    [SerializeField] private GameObject StartP2;
    [SerializeField] private GameObject FinishP1;
    [SerializeField] private GameObject FinishP2;
    private bool isPlayerInZone;
    private void Start()
    {
        isPlayerInZone = false;
    }
    public void UpdateMe(bool Status)
    {
        if(!isPlayerInZone)
        {
            if(StartP1 != null && StartP2 != null)
            {
                StartP1.SetActive(Status);
                StartP2.SetActive(Status);
            }
            if(FinishP1 != null && FinishP2 != null)
            {
                FinishP1.SetActive(Status);
                FinishP2.SetActive(Status);
            }
        }
        else
        {
            if (FinishP1 != null && FinishP2 != null)
            {
                FinishP1.SetActive(true);
                FinishP2.SetActive(true);
            }
        }
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")  isPlayerInZone = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            isPlayerInZone = true;
            print("SHIP ANGLE = " + ShipController.Instance.GetAngle());
            KillStartParticleLine();
            CameraVFX();
            ObjectPooler.Instance.SpawnFromPool("ReverseFX", ShipController.Instance.GetTransform().position, ShipController.Instance.GetTransform().rotation);
            ShipController.Instance.ReverseHandler(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ship")
        {
            isPlayerInZone = false;
            KillFinishParticleLine();
            CameraVFX();
            ObjectPooler.Instance.SpawnFromPool("ReverseFX", ShipController.Instance.GetTransform().position, ShipController.Instance.GetTransform().rotation);
            ShipController.Instance.ReverseHandler(false);
            Destroy(gameObject);
        }
    }

    private void KillStartParticleLine()
    {
        //StartP1.SetActive(false);
       // StartP2.SetActive(false);
        Destroy(StartP1);
        Destroy(StartP2);
    }

    private void KillFinishParticleLine()
    {
        //FinishP1.SetActive(false);
        //FinishP2.SetActive(false);
        Destroy(FinishP1);
        Destroy(FinishP2);
        GetComponentInChildren<OcllusionReverZone>().StopOcllusion();
    }

    private void CameraVFX()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipFollow>().StartReverseCameraFX();
    }
}
