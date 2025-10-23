using System;
using System.Collections.Generic;
using UnityEngine;





public static class PlayerSave
{
    [Tooltip("Subscribe to this in AWAKE. The data is loaded in START.")]
    public static event Action OnSaveLoaded;

    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    private static DateTime _lastPlayedTime = DateTime.MinValue;

    #region PUBLICS
    
    public static DateTime LastPlayedTime => _lastPlayedTime;
    
    #endregion
    public static void LoadPlayerSave()
    {
        ClickManager.LoadClick();
        FigureManager.LoadFigures();
        DaddyManager.LoadDaddies();
        LoadLastPlayedTime();
 
        OnSaveLoaded?.Invoke();
    }
    private static void LoadLastPlayedTime()
    {
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
        ClickManager.SaveClick();
        FigureManager.SaveFigures();
        DaddyManager.SaveDaddies();
        SaveLastTime();
        
        PlayerPrefs.Save();
    }

    private static void SaveLastTime()
    {
        _lastPlayedTime = DateTime.Now;
        PlayerPrefs.SetString(LAST_PLAYED_TIME_KEY, _lastPlayedTime.ToString());
    }
    
    public static void ManualSave()
    {
        SavePlayerData();
    }
    
    public static void ResetSaveData()
    {
        FigureManager.ResetSave();
        DaddyManager.ResetSave();
        ClickManager.ResetSave();
        PlayerPrefs.DeleteKey(LAST_PLAYED_TIME_KEY);
        _lastPlayedTime = DateTime.MinValue;
        
        OnSaveLoaded?.Invoke();
    }
}

