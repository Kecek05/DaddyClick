using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// [CreateAssetMenu(fileName = "FigureDataListSO", menuName = "Scriptable Objects/Figure/FigureDataListSO")]
public class FigureDataListSO : ScriptableObject
{
    [Title("Figures Data")]
    public List<FigureDataSO> Figures;
    [Space(10f)]
    
    [Title("Figures Shop Data")]
    public List<FigureShopSO> FiguresShop;
    
    public FigureShopSO GetFigureShopSOByType(FigureType figureType)
    {
        return FiguresShop.Find(figureShop => figureShop.FigureData.FigureType == figureType);
    }

    public FigureDataSO GetFigureDataSOByType(FigureType figureType)
    {
        return Figures.Find(figureData => figureData.FigureType == figureType);
    }
}
