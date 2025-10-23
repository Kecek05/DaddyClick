using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShelfUIManager : MonoBehaviour
{
    [SerializeField] [Required] private GameObject _itemShelfPrefab;
    [SerializeField] [Required] private Transform _shelfParent;
    [SerializeField] [Required] private FigureDataListSO _figureDataListSO;
    private Dictionary<FigureType, FigureShelfItem>  _figureShelfItems = new Dictionary<FigureType, FigureShelfItem>();
    private void Awake()
    {
        FigureManager.OnGainFigure += PlayerSaveOnOnGainFigure;
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    private void OnDestroy()
    {
        FigureManager.OnGainFigure -= PlayerSaveOnOnGainFigure;
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }
    
    private void PlayerSaveOnOnSaveLoaded()
    {
        var sortedFigures = FigureManager.BoughtFigures
            .OrderBy(f => _figureDataListSO.GetFigureDataSOByType(f.Key).Stars)
            .ThenBy(f => f.Key);
        
        foreach (var figureItem in sortedFigures)
        {
            PlayerSaveOnOnGainFigure(figureItem.Key, figureItem.Value);
        }
    }

    private void PlayerSaveOnOnGainFigure(FigureType type, int figureCount)
    {
        if (figureCount <= 0) return;
        
        if (!_figureShelfItems.ContainsKey(type))
        {
            CreateShelfItem(type);
        }
        UpdateFigureItem(type,figureCount);
    }

    private void CreateShelfItem(FigureType figureType)
    {
        FigureShelfItem newItem = Instantiate(_itemShelfPrefab, _shelfParent).GetComponent<FigureShelfItem>();
        newItem.SetupItem(_figureDataListSO.GetFigureDataSOByType(figureType));
        _figureShelfItems.Add(figureType, newItem);
    }

    private void UpdateFigureItem(FigureType figureType, int figureAmount)
    {
        if (_figureShelfItems.ContainsKey(figureType))
        {
            _figureShelfItems[figureType].UpdateAmount(figureAmount);
        }
        else
            Debug.LogWarning($"No figure shelf item found for figure type: {figureType}");
        
    }
}
