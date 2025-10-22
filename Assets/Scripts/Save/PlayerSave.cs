using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FiguresSaveData
{
    public List<FigureType> figureTypes = new List<FigureType>();
    public List<int> figureCounts = new List<int>();
}

public static class PlayerSave
{
    public static event Action OnGainFigure;
    public static event Action OnSaveLoaded;
    
    private const string CLICKS_KEY = "PlayerClicks";
    private const string FIGURES_KEY = "PlayerFigures";
    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    
    private static Dictionary<FigureType, int> _figures = new();
    private static float _clicks = 0f;
    private static DateTime _lastPlayedTime = DateTime.MinValue;
    
    public static Dictionary<FigureType, int> Figures => _figures;
    public static float Clicks => _clicks;
    public static DateTime LastPlayedTime => _lastPlayedTime;

    public static void LoadPlayerSave()
    {
        // Load Clicks
        _clicks = PlayerPrefs.GetFloat(CLICKS_KEY, 0f);
        CurrencyManager.SetCurrency(_clicks);
        
        // Load Figures
        foreach (FigureType figureType in Enum.GetValues(typeof(FigureType)))
        {
            _figures.Add(figureType, 0);
        }
        
        if (PlayerPrefs.HasKey(FIGURES_KEY))
        {
            string figuresJson = PlayerPrefs.GetString(FIGURES_KEY);
            FiguresSaveData figuresData = JsonUtility.FromJson<FiguresSaveData>(figuresJson);
            
            if (figuresData != null && figuresData.figureTypes != null && figuresData.figureCounts != null)
            {
                for (int i = 0; i < figuresData.figureTypes.Count; i++)
                {
                    Figures[figuresData.figureTypes[i]] = figuresData.figureCounts[i];
                }
            }
        }
        
        // Load Last Played Time
        if (PlayerPrefs.HasKey(LAST_PLAYED_TIME_KEY))
        {
            string lastPlayedTimeString = PlayerPrefs.GetString(LAST_PLAYED_TIME_KEY);
            if (DateTime.TryParse(lastPlayedTimeString, out DateTime loadedTime))
            {
                _lastPlayedTime = loadedTime;
            }
        }
        OnSaveLoaded?.Invoke();
    }

    public static void SavePlayerData()
    {
        // Save Clicks
        PlayerPrefs.SetFloat(CLICKS_KEY, _clicks);
        
        // Save Figures
        FiguresSaveData figuresData = new FiguresSaveData();
        foreach (var figurePair in _figures)
        {
            figuresData.figureTypes.Add(figurePair.Key);
            figuresData.figureCounts.Add(figurePair.Value);
        }
        
        string figuresJson = JsonUtility.ToJson(figuresData);
        PlayerPrefs.SetString(FIGURES_KEY, figuresJson);
        
        // Save Last Played Time
        _lastPlayedTime = DateTime.Now;
        PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, _lastPlayedTime.ToString());
        
        PlayerPrefs.Save();
    }

    public static void GainFigure(FigureType figureType)
    {
        _figures[figureType] += 1;
        OnGainFigure?.Invoke();
    }
    
    public static int GetFigureAmountByType(FigureType figureType)
    {
        _figures.TryGetValue(figureType, out int figureCount);
        return figureCount;
    }
    
    public static void SetClicks(float amount)
    {
        _clicks = amount;
    }
    
    public static void ManualSave()
    {
        SavePlayerData();
    }
}

