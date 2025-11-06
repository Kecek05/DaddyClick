using UnityEngine;

[CreateAssetMenu(fileName = "DaddyShopSO", menuName = "Scriptable Objects/Daddy/DaddyShopSO")]
public class DaddyShopSO : ScriptableObject
{
    public DaddyDataSO DaddyData;
    public float Cost;
    public float CostExponent = 1.15f;
}
