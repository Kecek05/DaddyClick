
public static class ClickUtils
{
    public static double GetCPS(FigureDataListSO figureDataSO)
    {
        double cps = 0.0;
        foreach (var figureData in figureDataSO.Figures)
        {
            int figureCount = 0;
            FigureManager.BoughtFigures.TryGetValue(figureData.FigureType, out figureCount);
            cps += figureData.CPS * figureCount;
        }

        return cps;
    }
    
    public static double GetDaddyMultiplier(DaddyDataListSO daddyDataListSO)
    {
        double multiplier = 1.0;
        foreach (var daddyData in daddyDataListSO.Daddies)
        {
            if (DaddyManager.BoughtDaddies.TryGetValue(daddyData.DaddyType, out bool unlocked))
            {
                if (unlocked)
                {
                    multiplier += daddyDataListSO.GetDaddyDataSOByType(daddyData.DaddyType).Multiplier;
                }
            }
        }
        return multiplier;
    }
}
