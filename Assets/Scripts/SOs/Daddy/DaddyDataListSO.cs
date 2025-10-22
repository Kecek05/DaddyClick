using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// [CreateAssetMenu(fileName = "DaddyDataListSO", menuName = "Scriptable Objects/Daddy/DaddyDataListSO")]
public class DaddyDataListSO : ScriptableObject
{
    [Title("Daddy Data")]
    public List<DaddyDataSO> Daddies;
    [Space(10f)]
    
    [Title("Daddy Shop Data")]
    public List<DaddyShopSO> DaddiesShop;
    
    public DaddyShopSO GetDaddyShopSOByType(DaddyType daddyType)
    {
        return DaddiesShop.Find(dadyShop => dadyShop.DaddyData.DaddyType == daddyType);
    }

    public DaddyDataSO GetDaddyDataSOByType(DaddyType daddyType)
    {
        return Daddies.Find(daddyData => daddyData.DaddyType == daddyType);
    }
}
