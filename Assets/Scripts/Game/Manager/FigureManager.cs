using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

public static class FigureManager
{
    public static event Action<FigureType, int> OnGainFigure;
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
        OnGainFigure?.Invoke(figureType, _boughtFigures[figureType]);
    }
    
    public static void GainFigure(FigureType figureType, int amount)
    {
        _boughtFigures[figureType] += amount;
        OnGainFigure?.Invoke(figureType, _boughtFigures[figureType]);
    }
    
    public static async Task SaveFigures()
    {
        FiguresSaveData figuresData = new FiguresSaveData();
        foreach (var figurePair in _boughtFigures)
        {
            figuresData.figureTypes.Add(figurePair.Key);
            figuresData.figureCounts.Add(figurePair.Value);
        }
        
        string figuresJson = JsonUtility.ToJson(figuresData);
        
        var data = new Dictionary<string, object> { {FIGURES_KEY, figuresJson } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        
    }

    public static async Task LoadFigures()
    {
        foreach (FigureType figureType in Enum.GetValues(typeof(FigureType)))
        {
            _boughtFigures.Add(figureType, 0);
        }
        
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>{FIGURES_KEY});
        if (playerData.TryGetValue(FIGURES_KEY, out var keyName))
        {
            string figuresJson = keyName.Value.GetAs<string>();
            FiguresSaveData figuresData = JsonUtility.FromJson<FiguresSaveData>(figuresJson);
            
            if (figuresData != null && figuresData.figureTypes != null && figuresData.figureCounts != null)
            {
                for (int i = 0; i < figuresData.figureTypes.Count; i++)
                {
                    _boughtFigures[figuresData.figureTypes[i]] = figuresData.figureCounts[i];
                    OnGainFigure?.Invoke(figuresData.figureTypes[i], _boughtFigures[figuresData.figureTypes[i]]);
                }
            }
        }
    }
    
    public static async Task ResetSave()
    {
        await CloudSaveService.Instance.Data.Player.DeleteAsync(FIGURES_KEY);
        _boughtFigures.Clear();
    }
}

[Serializable]
public class FiguresSaveData
{
    public List<FigureType> figureTypes = new List<FigureType>();
    public List<int> figureCounts = new List<int>();
}