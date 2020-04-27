using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGUIAnimationGift : MonoBehaviour {

    public static PlayGUIAnimationGift Instance;
    public GameObject GiftAnim;
    private void Start()
    {
        Instance = this;
    }

    public void PlayGiftAnimation(int Gift)
    {
        GameObject giftanim;
       
       // giftanim = ObjectPooler.Instance.SpawnFromPool("GiftAnimation", Vector3.zero, transform.rotation) as GameObject;
        giftanim = Instantiate(GiftAnim, Vector3.zero, transform.rotation) as GameObject;
        giftanim.transform.SetParent(GameObject.FindGameObjectWithTag("GUISpawn").transform , false);
        GiftAnimation Anim = giftanim.GetComponent<GiftAnimation>();
        Anim.GiftValue = Gift;
    }


}
