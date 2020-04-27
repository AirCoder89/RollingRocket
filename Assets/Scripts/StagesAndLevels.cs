using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesAndLevels : MonoBehaviour {

    public bool isFirstOpen;
    public Level Level01;
    public Level Level02;
    public Level Level03;
    private List<Level> allLevels;
    void Start()
    {
     // PlayerPrefs.SetInt("IsFirstOpen", 0);
        if (PlayerPrefs.GetInt("IsFirstOpen", 0) == 1)
        {
            isFirstOpen = false;
            LevelInit();
        }
        else
        {
            isFirstOpen = true;
            FirstLevelInit();
        }
    }
    private void LevelInit()
    {
        allLevels = new List<Level>();
        Level01 = new Level("Level01");
        allLevels.Add(Level01);
        Level02 = new Level("Level02");
        allLevels.Add(Level02);
        Level03 = new Level("Level03");
        allLevels.Add(Level03);
    }

    private void FirstLevelInit()
    {
        //init the levels and stages
        PlayerPrefs.SetInt("IsFirstOpen", 1);
        isFirstOpen = false;
        allLevels = new List<Level>();
        Level01 = new Level("Level01",false, 7);
        Level01.FirstInit(true);
        allLevels.Add(Level01);
        Level02 = new Level("Level02",true, 7);
        Level02.FirstInit(false);
        allLevels.Add(Level02);
        Level03 = new Level("Level03", true, 7);
        Level03.FirstInit(false);
        allLevels.Add(Level03);
    }

    public void StageComplete(string stageName)
    {
        foreach(Level L in allLevels)
        {
            foreach(Stage S in L.Stages)
            {
                if(stageName == S.StageName)
                {
                    L.UnLockStagebyIndex(S.StageIndex + 1);
                    return;
                }
            }
        }
    }

    public int GetTotalLevelCoins(string stageName)
    {
        foreach (Level L in allLevels)
        {
            foreach (Stage S in L.Stages)
            {
                if (stageName == S.StageName)
                {
                    return L.TotalLevelCoins;
                }
            }
        }

        return -1; // not found
    }
    public bool isLevelActive(string levelName)
    {
        if (levelName == "Level01") return !Level01.IsLocked;
        else if (levelName == "Level02") return !Level02.IsLocked;
        else if (levelName == "Level03") return !Level03.IsLocked;

        return false;
    }

    public void BuyStageByName(string stageName)
    {
        foreach (Level L in allLevels)
        {
            foreach (Stage S in L.Stages)
            {
                if (stageName == S.StageName)
                {
                    S.BuyThisStage();
                }
            }
        }
    }

    public void UnlockLevelByName(string levelName,bool OpenFirstStage)
    {
        if(levelName == "Level01")
        {
            Level01.Unlock(OpenFirstStage);
        }else if (levelName == "Level02")
        {
            Level02.Unlock(OpenFirstStage);
        }
        else if (levelName == "Level03")
        {
            Level03.Unlock(OpenFirstStage);
        }
    }
}
