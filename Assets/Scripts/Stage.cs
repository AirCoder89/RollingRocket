using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage {
    public string StageName;
    public int StageIndex;
    private bool IsLocked;
    private bool IsExtra;
    private bool IsBuyed;

    public Stage(int index, string name, bool islocked,bool isextra)
    {
        this.StageIndex = index;
        this.StageName = name;
        this.IsLocked = islocked;
        this.IsExtra = isextra;
        this.IsBuyed = false;
        FirstInit(); //save data
    }
    private void FirstInit()
    {
        PlayerPrefs.SetInt("Index" + this.StageName, this.StageIndex);
        

        if(IsLocked) PlayerPrefs.SetInt("IsLocked" + this.StageName, 1);
        else PlayerPrefs.SetInt("IsLocked" + this.StageName, 0);

        if (IsExtra) PlayerPrefs.SetInt("IsExtra" + this.StageName, 1);
        else PlayerPrefs.SetInt("IsExtra" + this.StageName, 0);

        if (IsBuyed) PlayerPrefs.SetInt("IsBuyed" + this.StageName, 1);
        else PlayerPrefs.SetInt("IsBuyed" + this.StageName, 0);

    }
    public Stage(int index, string name)
    {
        this.StageIndex = index;
        this.StageName = name;
        init(); //load data
    }

    public bool GetIsLocked()
    {
        if (PlayerPrefs.GetInt("IsLocked" + this.StageName, 1) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsExtra()
    {
        if (PlayerPrefs.GetInt("IsExtra" + this.StageName, 1) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsBuyed()
    {
        if (PlayerPrefs.GetInt("IsBuyed" + this.StageName, 1) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void init()
    {
        this.StageIndex = PlayerPrefs.GetInt("Index" + this.StageName, -1);
        
        if(PlayerPrefs.GetInt("IsLocked" + this.StageName, 1) == 1)
        {
            IsLocked = true;
        }
        else
        {
            IsLocked = false;
        }

        if (PlayerPrefs.GetInt("IsExtra" + this.StageName, 1) == 1)
        {
            IsExtra = true;
        }
        else
        {
            IsExtra = false;
        }

        if (PlayerPrefs.GetInt("IsBuyed" + this.StageName, 1) == 1)
        {
            IsBuyed = true;
        }
        else
        {
            IsBuyed = false;
        }
    }
    public void Unlock()
    {
        this.IsLocked = false;
        PlayerPrefs.SetInt("IsLocked" + this.StageName, 0);
    }

    public void Lock()
    {
        this.IsLocked = true;
        PlayerPrefs.SetInt("IsLocked" + this.StageName, 1);
    }

    public void BuyThisStage()
    {
        if(IsExtra && !IsBuyed)
        {
            IsBuyed = true;
            PlayerPrefs.SetInt("IsBuyed" + this.StageName, 1);
        }
    }

    public void SetNotBuyed()
    {
        if (IsExtra && IsBuyed)
        {
            IsBuyed = false;
            PlayerPrefs.SetInt("IsBuyed" + this.StageName, 0);
        }
        Unlock();
    }
}
