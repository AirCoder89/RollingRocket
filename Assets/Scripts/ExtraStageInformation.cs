
public class ExtraStageInformation{
    public static string GetName(string StageName)
    {
        switch (StageName)
        {
            case "Level01Stage05": return "Level01 - Stage05";
            case "Level01Stage06": return "Level01 - Stage06";
            case "Level01Stage07": return "Level01 - Stage07";
            case "Level02Stage05": return "Level02 - Stage05";
            case "Level02Stage06": return "Level02 - Stage06";
            case "Level02Stage07": return "Level02 - Stage07";
            case "Level03Stage05": return "Level03 - Stage05";
            case "Level03Stage06": return "Level03 - Stage06";
            case "Level03Stage07": return "Level03 - Stage07";
            default: return "NULL";
        }
    }
    public static string GetDistance(string StageName)
    {
        switch(StageName)
        {
            case  "Level01Stage05" : return "1.74 Km";
            case  "Level01Stage06" : return "1.92 Km"; 
            case  "Level01Stage07" : return "1.93 Km"; 
            case "Level02Stage05": return "1.46 Km"; 
            case "Level02Stage06": return "1.41 Km"; 
            case "Level02Stage07": return "1.29 Km"; 
            case "Level03Stage05": return " Km"; 
            case "Level03Stage06": return " Km"; 
            case "Level03Stage07": return " Km"; 
            default: return "NULL";
        }
       
    }

    public static string GetCoins(string StageName)
    {
        switch (StageName)
        {
            case "Level01Stage05": return "93 Coins";
            case "Level01Stage06": return "90 Coins";
            case "Level01Stage07": return "101 Coins";
            case "Level02Stage05": return "79 Coins";
            case "Level02Stage06": return "83 Coins";
            case "Level02Stage07": return "87 Coins";
            case "Level03Stage05": return " Coins";
            case "Level03Stage06": return " Coins";
            case "Level03Stage07": return " Coins";
            default: return "NULL";
        }

    }

    public static string GetNitro(string StageName)
    {
        switch (StageName)
        {
            case "Level01Stage05": return "X 2";
            case "Level01Stage06": return "X 2";
            case "Level01Stage07": return "X 2";
            case "Level02Stage05": return "X 1";
            case "Level02Stage06": return "X 1";
            case "Level02Stage07": return "X 1";
            case "Level03Stage05": return "X "; 
            case "Level03Stage06": return "X ";
            case "Level03Stage07": return "X ";
            default: return "NULL";
        }

    }

    public static string GetSlowMotion(string StageName)
    {
        switch (StageName)
        {
            case "Level01Stage05": return "X 2";
            case "Level01Stage06": return "X 2";
            case "Level01Stage07": return "X 2";
            case "Level02Stage05": return "X 1"; 
            case "Level02Stage06": return "X 1";
            case "Level02Stage07": return "X 0";
            case "Level03Stage05": return "X ";
            case "Level03Stage06": return "X ";
            case "Level03Stage07": return "X ";
            default: return "NULL";
        }
    }

    public static string GetInverse(string StageName)
    {
        switch (StageName)
        {
            case "Level01Stage05": return "X 0"; 
            case "Level01Stage06": return "X 0";
            case "Level01Stage07": return "X 0"; 
            case "Level02Stage05": return "X 0";
            case "Level02Stage06": return "X 1";
            case "Level02Stage07": return "X 1"; 
            case "Level03Stage05": return "X "; 
            case "Level03Stage06": return "X ";
            case "Level03Stage07": return "X ";
            default: return "NULL";
        }
    }

}
