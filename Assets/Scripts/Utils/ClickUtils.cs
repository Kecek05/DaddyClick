
public static class ClickUtils
{
    public static float GetCPS(FigureDataListSO figureDataSO)
    {
        float cps = 0f;
        foreach (var figureData in figureDataSO.Figures)
        {
            int figureCount = 0;
            PlayerSave.Figures.TryGetValue(figureData.FigureType, out figureCount);
            cps += figureData.CPS * figureCount;
        }

        return cps;
    }
}
