using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CongratsMessage : MonoBehaviour {

    [SerializeField] private Animator Animation;
    [SerializeField] private TextMeshProUGUI ValueTxt;
	
    public void OpenCongratsWatchVideo(int value)
    {
        if (Animation != null) Animation.gameObject.SetActive(true);
        else GetComponentInChildren<Animator>().gameObject.SetActive(true);
        ValueTxt.text = value.ToString() + " COINS";
        if (Animation != null) Animation.SetTrigger("open");
        else GetComponentInChildren<Animator>().SetTrigger("open");
    }

    public void OpenCongratsIAPCoins(int value)
    {
        if (Animation != null)  Animation.gameObject.SetActive(true);
        else GetComponentInChildren<Animator>().gameObject.SetActive(true);

        ValueTxt.text = value.ToString() + " COINS";
        if (Animation != null) Animation.SetTrigger("open");
        else GetComponentInChildren<Animator>().SetTrigger("open");
    }

    public void PressOK()
    {
        if (Animation != null) Animation.SetTrigger("close");
        else GetComponentInChildren<Animator>().SetTrigger("close");
    }
}
