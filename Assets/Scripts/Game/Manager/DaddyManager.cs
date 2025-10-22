using System;
using System.Collections.Generic;
using UnityEngine;

public static class DaddyManager
{
    private const string DADDIES_KEY = "PlayerDaddies";
    
    public static event Action OnUnlockDaddy;
    
    private static Dictionary<DaddyType, bool> _boughtDaddies = new();
    
    
    public static void UnlockDaddy(DaddyType daddyType)
    {
        _boughtDaddies[daddyType] = true;
        OnUnlockDaddy?.Invoke();
    }
    
    public static bool GetDaddyUnlockStatusByType(DaddyType daddyType)
    {
        _boughtDaddies.TryGetValue(daddyType, out bool isUnlocked);
        return isUnlocked;
    }
    
    public static void LoadDaddies()
    {
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
    
    public static void SaveDaddies()
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

    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey(DADDIES_KEY);
        _boughtDaddies.Clear();
    }
}

[Serializable]
public class DaddiesSaveData
{
    public List<DaddyType> daddyTypes = new List<DaddyType>();
    public List<bool> daddyUnlocked = new List<bool>();
}
