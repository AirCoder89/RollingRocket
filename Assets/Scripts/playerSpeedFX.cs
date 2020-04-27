using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpeedFX : MonoBehaviour, IPooledObject
{

    SpriteRenderer spriteFX;
    [Range(0,20)] public float timer = 0.25f; //to destroy the FX after
    [Range(0,20)] public float Fadetime = 0.001f; 
    [ColorUsage(true)] public Color colorFX;

    // Use this for initialization
    public void OnObjectSpawn()
    {
        spriteFX = GetComponent<SpriteRenderer>();
       
        transform.position = ShipController.Instance.GetPosition();
        transform.localScale = ShipController.Instance.GetLocalScale();
        // spriteFX.sprite = PlayerScript.GetSprite().sprite;
        spriteFX.sprite = ShipController.Instance.GetSpriteRenderer().sprite;
       // spriteFX.color = new Vector4(8, 70, 100, 0.2f); //RGB Alpha
        spriteFX.color = new Vector4(colorFX.r, colorFX.g, colorFX.b, colorFX.a);
        spriteFX.sortingLayerName = "ShipLayer";
       
        //start fading out
        startFadingOut();
        //destroy
        //Destroy(gameObject, timer);
    }
	
    IEnumerator Reuse()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        for(float i = timer; i >= -Fadetime; i-= Fadetime)
        {
            Color c = spriteFX.material.color;
            c.a = i;
            spriteFX.material.color = c;
            yield return new WaitForSeconds(Fadetime);
        }
    }

    public void startFadingOut()
    {
        StartCoroutine("FadeOut");
        StartCoroutine("Reuse");
    }

}
