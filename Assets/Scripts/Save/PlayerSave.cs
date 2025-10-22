using System;
using System.Collections.Generic;
using UnityEngine;





public static class PlayerSave
{
    [Tooltip("Subscribe to this in AWAKE. The data is loaded in START.")]
    public static event Action OnSaveLoaded;
    
    private const string CLICKS_KEY = "PlayerClicks";

    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    
    private static float _clicks = 0f;
    private static DateTime _lastPlayedTime = DateTime.MinValue;

    #region PUBLICS
    

    public static float Clicks => _clicks;
    public static DateTime LastPlayedTime => _lastPlayedTime;
    
    #endregion
    public static void LoadPlayerSave()
    {
        LoadClicks();
        FigureManager.LoadFigures();
        DaddyManager.LoadDaddies();
        LoadLastPlayedTime();
 
        OnSaveLoaded?.Invoke();
    }

    private static void LoadClicks()
    {
        // Load Clicks
        _clicks = PlayerPrefs.GetFloat(CLICKS_KEY, 0f);
        CurrencyManager.SetCurrency(_clicks);
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
    
    public static void SavePlayerData()
    {
        SaveClicks();
        FigureManager.SaveFigures();
        DaddyManager.SaveDaddies();
        SaveLastTime();
        
        PlayerPrefs.Save();
    }

    private static void SaveClicks()
    {
        PlayerPrefs.SetFloat(CLICKS_KEY, _clicks);
    }

    private static void SaveLastTime()
    {
        _lastPlayedTime = DateTime.Now;
        PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, _lastPlayedTime.ToString());
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
        FigureManager.ResetSave();
        DaddyManager.ResetSave();
        PlayerPrefs.DeleteKey(CLICKS_KEY);
        PlayerPrefs.DeleteKey(LAST_PLAYED_TIME_KEY);
        
        _clicks = 0f;
        _lastPlayedTime = DateTime.MinValue;
        
        CurrencyManager.SetCurrency(0f);
        
        OnSaveLoaded?.Invoke();
    }
}

