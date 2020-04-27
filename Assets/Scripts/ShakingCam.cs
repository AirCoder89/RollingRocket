using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ShakingCam : MonoBehaviour {

    CameraShaker CShaker;
	// Use this for initialization
	void Start () {
        CShaker = GetComponent<CameraShaker>();
        EventHandler.onShipDieEvent += Shake;
	}

    public void Shake()
    {
        print("SHAKE SHIP DIE");
        if(SceneHandler.GetInstance().Settings.GetCameraShakeStatus())
        {
            if (!ShipController.Instance.isLevelWin)
            {
                CShaker.ShakeOnce(1f, 6f, 0.1f, 1f);
            }
                
        }
    }
    

}
