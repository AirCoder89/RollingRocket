using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseFXScript : MonoBehaviour {

    [SerializeField] private ParticleSystem ps;
    void Start () {
        ParticleSystem.VelocityOverLifetimeModule Vel = ps.velocityOverLifetime;
        Vel.y = (ShipController.Instance.GetAngle() * -1);
        Vel.orbitalX = -12;
    }
	
}
