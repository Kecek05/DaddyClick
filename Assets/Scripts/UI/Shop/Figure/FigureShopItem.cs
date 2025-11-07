
using UnityEngine;

public class FigureShopItem : BaseShopItem
{
    private FigureShopSO _figureShopSO;

    protected override void OnButtonClick()
    {
        if (ClickManager.CanSpendClicks(_clickCost))
        {
            ClickManager.SpendClicks(_clickCost);
            FigureManager.GainFigure(_figureShopSO.FigureData.FigureType);
            UpdateBought();
        }
    }

    protected override void OnButtonAllClick()
    {
        ClickManager.CalculateSpendMaxPossible(
            _figureShopSO.Cost,
            _figureShopSO.CostExponent,
            FigureManager.GetFigureAmountByType(_figureShopSO.FigureData.FigureType),
            (cost, quantity) =>
            {
                // Debug.Log($"Cost: {cost} / {quantity}");
                ClickManager.SpendClicks(cost);
                FigureManager.GainFigure(_figureShopSO.FigureData.FigureType, quantity);
                UpdateBought();
            });
    }
    
    public void UpdateCountText(int figureCount)
    {
        _buyCountText.text = $"{MathK.FormatNumberWithSuffix(figureCount)}";
    }

    public void SetupItem(FigureShopSO figureShopSO)
    {
        _figureShopSO = figureShopSO;
        _clickCost = _figureShopSO.Cost;
        _nameText.text = _figureShopSO.FigureData.Name;
        _valueText.text = $"+{MathK.FormatNumberWithSuffix(_figureShopSO.FigureData.CPS)}/s";
        _itemImage.sprite = _figureShopSO.FigureData.Icon;
        for (int i = 0; i < _figureShopSO.FigureData.Stars; i++)
        {
            Instantiate(_starPrefab, _starParent);
        }
        
        UpdateBought();
    }

    protected override void UpdateBought()
    {
        int currentLevel = FigureManager.GetFigureAmountByType(_figureShopSO.FigureData.FigureType);
        _clickCost = ClickManager.CalculateCostAtLevel(_figureShopSO.Cost, _figureShopSO.CostExponent, currentLevel);
        _costText.text = $"${MathK.FormatNumberWithSuffix(_clickCost)}";
    }
}
