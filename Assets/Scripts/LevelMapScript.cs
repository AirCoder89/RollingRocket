
using UnityEngine;
using UnityEngine.UI;

public class LevelMapScript : MonoBehaviour {

    [SerializeField] private Transform Ship;
    [SerializeField] private Slider sliderBar;
    public float FinalPosition;
    private float Ratio = 0;
    
    public string GetProgress()
    {
        if((Ratio * 100) < 10)
        {
            return "0" + (Ratio * 100).ToString("0") + "%";
        }
        else
        {
            return (Ratio * 100).ToString("0") + "%";
        }
        
    }
   
	void Update () {
		if(ShipController.Instance == null)
        {
            return;
        }
      
            Ratio = Ship.position.x / FinalPosition;
            sliderBar.value = Ratio;
        
	}
}
