using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GiftAnimation : MonoBehaviour
{
    [SerializeField] Animator MainAnimation;
    [SerializeField] AllCoinsGUIAnim CoinsAnimation;
    [SerializeField] TextMeshProUGUI CoinsValue;
    [SerializeField] GameObject UIText;
    public int GiftValue;
    void Start()
    {
        Destroy(gameObject, 3.65f);
        MainAnimation.SetTrigger("play");
        StartCoroutine(animationCoins(GiftValue));
    }
   
    IEnumerator animationCoins(int G)
    {
        yield return new WaitForSeconds(0.9f);
        UIText.SetActive(true);
        CoinsValue.text = "+ " + G.ToString();
        CoinsAnimation.gameObject.SetActive(true);
        CoinsAnimation.PlayAnimation();
    }
    IEnumerator CloseME()
    {
        yield return new WaitForSeconds(3.5f);
        gameObject.transform.parent = null;
        UIText.SetActive(false);
        gameObject.SetActive(false);
    }
}
