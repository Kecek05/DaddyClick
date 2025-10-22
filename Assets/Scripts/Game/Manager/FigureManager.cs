using System;
using System.Collections.Generic;
using UnityEngine;

public static class FigureManager
{
    public static event Action OnGainFigure;
    private const string FIGURES_KEY = "PlayerFigures";
    
    
    private static Dictionary<FigureType, int> _boughtFigures = new();

    #region PUBLICS

    public static Dictionary<FigureType, int> BoughtFigures => _boughtFigures;
    

    #endregion
    
    
    public static int GetFigureAmountByType(FigureType figureType)
    {
        _boughtFigures.TryGetValue(figureType, out int figureCount);
        return figureCount;
    }
    
    public static void GainFigure(FigureType figureType)
    {
        _boughtFigures[figureType] += 1;
        OnGainFigure?.Invoke();
    }
    
    public static void SaveFigures()
    {
        FiguresSaveData figuresData = new FiguresSaveData();
        foreach (var figurePair in _boughtFigures)
        {
            figuresData.figureTypes.Add(figurePair.Key);
            figuresData.figureCounts.Add(figurePair.Value);
        }
        
        string figuresJson = JsonUtility.ToJson(figuresData);
        PlayerPrefs.SetString(FIGURES_KEY, figuresJson);
    }

    public static void LoadFigures()
    {
        foreach (FigureType figureType in Enum.GetValues(typeof(FigureType)))
        {
            _boughtFigures.Add(figureType, 0);
        }
        
        if (PlayerPrefs.HasKey(FIGURES_KEY))
        {
            string figuresJson = PlayerPrefs.GetString(FIGURES_KEY);
            FiguresSaveData figuresData = JsonUtility.FromJson<FiguresSaveData>(figuresJson);
            
            if (figuresData != null && figuresData.figureTypes != null && figuresData.figureCounts != null)
            {
                for (int i = 0; i < figuresData.figureTypes.Count; i++)
                {
                    _boughtFigures[figuresData.figureTypes[i]] = figuresData.figureCounts[i];
                }
            }
        }
    }
    
    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey(FIGURES_KEY);
        _boughtFigures.Clear();
    }
}

[Serializable]
public class FiguresSaveData
{
    public List<FigureType> figureTypes = new List<FigureType>();
    public List<int> figureCounts = new List<int>();
}