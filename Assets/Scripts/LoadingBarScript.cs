using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarScript : MonoBehaviour {

    public static LoadingBarScript Instance;

    [SerializeField] private GameObject PanelLoading;
    [SerializeField] private Text loadingText;
    [SerializeField] private Slider sliderBar;
    [SerializeField] private Image BlackScreen;
  
    private Animator animator;
    private bool isBlackPanel;
    private string SceneName;
    private string sceneType;
    void Start () {

        Instance = this;
        animator = GetComponent<Animator>();
        
        PanelLoading.gameObject.SetActive(false);
        
        isBlackPanel = false;
        //Hide Slider Progress Bar in start
        
	}
    public void FadeIn()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (sceneType == "level")
        {
            if (SceneHandler.GetInstance().ResetCounter % 4 == 0)
            {
                /* if (AdmobManager.Instance.interstitial.IsLoaded())
                 {
                     AdmobManager.Instance.ShowInterstitial();
                 }
                 else
                 {*/
                
                    string PID = AdManager.Instance.IsSimpleVideoReadyToShow();
                    if (!PID.Equals("NULL"))
                    {
                        SceneHandler.GetInstance().DebugLog("Simple Video ID: " + PID);
                        AdManager.Instance.UnityAd_Show(PID);
                    }
                    else
                    {
                        SceneHandler.GetInstance().DebugLog("Fail to load Simple Video");
                    }
                //}

            }

            isBlackPanel = false;
            SceneHandler.GetInstance().SetCurrentLevelName(SceneName);
        }
        else if (sceneType == "win")
        {
            //hide everything in the panel (just a black)
            isBlackPanel = true;
            loadingText.gameObject.SetActive(false);
            sliderBar.gameObject.SetActive(false);
            BlackScreen.enabled = true;
           
        }
        else
        {
            BlackScreen.enabled = false;
            isBlackPanel = false;
        }
        PanelLoading.gameObject.SetActive(true);
        loadingText.text = "Loading [Starting]";
        StartCoroutine(LoadNewScene(SceneName));
    }
    public void LoadScene(string _sceneName,string _sceneType)
    {
        this.SceneName = _sceneName;
        this.sceneType = _sceneType;
        if (sceneType == "win")
        {
            OnFadeComplete();
        }
        else
        {
            FadeIn();
        }
    }
	
    
    IEnumerator LoadNewScene(string sceneName) {

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            if(!isBlackPanel)
            {
                float progress = Mathf.Clamp01(async.progress / 0.9f);
                sliderBar.value = progress;
                loadingText.text = "Loading [" + progress * 100f + "%]";
            }
            else
            {
                BlackScreen.enabled = true;
            }
           
            yield return null;

        }

    }

}
