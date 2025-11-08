using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DaddyShopSO", menuName = "Scriptable Objects/Daddy/DaddyShopSO")]
public class DaddyShopSO : ScriptableObject
{
    [ValidateInput("ValidateDaddyData", "The first 5 letters of DaddyData file must match this file's first 5 letters")]
    public DaddyDataSO DaddyData;
    public double Cost;
    public double CostExponent = 1.15;
    
    private bool ValidateDaddyData(DaddyDataSO value)
    {
        if (value == null) return true; // Allow null values
        
        string thisFileName = this.name;
        string daddyDataFileName = value.name;
        
        // Check if both names have at least 5 characters
        if (thisFileName.Length < 5 || daddyDataFileName.Length < 5)
            return false;
        
        // Compare first 5 letters
        string thisPrefix = thisFileName.Substring(0, 5);
        string daddyPrefix = daddyDataFileName.Substring(0, 5);
        
        return thisPrefix.Equals(daddyPrefix, System.StringComparison.OrdinalIgnoreCase);
    }
}
