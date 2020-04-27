using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    private float TimeMagnet = 10f; //10 sec
    private float TimeScale = 10f; //10 sec
    [SerializeField] private Text MagnetTxt;
    [SerializeField] private Text ScaleTxt;
    [SerializeField] private GameObject ButtonControl;
   

    [SerializeField] private FuelProgressBar ProgressBarFuel;
    
    private float FixedTime;
    private bool isGamePaused;
    private float OldTimeScale;
    private float SavedDistance;

    private float LevelSpeed;
    private float ShipXSpeed;

    private bool NitroON;
    private bool SlowMotionON;

    // Use this for initialization
    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        Start();
    }
    void Start () {

       // SceneHandler.GetInstance().SaveTotalCoins(0);
        AdManager.Instance.InitializeAD();
        Instance = this;
        isGamePaused = false;
        
        FixedTime = Time.fixedDeltaTime;
       
        if (SceneManager.GetActiveScene().name == "Level01Stage01") InitLevel01Stage01();
        else if (SceneManager.GetActiveScene().name == "Level01Stage02") InitLevel01Stage02();
        else if (SceneManager.GetActiveScene().name == "Level01Stage03") InitLevel01Stage03();
        else if (SceneManager.GetActiveScene().name == "Level01Stage04") InitLevel01Stage04();
        else if (SceneManager.GetActiveScene().name == "Level01Stage05") InitLevel01Stage05();
        else if (SceneManager.GetActiveScene().name == "Level01Stage06") InitLevel01Stage06();
        else if (SceneManager.GetActiveScene().name == "Level01Stage07") InitLevel01Stage07();
        else if (SceneManager.GetActiveScene().name == "Endless") InitEndless();
        else if (SceneManager.GetActiveScene().name == "Level02Stage01") InitLevel02Stage01();
        else if (SceneManager.GetActiveScene().name == "Level02Stage02") InitLevel02Stage02();
        else if (SceneManager.GetActiveScene().name == "Level02Stage03") InitLevel02Stage03();
        else if (SceneManager.GetActiveScene().name == "Level02Stage04") InitLevel02Stage04();
        else if (SceneManager.GetActiveScene().name == "Level02Stage05") InitLevel02Stage05();
        else if (SceneManager.GetActiveScene().name == "Level02Stage06") InitLevel02Stage06();
        else if (SceneManager.GetActiveScene().name == "Level02Stage07") InitLevel02Stage07();
        else if (SceneManager.GetActiveScene().name == "Level03Stage01") InitLevel03Stage01();
        else if (SceneManager.GetActiveScene().name == "Level03Stage02") InitLevel03Stage02();
        else if (SceneManager.GetActiveScene().name == "Level03Stage03") InitLevel03Stage03();
        else if (SceneManager.GetActiveScene().name == "Level03Stage04") InitLevel03Stage04();
        else if (SceneManager.GetActiveScene().name == "Level03Stage05") InitLevel03Stage05();
        else if (SceneManager.GetActiveScene().name == "Level03Stage06") InitLevel03Stage06();
        else if (SceneManager.GetActiveScene().name == "Level03Stage07") InitLevel03Stage07();


        //--------
        GameObject ship = GameObject.FindGameObjectWithTag("ShipParent");
        if (ship != null) ShipController.Instance = ship.GetComponent<ShipController>();
        //--------

        AfterInit();
        UpdateShipControl();
        
    }
    public void UpdateShipControl()
    {
        Button ControlBtn;
        if (SceneHandler.GetInstance().Settings.GetGameController() == "tap")
        {
            ButtonControl.SetActive(false);
            ControlBtn = ButtonControl.GetComponentInChildren<Button>();
            ShipController.Instance.GameControl = "tap";
            ControlBtn.GetComponentInChildren<Button>().enabled = false;
            ControlBtn.interactable = false;
        }
        else
        {
            ButtonControl.SetActive(true);
            ControlBtn = ButtonControl.GetComponentInChildren<Button>();
            ControlBtn.enabled = true;
            ControlBtn.interactable = true;
            if (SceneHandler.GetInstance().Settings.GetButtonPosition() == "right")
            {
                //right
                ControlBtn.GetComponent<RectTransform>().localPosition = new Vector3(520, ControlBtn.GetComponent<RectTransform>().localPosition.y, 0);
            }
            else
            {
                //left
                ControlBtn.GetComponent<RectTransform>().localPosition = new Vector3(-520, ControlBtn.GetComponent<RectTransform>().localPosition.y, 0);
            }
        }
    }

    public void HideButtonControl()
    {
        ButtonControl.SetActive(false);
    }

    public void ShowBtnControl()
    {
        ButtonControl.SetActive(true);
    }
    public void BtnControlDown()
    {
        ShipController.Instance.touchBegan = true;
        ShipController.Instance.touchEnd = false;
    }

    public void BtnControlUp()
    {
        ShipController.Instance.touchBegan = false;
        ShipController.Instance.touchEnd = true;
    }
    public void HitFuel()
    {
        ShipController.Instance.SetFuelStatus(false);
        ProgressBarFuel.AddFuel(200f);
    }
    public void PauseGame()
    {
        if (!isGamePaused)
        {
            ShipController.Instance.isGamePaused = true;
            OldTimeScale = Time.timeScale;
            isGamePaused = true;
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {
        if (isGamePaused)
        {
            ShipController.Instance.isGamePaused = false;
            isGamePaused = false;
            Time.timeScale = OldTimeScale;
        }
    }
   
    public void HitMagnetBonus()
    {
        if(ShipController.Instance.isOnMagnet)
        {
            CancelInvoke("CountMagnet");
            TimeMagnet = 10f;
        }

        MagnetTxt.text = TimeMagnet.ToString("F");
        ShipController.Instance.isOnMagnet = true;
        InvokeRepeating("CountMagnet", 0.0f, 0.01f);

    }

    public void HitBigScaleBonus()
    {
        ShipController.Instance.SetScale("Big");
        ScaleTxt.text = TimeScale.ToString("F");
        CancelInvoke("CountScale");
        InvokeRepeating("CountScale", 0.0f, 0.01f);
    }
    public void HitSmallScaleBonus()
    {
        ShipController.Instance.SetScale("Small");
        ScaleTxt.text = TimeScale.ToString("F");
        CancelInvoke("CountScale");
        InvokeRepeating("CountScale", 0.0f, 0.01f);
    }
   
    public void CountScale()
    {
        if (TimeScale <= 0f)
        {
            ScaleTxt.text = "0.00";
            TimeScale = 10f;
            ShipController.Instance.SetScale("Normal");
            CancelInvoke("CountScale");
        }
        else
        {
            TimeScale -= 0.01f;
            ScaleTxt.text = TimeScale.ToString("F");
        }
    }
    public void CountMagnet()
    {
        if(TimeMagnet <= 0f)
        {
            MagnetTxt.text = "0.00";
            TimeMagnet = 10f;
            ShipController.Instance.isOnMagnet = false;
            CancelInvoke("CountMagnet");
        }
        else
        {
            TimeMagnet -=0.01f;
            MagnetTxt.text = TimeMagnet.ToString("F");
        }
    }
//################################# NITRO ZONE #################################
    public void StartNitro()
    {
        NitroON = true;
        if (SlowMotionON) ForceEndSlowMotion();
        ShipController.Instance.SetTrailSpeedFX(true,3f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipFollow>().StartNitro();
        StartCoroutine(EnterNitroZone());
    }
    IEnumerator EnterNitroZone()
    {
        PauseMenuScript.CanOpen = false;
        float startTime = Time.time;
        float duration = 2f;
        float SPEED = 1.3f;
        float PITCH = 1.05f;
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //Fade in Trail FX
            ShipController.Instance.UpdateTrailSpeedAlpha(Mathf.Lerp(ShipController.Instance.getTrailSpeedAlpha(), 255f, smoothing));
            //lerp
            Time.timeScale = Mathf.Lerp(Time.timeScale, SPEED, smoothing);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, Time.timeScale * .02f, smoothing);
            //Lerp Music Pitch
            float pitchTarget = 1f;
            pitchTarget = Mathf.Lerp(pitchTarget, PITCH, smoothing);
            SceneHandler.GetInstance().Audio.SetMusicPitch(pitchTarget);
            yield return null;
        }

        //Enter slow motion complete
        PauseMenuScript.CanOpen = true;
        Time.timeScale = SPEED;
        SceneHandler.GetInstance().Audio.SetMusicPitch(PITCH);
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return null;
    }

    public void StopNitro()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipFollow>().StopNitro();
        StartCoroutine(ExitNitroZone());
    }

    IEnumerator ExitNitroZone()
    {
        float startTime = Time.time;
        float duration = 3f;
        float pitchTarget = 1.05f;
        PauseMenuScript.CanOpen = false;
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //Fade out Trail FX
            ShipController.Instance.UpdateTrailSpeedAlpha(Mathf.Lerp(ShipController.Instance.getTrailSpeedAlpha(), 0f, smoothing));
            //lerp
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, smoothing);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, FixedTime, smoothing);

            //Lerp Music Pitch
            pitchTarget = Mathf.Lerp(pitchTarget, 1f, smoothing);
            SceneHandler.GetInstance().Audio.SetMusicPitch(pitchTarget);
            yield return null;
        }

        //Enter slow motion complete
        NitroON = false;
        PauseMenuScript.CanOpen = true;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = FixedTime;
        ShipController.Instance.SetTrailSpeedFX(false,0);
        SceneHandler.GetInstance().Audio.SetMusicPitch(1f);
        yield return null;
    }
    private void ForceStopNitro()
    {
        ResetTime();
        NitroON = false;
        StopCoroutine(EnterNitroZone());
        StopCoroutine(ExitNitroZone());
    }
    //################################# SLOW MOTION #################################
    public void StartSlowMotion(bool moveOffset)
    {
        SlowMotionON = true;
        if (NitroON) ForceStopNitro();
        ShipController.Instance.SetTrailSpeedFX(true,0.7f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipFollow>().StartSlowMotion(moveOffset);
        StartCoroutine(EnterSlowMotion());
    }

    public void EndSlowMotion()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShipFollow>().StopSlowMotion();
        StartCoroutine(ExitSlowMotion());
       
    }
    
    IEnumerator EnterSlowMotion()
    {
        PauseMenuScript.CanOpen = false;
        float startTime = Time.time;
        float duration = 2f;
        float SLOW = 0.70f;
        while(Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //Fade in Trail FX
            ShipController.Instance.UpdateTrailSpeedAlpha(Mathf.Lerp(ShipController.Instance.getTrailSpeedAlpha(),255f, smoothing));
            //lerp
            Time.timeScale = Mathf.Lerp(Time.timeScale, SLOW, smoothing);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, Time.timeScale * .02f, smoothing);

            //Lerp Music Pitch
            float pitchTarget = 1f;
            pitchTarget = Mathf.Lerp(pitchTarget, 0.95f, smoothing);
            SceneHandler.GetInstance().Audio.SetMusicPitch(pitchTarget);
            yield return null;
        }

        //Enter slow motion complete
        PauseMenuScript.CanOpen = true;
        Time.timeScale = SLOW;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return null;
    }

    IEnumerator ExitSlowMotion()
    {
        float startTime = Time.time;
        float duration = 3f;
        PauseMenuScript.CanOpen = false;
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //Fade out Trail FX
            ShipController.Instance.UpdateTrailSpeedAlpha(Mathf.Lerp(ShipController.Instance.getTrailSpeedAlpha(), 0f, smoothing));
            //lerp
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, smoothing);
            Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, FixedTime, smoothing);
            //Lerp Music Pitch
            float pitchTarget = 0.95f;
            pitchTarget = Mathf.Lerp(pitchTarget, 1f, smoothing);
            SceneHandler.GetInstance().Audio.SetMusicPitch(pitchTarget);
            yield return null;
        }

        //Enter slow motion complete
        SlowMotionON = false;
        PauseMenuScript.CanOpen = true;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = FixedTime;
        ShipController.Instance.SetTrailSpeedFX(false,0f);
        yield return null;
    }
   private void ForceEndSlowMotion()
    {
        ResetTime();
        SlowMotionON = false;
        StopCoroutine(EnterSlowMotion());
        StopCoroutine(ExitSlowMotion());
    }
    public void StopBonusCounting()
    {
        CancelInvoke();
        MagnetTxt.text = "0.00";
        ScaleTxt.text = "0.00";
        
        //------>> Always check if those values is the same at the top
        TimeMagnet = 10f;
        TimeScale = 10f;
        ShipController.Instance.isOnMagnet = false;
    }
    public void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        //reset Audio Pitch
        ShipController.Instance.SetTrailSpeedFX(false, 0);
        SceneHandler.GetInstance().Audio.SetMusicPitch(1f);
    }
    public void UpdateFuelProgressBar(float fuelConsume)
    {
        ProgressBarFuel.ConsumeFuel(fuelConsume);
    }
   
    public void RemoveStartStation()
    {
        //remove start station
        GameObject Start_startion = GameObject.FindGameObjectWithTag("StartStation");
        if (Start_startion != null) Destroy(Start_startion);
        else print("START STATION NOT FOUND !!");
    }
    //************* Init Levels
    private void InitLevel01Stage01()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(72);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(72);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
   
    private void InitLevel01Stage02()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(78);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(78);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel01Stage03()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(73);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(73);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel01Stage04()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(126);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(126);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel01Stage05()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(93);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(93);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel01Stage06()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(90);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(90);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel01Stage07()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(101);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(101);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitEndless()
    {
       // SceneHandler.GetInstance().SetCoinsInLevel(95);
       // SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(95);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel02Stage01()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(73);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(73);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel02Stage02()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(71);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(71);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel02Stage03()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(73);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(73);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    
    private void InitLevel02Stage04()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(78);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(78);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel02Stage05()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(79);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(79);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel02Stage06()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(83);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(83);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel02Stage07()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(87);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(87);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }

    private void InitLevel03Stage01()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage02()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage03()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage04()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage05()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage06()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void InitLevel03Stage07()
    {
        SceneHandler.GetInstance().SetCoinsInLevel(0);
        SceneHandler.GetInstance().Stages.Level01.SetTotalLevelCoins(0);
        SceneHandler.GetInstance().SetMaxFuel(); //get fuel tank from ShipData
        SceneHandler.GetInstance().InitOnStart(); //init Scene handler for new stage
        ProgressBarFuel.InitProgressBar(500);
    }
    private void AfterInit()
	{
		if (SceneHandler.GetInstance().GetisContinue() && SceneHandler.GetInstance().GetisCheckedPoint()) {
            //set the player position
            //ship.transform.position = SceneHandler.GetInstance().GetSavedPosition();
            ShipController.Instance.SetPosition(SceneHandler.GetInstance().GetSavedPosition());
            ShipController.Instance.UpdateCoinsText(SceneHandler.GetInstance().GetLevelCoins());
            SceneHandler.GetInstance().RemoveLastCheckPointFromScene();
            SceneHandler.GetInstance().SetIsContinue(false);
            SceneHandler.GetInstance().SetIsLateContinue(true);
		}
	}
    //********************************************
	//---------- Restart Level
    public void RestartLevel()
    {
        SceneHandler.GetInstance().RemoveCheckPoint();
        SceneHandler.GetInstance().ResetLevelCoins();
       
        LoadingBarScript.Instance.LoadScene(SceneManager.GetActiveScene().name,"level");
    }

	//-------- Continue Level
    
    public void ContinueLevel()
    {
        
        if (SceneHandler.GetInstance().GetisCheckedPoint()) {

            //-- set Continue flag to true
            SceneHandler.GetInstance().SetIsContinue(true);

            //restart level
            LoadingBarScript.Instance.LoadScene(SceneManager.GetActiveScene().name, "level");
        }
       
    }

    //########################## GET & SET ################################
    //--Get

    //--Set
}
