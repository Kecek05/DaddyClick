using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FiguresSaveData
{
    public List<FigureType> figureTypes = new List<FigureType>();
    public List<int> figureCounts = new List<int>();
}

[Serializable]
public class DaddiesSaveData
{
    public List<DaddyType> daddyTypes = new List<DaddyType>();
    public List<bool> daddyUnlocked = new List<bool>();
}

public static class PlayerSave
{
    public static event Action OnGainFigure;
    public static event Action OnUnlockDaddy;
    
    [Tooltip("Subscribe to this in AWAKE. The data is loaded in START.")]
    public static event Action OnSaveLoaded;
    
    private const string CLICKS_KEY = "PlayerClicks";
    private const string FIGURES_KEY = "PlayerFigures";
    private const string DADDIES_KEY = "PlayerDaddies";
    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    
    private static Dictionary<FigureType, int> _boughtFigures = new();
    private static Dictionary<DaddyType, bool> _boughtDaddies = new();
    private static float _clicks = 0f;
    private static DateTime _lastPlayedTime = DateTime.MinValue;

    #region PUBLICS
    
    public static Dictionary<FigureType, int> boughtFigures => _boughtFigures;
    public static float Clicks => _clicks;
    public static DateTime LastPlayedTime => _lastPlayedTime;
    
    #endregion
    public static void LoadPlayerSave()
    {
        LoadClicks();
        LoadFigures();
        LoadDaddies();
        LoadLastPlayedTime();
 
        OnSaveLoaded?.Invoke();
    }

    private static void LoadClicks()
    {
        // Load Clicks
        _clicks = PlayerPrefs.GetFloat(CLICKS_KEY, 0f);
        CurrencyManager.SetCurrency(_clicks);
    }

    private static void LoadFigures()
    {
        // Load Figures
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
                    boughtFigures[figuresData.figureTypes[i]] = figuresData.figureCounts[i];
                }
            }
        }
    }

    private static void LoadLastPlayedTime()
    {
        // Load Last Played Time
        if (PlayerPrefs.HasKey(LAST_PLAYED_TIME_KEY))
        {
            string lastPlayedTimeString = PlayerPrefs.GetString(LAST_PLAYED_TIME_KEY);
            if (DateTime.TryParse(lastPlayedTimeString, out DateTime loadedTime))
            {
                _lastPlayedTime = loadedTime;
            }
        }
    }

    private static void LoadDaddies()
    {
        // Load Figures
        foreach (DaddyType daddyType in Enum.GetValues(typeof(DaddyType)))
        {
            _boughtDaddies.Add(daddyType, false);
        }
        
        if (PlayerPrefs.HasKey(DADDIES_KEY))
        {
            string daddiesJson = PlayerPrefs.GetString(DADDIES_KEY);
            DaddiesSaveData daddiesData = JsonUtility.FromJson<DaddiesSaveData>(daddiesJson);
            
            if (daddiesData != null && daddiesData.daddyTypes != null && daddiesData.daddyUnlocked != null)
            {
                for (int i = 0; i < daddiesData.daddyTypes.Count; i++)
                {
                    _boughtDaddies[daddiesData.daddyTypes[i]] = daddiesData.daddyUnlocked[i];
                }
            }
        }
    }
    
    public static void SavePlayerData()
    {
        SaveClicks();
        SaveFigures();
        SaveDaddies();
        SaveLastTime();
        
        PlayerPrefs.Save();
    }

    private static void SaveClicks()
    {
        PlayerPrefs.SetFloat(CLICKS_KEY, _clicks);
    }

    private static void SaveFigures()
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

    private static void SaveLastTime()
    {
        _lastPlayedTime = DateTime.Now;
        PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, _lastPlayedTime.ToString());
    }
    
    private static void SaveDaddies()
    {
        DaddiesSaveData daddiesData = new DaddiesSaveData();
        foreach (var daddyPair in _boughtDaddies)
        {
            daddiesData.daddyTypes.Add(daddyPair.Key);
            daddiesData.daddyUnlocked.Add(daddyPair.Value);
        }
        
        string daddiesJson = JsonUtility.ToJson(daddiesData);
        PlayerPrefs.SetString(DADDIES_KEY, daddiesJson);
    }
    
    public static void GainFigure(FigureType figureType)
    {
        _boughtFigures[figureType] += 1;
        OnGainFigure?.Invoke();
    }
    
    public static void UnlockDaddy(DaddyType daddyType)
    {
        _boughtDaddies[daddyType] = true;
        OnUnlockDaddy?.Invoke();
    }
    
    public static int GetFigureAmountByType(FigureType figureType)
    {
        _boughtFigures.TryGetValue(figureType, out int figureCount);
        return figureCount;
    }
    
    public static bool GetDaddyUnlockStatusByType(DaddyType daddyType)
    {
        _boughtDaddies.TryGetValue(daddyType, out bool isUnlocked);
        return isUnlocked;
    }
    
    public static void SetClicks(float amount)
    {
        _clicks = amount;
    }
    
    public static void ManualSave()
    {
        SavePlayerData();
    }
    
    
    
    
    
    public static void ResetSaveData()
    {
        PlayerPrefs.DeleteKey(CLICKS_KEY);
        PlayerPrefs.DeleteKey(FIGURES_KEY);
        PlayerPrefs.DeleteKey(DADDIES_KEY);
        PlayerPrefs.DeleteKey(LAST_PLAYED_TIME_KEY);
        
        _boughtFigures.Clear();
        _boughtDaddies.Clear();
        _clicks = 0f;
        _lastPlayedTime = DateTime.MinValue;
        
        CurrencyManager.SetCurrency(0f);
        
        OnSaveLoaded?.Invoke();
    }
}

