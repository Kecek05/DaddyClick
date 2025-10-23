using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

public class FiguresShopUI : BaseShopUI
{
    [SerializeField] [Required] FigureDataListSO _figureDataListSO;

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
        }
    }
    
}
