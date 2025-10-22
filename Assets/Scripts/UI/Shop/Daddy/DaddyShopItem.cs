
public class DaddyShopItem : BaseShopItem
{
    private DaddyShopSO _daddyShopSO;

    protected override void OnButtonClick()
    {
        if (CurrencyManager.CanSpendCurrency(_currentCost) && !PlayerSave.GetDaddyUnlockStatusByType(_daddyShopSO.DaddyData.DaddyType))
        {
            CurrencyManager.SpendCurrency(_currentCost);
            PlayerSave.UnlockDaddy(_daddyShopSO.DaddyData.DaddyType);
            UpdateBought();
        }
    }

    public void SetupItem(DaddyShopSO daddyShopSO)
    {
        _daddyShopSO = daddyShopSO;
        _currentCost = _daddyShopSO.Cost;
        _nameText.text = _daddyShopSO.DaddyData.Name;
        _costText.text = $"${_currentCost}";
        _valueText.text = $"x{_daddyShopSO.DaddyData.Multiplier}";
        
        if (PlayerSave.GetDaddyUnlockStatusByType(_daddyShopSO.DaddyData.DaddyType))
            UpdateBought();
        
    }

    protected override void UpdateBought()
    {
        _costText.text = "Unlocked";
        _buyButton.interactable = false;
    }
}
