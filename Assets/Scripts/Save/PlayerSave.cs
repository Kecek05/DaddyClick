using System;
using System.Collections.Generic;
using UnityEngine;





public static class PlayerSave
{
    [Tooltip("Subscribe to this in AWAKE. The data is loaded in START.")]
    public static event Action OnSaveLoaded;

    private const string LAST_PLAYED_TIME_KEY = "LastPlayedTime";
    private const string SKIN_KEY = "SelectedSkinType";
    private static DateTime _lastPlayedTime = DateTime.MinValue;
    private static DaddyType _selectedSkinType;

    #region PUBLICS
    
    public static DateTime LastPlayedTime => _lastPlayedTime;
    
    public static DaddyType SelectedSkinType => _selectedSkinType;
    
    #endregion
    public static void LoadPlayerSave()
    {
        ClickManager.LoadClick();
        FigureManager.LoadFigures();
        DaddyManager.LoadDaddies();
        LoadLastPlayedTime();
        LoadSelectedSkin();
 
        OnSaveLoaded?.Invoke();
    }

    private static void LoadSelectedSkin()
    {
        _selectedSkinType = (DaddyType)PlayerPrefs.GetInt(SKIN_KEY, 1);
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
        SaveSelectedSkin();
        
        PlayerPrefs.Save();
    }

    private static void SaveSelectedSkin()
    {
        PlayerPrefs.SetInt(SKIN_KEY, (int)_selectedSkinType);
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
    
    public static void SetSelectedSkin(DaddyType daddyType)
    {
        _selectedSkinType = daddyType;
    }
}

