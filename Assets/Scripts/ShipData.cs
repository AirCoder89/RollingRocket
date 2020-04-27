
using UnityEngine;
using System.Collections.Generic;

public class ShipData {

    private List<float> SpeedRotationLevel;
    private List<float> ThrustLevel;
    private List<float> FuelConsumeLevel;
    private List<float> FuelTankLevel;
    private List<float> MagnetForceLevel;
    private List<float> SenstivityLevel;

    public ShipData()
    {
        //ResetToDefault();
        InitLevels();
    }

    private void ResetToDefault()
    {
        SetSpeedRotation(0);
        SetThrust(0);
        SetSenstivity(0);
        SetMagnet(0);
        SetFuelTank(0);
        SetFuelConsume(0);
    }
    public void InitLevels()
    {
        //Speed Rotation Levels
        SpeedRotationLevel = new List<float>();
        SpeedRotationLevel.Add(25f) ; //Level 01
        SpeedRotationLevel.Add(28f) ; //Level 02
        SpeedRotationLevel.Add(31f) ; //Level 03
        SpeedRotationLevel.Add(34f) ; //Level 04

        //Thrust Levels
        ThrustLevel = new List<float>();
        ThrustLevel.Add(0.3f); //Level 01
        ThrustLevel.Add(0.315f); //Level 02
        ThrustLevel.Add(0.325f); //Level 03
        ThrustLevel.Add(0.335f); //Level 04


        //Fuel Consume Levels
        FuelConsumeLevel = new List<float>();
        FuelConsumeLevel.Add(1.2f); //Level 01
        FuelConsumeLevel.Add(1f); //Level 02
        FuelConsumeLevel.Add(0.8f); //Level 03
        FuelConsumeLevel.Add(0.6f); //Level 04

        //Fuel Tank Levels
        FuelTankLevel = new List<float>();
        FuelTankLevel.Add(500f); //Level 01
        FuelTankLevel.Add(650f); //Level 02
        FuelTankLevel.Add(800f); //Level 03
        FuelTankLevel.Add(1000f); //Level 04

        //Magnet Force Levels
        MagnetForceLevel = new List<float>();
        MagnetForceLevel.Add(8.3f); //Level 01
        MagnetForceLevel.Add(10f); //Level 02
        MagnetForceLevel.Add(12f); //Level 03
        MagnetForceLevel.Add(15f); //Level 04

        //Senstivity Levels
        SenstivityLevel = new List<float>();
        SenstivityLevel.Add(1f); //Level 01
        SenstivityLevel.Add(2f); //Level 02
        SenstivityLevel.Add(3f); //Level 03
        SenstivityLevel.Add(4f); //Level 04

    }
    public float GetSpeedRotation(int Level)
    {
        return SpeedRotationLevel[Level];
    }
    public int GetLevelSpeedRotation()
    {
        return PlayerPrefs.GetInt("SpeedRotation", 0);
    }
    public void SetSpeedRotation(int Level)
    {
        PlayerPrefs.SetInt("SpeedRotation", Level);
    }


    public float GetThrust(int Level)
    {
        return ThrustLevel[Level];
    }
    public int GetLevelThrust()
    {
        return PlayerPrefs.GetInt("Thrust", 0);
    }
    public void SetThrust(int Level)
    {
        PlayerPrefs.SetInt("Thrust", Level);
    }


    public float GetMagnet(int Level)
    {
        return MagnetForceLevel[Level];
    }
    public int GetLevelMagnet()
    {
        return PlayerPrefs.GetInt("Magnet", 0);
    }
    public void SetMagnet(int Level)
    {
        PlayerPrefs.SetInt("Magnet", Level);
    }

    public float GetSenstivity(int Level)
    {
        return SenstivityLevel[Level];
    }
    public int GetLevelSenstivity()
    {
        return PlayerPrefs.GetInt("Senstivity", 0);
    }
    public void SetSenstivity(int Level)
    {
        PlayerPrefs.SetInt("Senstivity", Level);
    }

    public float GetFuelConsume(int Level)
    {
        return FuelConsumeLevel[Level];
    }
    public int GetLevelFuelConsume()
    {
        return PlayerPrefs.GetInt("FuelConsume", 0);
    }
    public void SetFuelConsume(int Level)
    {
        PlayerPrefs.SetInt("FuelConsume", Level);
    }


    public float GetFuelTank(int Level)
    {
        return FuelTankLevel[Level];
    }
    public int GetLevelFuelTank()
    {
        return PlayerPrefs.GetInt("FuelTank", 0);
    }
    public void SetFuelTank(int Level)
    {
        PlayerPrefs.SetInt("FuelTank", Level);
    }
}
