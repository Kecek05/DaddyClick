using UnityEngine;

[CreateAssetMenu(fileName = "FigureShopSO", menuName = "Scriptable Objects/Figure/FigureShopSO")]
public class FigureShopSO : ScriptableObject
{
    public FigureDataSO FigureData;
    public float Cost;
    public float CostExponent = 1.05f;
}
