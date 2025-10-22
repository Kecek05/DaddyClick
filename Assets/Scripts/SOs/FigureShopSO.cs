using UnityEngine;

[CreateAssetMenu(fileName = "FigureShopSO", menuName = "Scriptable Objects/FigureShopSO")]
public class FigureShopSO : ScriptableObject
{
    public FigureDataSO FigureData;
    public float Cost;
}
