
public static class ClickUtils
{
    public static float GetCPS(FigureDataListSO figureDataSO)
    {
        float cps = 0f;
        foreach (var figureData in figureDataSO.Figures)
        {
            int figureCount = 0;
            FigureManager.BoughtFigures.TryGetValue(figureData.FigureType, out figureCount);
            cps += figureData.CPS * figureCount;
        }

        return cps;
    }
    
    public static float GetDaddyMultiplier(DaddyDataListSO daddyDataListSO)
    {
        float multiplier = 1f;
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
