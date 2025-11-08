using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "FigureShopSO", menuName = "Scriptable Objects/Figure/FigureShopSO")]
public class FigureShopSO : ScriptableObject
{
    [ValidateInput("ValidateFigureData", "The first 5 letters of FigureData file must match this file's first 5 letters")]
    public FigureDataSO FigureData;
    public double Cost;
    public double CostExponent = 1.05;
    
    private bool ValidateFigureData(FigureDataSO value)
    {
        if (value == null) return true; // Allow null values
        
        string thisFileName = this.name;
        string figureDataFileName = value.name;
        
        // Check if both names have at least 5 characters
        if (thisFileName.Length < 5 || figureDataFileName.Length < 5)
            return false;
        
        // Compare first 5 letters
        string thisPrefix = thisFileName.Substring(0, 5);
        string figurePrefix = figureDataFileName.Substring(0, 5);
        
        return thisPrefix.Equals(figurePrefix, System.StringComparison.OrdinalIgnoreCase);
    }
}
