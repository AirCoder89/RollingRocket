using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlatform : MonoBehaviour {

	public int GetChildNumber()
    {
        return gameObject.transform.childCount;
    }
}
