using Sirenix.OdinInspector;
using UnityEngine;

public class DaddiesShopUI : BaseShopUI
{
    [SerializeField] [Required] DaddyDataListSO _daddyDataListSO;

    protected override void SetupUI()
    {
        foreach (DaddyShopSO daddyShopSO in _daddyDataListSO.DaddiesShop)
        {
            DaddyShopItem daddyShopItem = Instantiate(_itemPrefab, _shopContentParent).GetComponent<DaddyShopItem>();
            daddyShopItem.SetupItem(daddyShopSO);
        }
    }

}
