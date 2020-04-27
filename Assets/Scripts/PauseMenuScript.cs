
using UnityEngine;


public class PauseMenuScript : MonoBehaviour {

    public static bool isOpen = false;
    public static bool CanOpen = true;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject MenuBtns;
    public void OpenMenu()
    {
        if(!isOpen && !GameOverPanelScript.Instance.IsOpen && CanOpen)
        {
            isOpen = true;
            this.gameObject.SetActive(true);
            LevelManager.Instance.PauseGame();
        }
    }
    public void CloseMenu()
    {
        isOpen = false;
            this.gameObject.SetActive(false);
            LevelManager.Instance.ResumeGame();
    }
    public void onClickSettings()
    {
        MenuBtns.SetActive(false);
        SettingsPanel.SetActive(true);
        SettingsPanel.GetComponent<SettingPauseMenuGUI>().InitGUI();
    }
    public void CLICK_BACK_FROM_SETTING()
    {
        SettingsPanel.SetActive(false);
        MenuBtns.SetActive(true);
    }
    public void onClickExitGame()
    {
        CloseMenu();
        LoadingBarScript.Instance.LoadScene("MainMenu", "mainMenu");
    }

    public void onClickResume()
    {
        CloseMenu();
    }

    public void onClickReplay()
    {
        CloseMenu();
        LevelManager.Instance.RestartLevel();
    }


}
