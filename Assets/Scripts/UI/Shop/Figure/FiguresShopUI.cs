using Sirenix.OdinInspector;
using UnityEngine;

public class FiguresShopUI : BaseShopUI
{
    [SerializeField] [Required] FigureDataListSO _figureDataListSO;

    protected override void SetupUI()
    {
        foreach (FigureShopSO figureDataSO in _figureDataListSO.FiguresShop)
        {
            FigureShopItem figureShopItem = Instantiate(_itemPrefab, _shopContentParent).GetComponent<FigureShopItem>();
            figureShopItem.SetupItem(figureDataSO);
        }
    }
    
}
