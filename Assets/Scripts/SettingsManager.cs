using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    private bool ShowADS = true;
    private float DefaultMusicVol = 0.7f;
    private bool DefaultSoundFX = true;
    private bool DefaultCameraShake = true;
    private string DefaultGameController = "tap";
    private string DefaultButtonPosition = "right";
    //***************** GET SET ******************************
    // -- Get
    public bool isShowADS()
    {
        if (PlayerPrefs.GetInt("ShowAds", 1) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetShowADS(bool status)
    {
        if(status)
        {
            PlayerPrefs.SetInt("ShowAds", 1);
        }else
        {
            PlayerPrefs.SetInt("ShowAds", 0);
        }
    }
    public string GetButtonPosition()
    {
        return PlayerPrefs.GetString("ButtonPosition", DefaultButtonPosition);
    }
    public string GetGameController()
    {
        return PlayerPrefs.GetString("GameController", DefaultGameController);
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", DefaultMusicVol);
    }
    public bool GetSoundEffectStatus()
    {
        int defaultSoundFX;
        if (DefaultSoundFX) defaultSoundFX = 1;
        else defaultSoundFX = 0;

       if (PlayerPrefs.GetInt("SoundFX", defaultSoundFX) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetCameraShakeStatus()
    {
        int defaultcamera;
        if (DefaultCameraShake) defaultcamera = 1;
        else defaultcamera = 0;

        if (PlayerPrefs.GetInt("CameraShake", defaultcamera) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // -- Set
    public void SetButtonPosition(string position)
    {
        PlayerPrefs.SetString("ButtonPosition", position);
    }
    public void SetGameController(string gamecontrol)
    {
        PlayerPrefs.SetString("GameController", gamecontrol);
    }
    public void SetSoundEffectStatus(bool status)
    {
        if (status)
        {
            PlayerPrefs.SetInt("SoundFX", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SoundFX", 0);
        }
    }
    public void SetCameraShakeStatus(bool status)
    {
        if(status)
        {
            PlayerPrefs.SetInt("CameraShake", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CameraShake", 0);
        }
    }
    public void SetMusicVolume(float musicVol)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
    }
}
