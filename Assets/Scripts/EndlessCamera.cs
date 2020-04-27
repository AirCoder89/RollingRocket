using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCamera : MonoBehaviour {

    [SerializeField] private Transform UpBlock;
    [SerializeField] private Transform DownBlock;
	
    public void SetPosition(float Xpos)
    {
        UpBlock.position = new Vector3(Xpos,UpBlock.position.y,0);
        DownBlock.position = new Vector3(Xpos, DownBlock.position.y, 0);
    }
}
