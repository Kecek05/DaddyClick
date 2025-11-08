using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
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
    public static async void LoadPlayerSave()
    {
        await ClickManager.LoadClick();
        await FigureManager.LoadFigures();
        await DaddyManager.LoadDaddies();
        await LoadLastPlayedTime();
        LoadSelectedSkin();
 
        OnSaveLoaded?.Invoke();
    }

    private static void LoadSelectedSkin()
    {
        _selectedSkinType = (DaddyType)PlayerPrefs.GetInt(SKIN_KEY, 0);
    }
    
    private static async Task LoadLastPlayedTime()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>{LAST_PLAYED_TIME_KEY});
        if (playerData.TryGetValue(LAST_PLAYED_TIME_KEY, out var keyName))
        {
            string lastPlayedTimeString = keyName.Value.GetAs<string>();
            if (DateTime.TryParse(lastPlayedTimeString, out DateTime loadedTime))
            {
                _lastPlayedTime = loadedTime;
            }
        }
    }
    
    public static async Task SavePlayerData()
    {
        await ClickManager.SaveClick();
        await FigureManager.SaveFigures();
        await DaddyManager.SaveDaddies();
        await SaveLastTime();
        SaveSelectedSkin();
    }

    private static void SaveSelectedSkin()
    {
        PlayerPrefs.SetInt(SKIN_KEY, (int)_selectedSkinType);
    }
    
    private static async Task SaveLastTime()
    {
        _lastPlayedTime = DateTime.Now;
        var data = new Dictionary<string, object> { {LAST_PLAYED_TIME_KEY, _lastPlayedTime.ToString() } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }
    
    public static async Task ManualSave()
    {
        await SavePlayerData();
    }
    
    public static async void ResetSaveData()
    {
        await FigureManager.ResetSave();
        await DaddyManager.ResetSave();
        await ClickManager.ResetSave();
        await CloudSaveService.Instance.Data.Player.DeleteAsync(LAST_PLAYED_TIME_KEY);
        _selectedSkinType = DaddyType.InitialDaddy;
        _lastPlayedTime = DateTime.MinValue;
        Debug.Log($"Reset Save Data");
        OnSaveLoaded?.Invoke();
    }
    
    public static void SetSelectedSkin(DaddyType daddyType)
    {
        _selectedSkinType = daddyType;
    }
}

