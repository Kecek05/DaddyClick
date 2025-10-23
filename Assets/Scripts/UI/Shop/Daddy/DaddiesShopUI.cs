using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

public class DaddiesShopUI : BaseShopUI
{
    [SerializeField] [Required] DaddyDataListSO _daddyDataListSO;

    protected override void SetupUI()
    {
        var sortedDaddies = _daddyDataListSO.DaddiesShop
            .OrderBy(d => d.DaddyData.Stars)
            .ThenBy(d => d.Cost);
        
        foreach (DaddyShopSO daddyShopSO in sortedDaddies)
        {
            DaddyShopItem daddyShopItem = Instantiate(_itemPrefab, _shopContentParent).GetComponent<DaddyShopItem>();
            daddyShopItem.SetupItem(daddyShopSO);
        }
    }

}
