
using UnityEngine;

public class DaddyShopItem : BaseShopItem
{
    private DaddyShopSO _daddyShopSO;
    private Material _defaultMaterial;

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
        _costText.text = $"${MathK.FormatNumberWithSuffix(_clickCost)}";
        _valueText.text = $"+x{_daddyShopSO.DaddyData.Multiplier}";
        _itemImage.sprite = _daddyShopSO.DaddyData.Icon;
        _itemImage.color = Color.black;
        for (int i = 0; i < _daddyShopSO.DaddyData.Stars; i++)
        {
            Instantiate(_starPrefab, _starParent);
        }
        
        if (DaddyManager.GetDaddyUnlockStatusByType(_daddyShopSO.DaddyData.DaddyType))
            UpdateBought();
        
    }

    protected override void UpdateBought()
    {
        _costText.text = "Unlocked";
        _itemImage.color = Color.white;
        _buyButton.interactable = false;
    }
}
