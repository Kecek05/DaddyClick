
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

    public void SetupItem(FigureShopSO figureShopSO)
    {
        _figureShopSO = figureShopSO;
        _clickCost = _figureShopSO.Cost;
        _nameText.text = _figureShopSO.FigureData.Name;
        _valueText.text = $"+{_figureShopSO.FigureData.CPS}/s";
        
        UpdateBought();
    }

    protected override void UpdateBought()
    {
        _clickCost = _figureShopSO.Cost * _figureShopSO.CostMultiplierCurve.Evaluate(FigureManager.GetFigureAmountByType(_figureShopSO.FigureData.FigureType));
        _costText.text = $"${MathK.FormatNumber(_clickCost)}";
    }
}
