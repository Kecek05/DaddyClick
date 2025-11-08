using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

public static class DaddyManager
{
    private const string DADDIES_KEY = "PlayerDaddies";
    
    public static event Action OnUnlockDaddy;
    
    private static Dictionary<DaddyType, bool> _boughtDaddies = new();
    
    public static Dictionary<DaddyType, bool> BoughtDaddies => _boughtDaddies;
    
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
    
    public static async Task LoadDaddies()
    {
        foreach (DaddyType daddyType in Enum.GetValues(typeof(DaddyType)))
        {
            _boughtDaddies.Add(daddyType, false);
        }

        _boughtDaddies[DaddyType.InitialDaddy] = true;
        
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>{DADDIES_KEY});
        if (playerData.TryGetValue(DADDIES_KEY, out var keyName))
        {
            string daddiesJson = keyName.Value.GetAs<string>();
            DaddiesSaveData daddiesData = JsonUtility.FromJson<DaddiesSaveData>(daddiesJson);
            
            if (daddiesData != null && daddiesData.daddyTypes != null && daddiesData.daddyUnlocked != null)
            {
                for (int i = 0; i < daddiesData.daddyTypes.Count; i++)
                {
                    _boughtDaddies[daddiesData.daddyTypes[i]] = daddiesData.daddyUnlocked[i];
                }
                OnUnlockDaddy?.Invoke();
            }
        }
    }
    
    public static async Task SaveDaddies()
    {
        DaddiesSaveData daddiesData = new DaddiesSaveData();
        foreach (var daddyPair in _boughtDaddies)
        {
            daddiesData.daddyTypes.Add(daddyPair.Key);
            daddiesData.daddyUnlocked.Add(daddyPair.Value);
        }
        
        string daddiesJson = JsonUtility.ToJson(daddiesData);
        
        var data = new Dictionary<string, object> { {DADDIES_KEY, daddiesJson } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    public static async Task ResetSave()
    {
        await CloudSaveService.Instance.Data.Player.DeleteAsync(DADDIES_KEY);
        _boughtDaddies.Clear();
    }
}

[Serializable]
public class DaddiesSaveData
{
    public List<DaddyType> daddyTypes = new List<DaddyType>();
    public List<bool> daddyUnlocked = new List<bool>();
}
