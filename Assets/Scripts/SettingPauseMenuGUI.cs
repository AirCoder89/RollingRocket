using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPauseMenuGUI : MonoBehaviour {
    //Gui Components
   
    [SerializeField] private Button MusicVolUpBtn;
    [SerializeField] private Button MusicVolDownBtn;
    [SerializeField] private Image MusicVolFill;

    [SerializeField] private ONOFFBtnScript SoundEffectBtn;
    [SerializeField] private ONOFFBtnScript CameraShakeBtn;

    [SerializeField] private Toggle TapOnScreen;
    [SerializeField] private Toggle PressButton;
    [SerializeField] private Toggle RightSide;
    [SerializeField] private Toggle LeftSide;

    //private declare
    private float VolumeRatio;
    private bool SoundFXStatus;
    private bool CameraShakeStatus;
    private string GameControl;
    private string btnPosition;

    public void InitGUI()
    {
       
        //Music Vol
        MusicVolUpBtn.onClick.AddListener(VolUp);
        MusicVolDownBtn.onClick.AddListener(VolDown);
        VolumeRatio = SceneHandler.GetInstance().Settings.GetMusicVolume();
        UpdateVolumeProgressBar();

        //sound fx
        SoundFXStatus = SceneHandler.GetInstance().Settings.GetSoundEffectStatus();
        SoundEffectBtn.UpdateBtn(SoundFXStatus);

        //Camera Shake
        CameraShakeStatus = SceneHandler.GetInstance().Settings.GetCameraShakeStatus();
        CameraShakeBtn.UpdateBtn(CameraShakeStatus);

        //Game Controller

        FirstUpdateGameController();
    }

    //------- Game Controller
    private void FirstUpdateGameController()
    {
        btnPosition = SceneHandler.GetInstance().Settings.GetButtonPosition();

        if (btnPosition == "right")
        {
            RightSide.isOn = true;
            LeftSide.isOn = false;
        }
        else
        {
            RightSide.isOn = false;
            LeftSide.isOn = true;
        }


        GameControl = SceneHandler.GetInstance().Settings.GetGameController();
       // print("*************************** " + GameControl);

        if (GameControl.Equals("tap"))
        {
            TapOnScreen.isOn = true;
            PressButton.isOn = false;
            RightSide.interactable = false;
            LeftSide.interactable = false;
        }
        else if (GameControl.Equals("btn"))
        {
            TapOnScreen.isOn = false;
            PressButton.isOn = true;
            RightSide.interactable = true;
            LeftSide.interactable = true;
        }

    }
    public void OnButtonPositionChanged(bool isRight)
    {
        if (isRight)
        {
            if (RightSide.isOn && !LeftSide.isOn)
            {
                SceneHandler.GetInstance().Settings.SetButtonPosition("right");
                btnPosition = "right";
            }
            else if (!RightSide.isOn && !LeftSide.isOn)
            {
                SceneHandler.GetInstance().Settings.SetButtonPosition("right");
                btnPosition = "right";
            }

        }
        else
        {
            if (!RightSide.isOn && LeftSide.isOn)
            {
                SceneHandler.GetInstance().Settings.SetButtonPosition("left");
                btnPosition = "left";
            }
            else if (!RightSide.isOn && !LeftSide.isOn)
            {
                SceneHandler.GetInstance().Settings.SetButtonPosition("right");
                btnPosition = "right";
            }
        }
        LevelManager.Instance.UpdateShipControl();
    }
    public void OnGameControllerChanged(bool isTap)
    {
        if (isTap)
        {
            if (TapOnScreen.isOn && !PressButton.isOn)
            {
                GameControl = "tap";
                RightSide.interactable = false;
                LeftSide.interactable = false;
                SceneHandler.GetInstance().Settings.SetGameController("tap");
            }
            else if (!TapOnScreen.isOn && !PressButton.isOn)
            {
                GameControl = "tap";
                RightSide.interactable = false;
                LeftSide.interactable = false;
                SceneHandler.GetInstance().Settings.SetGameController("tap");
            }

        }
        else
        {
            if (!TapOnScreen.isOn && PressButton.isOn)
            {
                GameControl = "btn";
                RightSide.interactable = true;
                LeftSide.interactable = true;
                SceneHandler.GetInstance().Settings.SetGameController("btn");
            }
            else if (!TapOnScreen.isOn && !PressButton.isOn)
            {
                GameControl = "tap";
                RightSide.interactable = false;
                LeftSide.interactable = false;
                SceneHandler.GetInstance().Settings.SetGameController("tap");
            }
        }

        LevelManager.Instance.UpdateShipControl();
    }

    //------- Camera Shake
    private void UpdateCameraShakeStatus()
    {
        CameraShakeBtn.UpdateBtn(CameraShakeStatus); //change btn shape
        SceneHandler.GetInstance().Settings.SetCameraShakeStatus(CameraShakeStatus); //save data
    }

    public void OnChangeCameraShakeStatus()
    {
        CameraShakeStatus = !CameraShakeStatus;
        UpdateCameraShakeStatus();
    }
    //------- Sound EFFECT
    private void UpdateSoundFXStatus()
    {
        SoundEffectBtn.UpdateBtn(SoundFXStatus); //change btn shape
        SceneHandler.GetInstance().Settings.SetSoundEffectStatus(SoundFXStatus); //save data
        SceneHandler.GetInstance().Audio.SetSoundEffectStatus(SoundFXStatus); //apply to audio
    }
    public void OnChangeSoundFXStatus()
    {
        SoundFXStatus = !SoundFXStatus;
        UpdateSoundFXStatus();
    }
    //------- Music Volume
    private void UpdateVolumeProgressBar()
    {
        MusicVolFill.fillAmount = VolumeRatio;
    }

    public void VolUp()
    {
        if (VolumeRatio < 1)
        {
            VolumeRatio += 0.1f;
            SceneHandler.GetInstance().Settings.SetMusicVolume(VolumeRatio);
            SceneHandler.GetInstance().Audio.SetMusicVolume(VolumeRatio);
            UpdateVolumeProgressBar();
        }
    }

    public void VolDown()
    {
        if (VolumeRatio > 0)
        {
            VolumeRatio -= 0.1f;
            SceneHandler.GetInstance().Settings.SetMusicVolume(VolumeRatio);
            SceneHandler.GetInstance().Audio.SetMusicVolume(VolumeRatio);
            UpdateVolumeProgressBar();
        }
    }
}
