using System;
using System.Collections.Generic;
using UnityEngine;


public enum FigureType
{
    None,
    Jason,
    IronMan,
}

public static class PlayerSave
{
    public static event Action OnGainFigure;
    
    public static float Money;
    public static Dictionary<FigureType, int> Figures = new()
    {
        { FigureType.None, 0 },
        { FigureType.Jason, 0 },
        { FigureType.IronMan, 0 }
    };

    public static void GainFigure(FigureType figureType)
    {
        Figures[figureType] += 1;
        OnGainFigure?.Invoke();
        Debug.Log(Figures[figureType]);
    }
    
    public static int GetFigureByType(FigureType figureType)
    {
        Figures.TryGetValue(figureType, out int figureCount);
        Debug.Log($"Figure: {figureType}, Count: {figureCount}");
        return figureCount;
    }
}
