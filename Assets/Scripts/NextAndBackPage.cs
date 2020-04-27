using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAndBackPage : MonoBehaviour {

    [SerializeField] private GameObject NextObj;
    [SerializeField] private GameObject BackObj;
    [SerializeField] private GameObject NextAndBackObj;
    [SerializeField] private GameObject PageIndexObj;

    private void Start()
    {
        
    }

    public void SetPageIndex(int index)
    {
        PageIndexObj.GetComponent<Animator>().SetInteger("page", index);
    }
    public void Next()
    {
        NextObj.SetActive(true);
        BackObj.SetActive(false);
        NextAndBackObj.SetActive(false);
    }
    public void Back()
    {
        NextObj.SetActive(false);
        BackObj.SetActive(true);
        NextAndBackObj.SetActive(false);
    }
    public void NextAndBack()
    {
        NextObj.SetActive(false);
        BackObj.SetActive(false);
        NextAndBackObj.SetActive(true);
    }
}
