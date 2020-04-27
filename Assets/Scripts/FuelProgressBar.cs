using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelProgressBar : MonoBehaviour {
    
    private float Ratio = 0;
    private float currentFuelAmount;
    private float MaxFuel;
    private lowFuelScript FuelAnimation;
    private Image FillProgressBar;
    private Text IndicatorProgressBar;
	void Start () {
        FillProgressBar = GameObject.FindGameObjectWithTag("FillProgressBar").GetComponent<Image>();
        IndicatorProgressBar = GameObject.FindGameObjectWithTag("FuelIndicatorTxt").GetComponent<Text>();
        
      
    }

    public void InitProgressBar(float CurrentFuel)
    {
        this.currentFuelAmount = CurrentFuel;
        this.MaxFuel = SceneHandler.GetInstance().GetMaxFuel();
    }
    public void AddFuel(float fuelAmount)
    {
        currentFuelAmount += fuelAmount;
        if (currentFuelAmount > MaxFuel)
        {
            currentFuelAmount = MaxFuel;
        }
        
    }

    public void ConsumeFuel(float consumeAmount)
    {
        currentFuelAmount -= consumeAmount;

        if (currentFuelAmount < 0)
        {
            currentFuelAmount = 0;
            ShipController.Instance.SetFuelStatus(true);
        }
       
    }
    
    private void Update()
    {
        if(ShipController.Instance == null)
        {
            return;
        }
        else
        {
            Ratio = Mathf.Lerp(Ratio, this.currentFuelAmount / this.MaxFuel, 4f * Time.deltaTime);
            FillProgressBar.fillAmount = Ratio;
            IndicatorProgressBar.text = (Ratio * 100).ToString("0") + "%";

            // GameObject PanelLowFuel = GameObject.FindGameObjectWithTag("FuelPanelAnim");
            if (ShipController.Instance.isGameStarted)
            {
                if (Ratio * 100 <= 25)
                {
                    GameObject.FindGameObjectWithTag("FuelAnim").GetComponent<lowFuelScript>().LowFuel();

                    GameObject.FindGameObjectWithTag("FuelPanelAnim").GetComponent<LowFuelPanelGui>().LowFuel();
                }
                else if (Ratio * 100 > 25)
                {
                    GameObject.FindGameObjectWithTag("FuelAnim").GetComponent<lowFuelScript>().FullFuel();

                    GameObject.FindGameObjectWithTag("FuelPanelAnim").GetComponent<LowFuelPanelGui>().FullFuel();
                }
            }
        }
    }
}
