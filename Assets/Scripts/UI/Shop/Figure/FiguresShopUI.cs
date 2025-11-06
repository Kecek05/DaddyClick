using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

public class FiguresShopUI : BaseShopUI
{
    [SerializeField] [Required] FigureDataListSO _figureDataListSO;
    private Dictionary<FigureType, FigureShopItem> _figureShopItems = new Dictionary<FigureType, FigureShopItem>();

    protected override void Awake()
    {
        base.Awake();
        FigureManager.OnGainFigure += FigureManagerOnOnGainFigure;
    }

    protected override void OnDestroy()
    {
        FigureManager.OnGainFigure -= FigureManagerOnOnGainFigure;
    }

    private void FigureManagerOnOnGainFigure(FigureType type, int amount)
    {
        if (_figureShopItems.TryGetValue(type, out FigureShopItem shopItem))
        {
            shopItem.UpdateCountText(amount);
        }
    }

    protected override void PlayerSaveOnOnSaveLoaded()
    {
        base.PlayerSaveOnOnSaveLoaded();
        foreach (var figureItem in FigureManager.BoughtFigures)
        {
            if (_figureShopItems.TryGetValue(figureItem.Key, out FigureShopItem shopItem))
            {
                shopItem.UpdateCountText(figureItem.Value);
            }
        }
    }

    protected override void SetupUI()
    {
        var sortedFigures = _figureDataListSO.FiguresShop
            .OrderBy(f => f.FigureData.Stars)
            .ThenBy(f => f.Cost)
            .ThenBy(f => f.FigureData.Name);
        
        foreach (FigureShopSO figureDataSO in sortedFigures)
        {
            FigureShopItem figureShopItem = Instantiate(_itemPrefab, _shopContentParent).GetComponent<FigureShopItem>();
            figureShopItem.SetupItem(figureDataSO);
            figureShopItem.gameObject.SetActive(false);
            _items.Add(figureShopItem.gameObject);
            _figureShopItems.Add(figureDataSO.FigureData.FigureType, figureShopItem);
        }
    }
    
}
