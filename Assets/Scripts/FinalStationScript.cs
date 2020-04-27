
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
public class FinalStationScript : MonoBehaviour {

    public GameObject Vortex;
    public GameObject FinalFX;
    private AnalyticsTracker Tracker;
    private void Start()
    {
        Tracker = GetComponent<AnalyticsTracker>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ship")
        {
            GameObject[] allGui = GameObject.FindGameObjectsWithTag("GUI");
            foreach (GameObject Gui in allGui)
            {
                Gui.SetActive(false);
            }
            
            SetNextLevel();
            if (SceneHandler.GetInstance().UseAnalytics) Tracker.TriggerEvent();
            LevelManager.Instance.ResetTime();
            if (ShipController.Instance != null) ShipController.Instance.onLevelWin();
            else print("SHIP INSTANCE IS NULL");
            EventHandler.LevelWin_TR();
            StartCoroutine(createVortex());
           // ObjectPooler.Instance.SpawnFromPool("FinalFX", transform.position, Quaternion.identity);
            Instantiate(FinalFX, transform.position, Quaternion.identity);
        }
    }

    IEnumerator createVortex()
    {
        yield return new WaitForSeconds(1f);
        Vector3 pos = Camera.main.transform.position + new Vector3(0, 0, 15);
        //ObjectPooler.Instance.SpawnFromPool("Vortex", pos, Quaternion.identity);
        Instantiate(Vortex, pos, Quaternion.identity);
    }

    private void SetNextLevel()
    {
        string currentLevel = SceneHandler.GetInstance().GetCurrentLevelName();
        if(currentLevel != "Level01Stage07" && currentLevel != "Level02Stage07" && currentLevel != "Level03Stage07")
        {
            SceneHandler.GetInstance().Stages.StageComplete(currentLevel); //unlock the next stage
        }
        string nextLevel = "";
        switch(currentLevel)
        {
            case "Level01Stage01": nextLevel = "Level01Stage02"; break;
            case "Level01Stage02": nextLevel = "Level01Stage03"; break;
            case "Level01Stage03": nextLevel = "Level01Stage04"; break;
            case "Level01Stage04": nextLevel = "LevelSelect"; break; //here we need to tell them you have to buy the Stage 5 and 6
            case "Level01Stage05": nextLevel = "LevelSelect"; break;
            case "Level01Stage06": nextLevel = "LevelSelect"; break;
            case "Level01Stage07": nextLevel = "LevelSelect"; break;

            case "Level02Stage01": nextLevel = "Level02Stage02"; break;
            case "Level02Stage02": nextLevel = "Level02Stage03"; break;
            case "Level02Stage03": nextLevel = "Level02Stage04"; break;
            case "Level02Stage04": nextLevel = "LevelSelect"; break; //here we need to tell them you have to buy the Stage 5 and 6
            case "Level02Stage05": nextLevel = "LevelSelect"; break;
            case "Level02Stage06": nextLevel = "LevelSelect"; break;
            case "Level02Stage07": nextLevel = "LevelSelect"; break;

            case "Level03Stage01": nextLevel = "Level03Stage02"; break;
            case "Level03Stage02": nextLevel = "Level03Stage03"; break;
            case "Level03Stage03": nextLevel = "Level03Stage04"; break;
            case "Level03Stage04": nextLevel = "LevelSelect"; break; //here we need to tell them you have to buy the Stage 5 and 6
            case "Level03Stage05": nextLevel = "LevelSelect"; break;
            case "Level03Stage06": nextLevel = "LevelSelect"; break;
            case "Level03Stage07": nextLevel = "LevelSelect"; break;

            default: nextLevel = "LevelSelect"; break;
        }

        SceneHandler.GetInstance().SetNextLevelName(nextLevel);

    }
}
