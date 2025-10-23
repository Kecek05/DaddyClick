
public class DaddyShopItem : BaseShopItem
{
    private DaddyShopSO _daddyShopSO;

    protected override void OnButtonClick()
    {
        if (ClickManager.CanSpendClicks(_clickCost) && !DaddyManager.GetDaddyUnlockStatusByType(_daddyShopSO.DaddyData.DaddyType))
        {
            ClickManager.SpendClicks(_clickCost);
            DaddyManager.UnlockDaddy(_daddyShopSO.DaddyData.DaddyType);
            UpdateBought();
        }
    }

    public void SetupItem(DaddyShopSO daddyShopSO)
    {
        _daddyShopSO = daddyShopSO;
        _clickCost = _daddyShopSO.Cost;
        _nameText.text = _daddyShopSO.DaddyData.Name;
        _costText.text = $"${MathK.FormatNumber(_clickCost)}";
        _valueText.text = $"x{_daddyShopSO.DaddyData.Multiplier}";
        
        if (DaddyManager.GetDaddyUnlockStatusByType(_daddyShopSO.DaddyData.DaddyType))
            UpdateBought();
        
    }

    protected override void UpdateBought()
    {
        _costText.text = "Unlocked";
        _buyButton.interactable = false;
    }
}
