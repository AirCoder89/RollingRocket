using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{
    public string LevelName;
    public bool IsLocked;
    public List<Stage> Stages;
    private int nbStages;
    private int LevelCoins;
    public int TotalLevelCoins;
    public Level(string name,bool islocked, int nbstages)
    {
        this.LevelName = name;
        this.IsLocked = islocked;
        this.nbStages = nbstages;
        this.LevelCoins = 0;
    }
    public Level(string name)
    {
        this.LevelName = name;
        Init();
    }
    public void Init() //if this not the first opned
    {
        //--init level
        this.nbStages = PlayerPrefs.GetInt("nbStages" + this.LevelName, -1);
        if(PlayerPrefs.GetInt("Locked" + this.LevelName, 1) == 1)
        {
            this.IsLocked = true;
        }
        else
        {
            this.IsLocked = false;
        }

        //--add stages and init them
        if(this.nbStages > -1)
        {
            this.Stages = new List<Stage>();
            Stage s;
            //init stages
            for (int i = 0; i < this.nbStages; i++)
            {
                s = new Stage(i, this.LevelName + "Stage0" + (i + 1).ToString());
                Stages.Add(s);
            }
        }
    }
    public void FirstInit(bool isStage1Opened)
    {
        //save data
        PlayerPrefs.SetInt("nbStages" + this.LevelName, this.nbStages);

        if(IsLocked) PlayerPrefs.SetInt("Locked" + this.LevelName, 1);
        else PlayerPrefs.SetInt("Locked" + this.LevelName, 0);

        //create stages and save stages data
        this.Stages = new List<Stage>();
        Stage s;
        //init stages
        for (int i=0;i< this.nbStages;i++)
        {
            if (i == 0 && isStage1Opened)
            {
                s = new Stage(i, this.LevelName + "Stage0" + (i + 1).ToString(),false,false);
            }
            else if(i == 4 || i==5 || i==6)
            {
                s = new Stage(i, this.LevelName + "Stage0" + (i + 1).ToString(),true,true);
            }
            else
            {
                s = new Stage(i, this.LevelName + "Stage0" + (i + 1).ToString(), true,false);
            }

            Stages.Add(s);
        }

    }

    public void Unlock(bool OpenFirstStage)
    {
        this.IsLocked = false;
        PlayerPrefs.SetInt("Locked" + this.LevelName, 0);
        if(OpenFirstStage)
        {
            Stages[0].Unlock();
        }
    }

    public void Lock()
    {
        this.IsLocked = true;
        PlayerPrefs.SetInt("Locked" + this.LevelName, 1);
    }

    public void SetTotalLevelCoins(int totalCoins)
    {
        this.TotalLevelCoins = totalCoins;
    }
    
    public void UnLockStagebyIndex(int index)
    {
        Stages[index].Unlock();
    }
}
