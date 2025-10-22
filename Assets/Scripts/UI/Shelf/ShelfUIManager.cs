using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShelfUIManager : MonoBehaviour
{
    [SerializeField] [Required] private GameObject _itemShelfPrefab;
    [SerializeField] [Required] private Transform _shelfParent;
    [SerializeField] [Required] private FigureDataListSO _figureDataListSO;
    private Dictionary<FigureType, FigureShelfItem>  _figureShelfItems = new Dictionary<FigureType, FigureShelfItem>();
    private void Start()
    {
        PlayerSave.OnGainFigure += PlayerSaveOnOnGainFigure;
    }

    private void OnDestroy()
    {
        PlayerSave.OnGainFigure -= PlayerSaveOnOnGainFigure;
    }

    private void PlayerSaveOnOnGainFigure()
    {
        foreach (var item in PlayerSave.boughtFigures)
        {
            if (item.Value > 0)
            {
                if (!_figureShelfItems.ContainsKey(item.Key))
                {
                    CreateShelfItem(item.Key);
                }
                UpdateFigureItem(item.Key, item.Value);
            }
        }
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
