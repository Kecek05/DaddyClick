
public class FigureShopItem : BaseShopItem
{
    private FigureShopSO _figureShopSO;

    protected override void OnButtonClick()
    {
        if (CurrencyManager.CanSpendCurrency(_currentCost))
        {
            CurrencyManager.SpendCurrency(_currentCost);
            FigureManager.GainFigure(_figureShopSO.FigureData.FigureType);
            UpdateBought();
        }
    }

    public void SetupItem(FigureShopSO figureShopSO)
    {
        _figureShopSO = figureShopSO;
        _currentCost = _figureShopSO.Cost;
        _nameText.text = _figureShopSO.FigureData.Name;
        _costText.text = $"${_currentCost}";
        _valueText.text = $"+{_figureShopSO.FigureData.CPS}/s";
        
        UpdateBought();
    }

    protected override void UpdateBought()
    {
        _currentCost = _figureShopSO.Cost * _figureShopSO.CostMultiplierCurve.Evaluate(FigureManager.GetFigureAmountByType(_figureShopSO.FigureData.FigureType));
        _costText.text = $"${_currentCost}";
    }
}
