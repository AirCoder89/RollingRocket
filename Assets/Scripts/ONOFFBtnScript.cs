
using UnityEngine;


public class ONOFFBtnScript : MonoBehaviour {

    
    [SerializeField] private GameObject ONImage;
    [SerializeField] private GameObject OFFImage;
    public void UpdateBtn(bool status)
    {
        ONImage.SetActive(status);
        OFFImage.SetActive(!status);
    }

}
