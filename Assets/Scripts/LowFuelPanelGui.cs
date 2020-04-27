using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowFuelPanelGui : MonoBehaviour {

    private Animator anim;
    [SerializeField] private GameObject img;
    [SerializeField] private GameObject txt;
    private bool isEnabled;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
       // img = GetComponent<Image>();
       // txt = GetComponent<Text>();
        Disable();
    }
    public void LowFuel()
    {
        if(!isEnabled) Enable();
        anim.SetTrigger("low");
    }

    public void FullFuel()
    {
        if (!isEnabled) Enable();
        anim.SetTrigger("full");
    }

    public void Disable()
    {
        isEnabled = false;
        img.SetActive(false);
        txt.SetActive(false);
    }

    public void Enable()
    {
        img.SetActive(true);
        txt.SetActive(true);
        isEnabled = true;
    }
}
