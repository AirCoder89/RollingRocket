using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOUBLE_DOOR : MonoBehaviour {

    [SerializeField] private Animator animDoor1;
    [SerializeField] private Animator animDoor2;

    public void Open(string Direction)
    {
        animDoor1.SetTrigger(Direction);
        animDoor2.SetTrigger(Direction);
    }

}
