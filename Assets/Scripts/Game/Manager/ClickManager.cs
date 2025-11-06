using System;
using System.Collections;
using UnityEngine;

public static class ClickManager
{
    public static event Action<float, Vector2> OnManualClick;
    public static event Action<float> OnClickChanged;
    public static event Action<float> OnCpsChanged;
    public static event Action<float> OnMultiplierChanged;
    
    private const string CLICKS_KEY = "PlayerClicks";
    
    
    private static float _clicks = 0f;
    private static float _cps;
    private static float _currentMultiplier = 1f;
    
    public static float CurrentMultiplier => _currentMultiplier;
    public static float CPS => _cps;
    public static double Clicks => _clicks;
    
    public static void SetCurrentMultiplier(float multiplier)
    {
        _currentMultiplier = multiplier;
        OnMultiplierChanged?.Invoke(_currentMultiplier);
    }

    public static void ManualClick(float amount, Vector2 position)
    {
        float manualClickAmount = amount * CurrentMultiplier;
        _clicks += manualClickAmount;
        OnManualClick?.Invoke(manualClickAmount, position);
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void AddClicks(float amount)
    {
        _clicks += amount * CurrentMultiplier;
        OnClickChanged?.Invoke(_clicks);
    }

    public static void SpendClicks(float amount)
    {
        _clicks -= amount;
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void SetClicks(float amount)
    {
        _clicks = amount;
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void SetCPS(float amount)
    {
        _cps = amount;
        OnCpsChanged?.Invoke(_cps);
    }
    
    public static bool CanSpendClicks(float amount)
    {
        return _clicks >= amount;
    }

    /// <summary>
    /// Calculates the maximum number of purchases possible using exponential cost growth.
    /// Cost formula: baseCost * (costExponent ^ currentLevel)
    /// Total cost formula (geometric series): baseCost * (costExponent^start) * (1 - costExponent^quantity) / (1 - costExponent)
    /// </summary>
    public static void CalculateSpendMaxPossible(float baseCost, float costExponent, int currentLevel, Action<float, int> onSpend)
    {
        float availableClicks = _clicks;
        
        // Binary search to find the maximum number of purchases
        int left = 0;
        int right = 10000; // Start with reasonable upper bound
        int maxPurchases = 0;
        
        // First, expand the search range if needed
        while (CalculateTotalCostExponential(baseCost, costExponent, currentLevel, right) <= availableClicks)
        {
            left = right;
            right *= 2;
            
            // Safety check to prevent infinite loop
            if (right > 1000000)
            {
                maxPurchases = right / 2;
                break;
            }
        }
        
        // Binary search for exact maximum
        if (maxPurchases == 0)
        {
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                float totalCost = CalculateTotalCostExponential(baseCost, costExponent, currentLevel, mid);
                
                if (totalCost <= availableClicks)
                {
                    maxPurchases = mid;
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }
        Debug.Log($"Quantity: {maxPurchases}");
        // Return the result
        if (maxPurchases > 0)
        {
            float totalCost = CalculateTotalCostExponential(baseCost, costExponent, currentLevel, maxPurchases);
            onSpend?.Invoke(totalCost, maxPurchases);
            Debug.Log($"Total cost: {totalCost} - Quantity: {maxPurchases}");
        }
    }
    
    /// <summary>
    /// Calculates the total cost for purchasing 'quantity' items using geometric series formula.
    /// Formula: baseCost * (exponent^startLevel) * (1 - exponent^quantity) / (1 - exponent)
    /// </summary>
    private static float CalculateTotalCostExponential(float baseCost, float exponent, int startLevel, int quantity)
    {
        if (quantity <= 0) return 0f;
        
        // If exponent is 1, cost is constant
        if (Mathf.Approximately(exponent, 1f))
        {
            return baseCost * quantity;
        }
        
        // Geometric series formula: a * (1 - r^n) / (1 - r)
        // where a = first term, r = ratio, n = number of terms
        float firstTerm = baseCost * Mathf.Pow(exponent, startLevel);
        float ratio = exponent;
        float numerator = 1f - Mathf.Pow(ratio, quantity);
        float denominator = 1f - ratio;
        
        return firstTerm * (numerator / denominator);
    }
    
    /// <summary>
    /// Calculates the cost at a specific level.
    /// Formula: baseCost * (exponent ^ level)
    /// </summary>
    public static float CalculateCostAtLevel(float baseCost, float exponent, int level)
    {
        Debug.Log($"Cost: {baseCost} * {exponent}^{level} = {baseCost * Mathf.Pow(exponent, level)}");
        return baseCost * Mathf.Pow(exponent, level);
    }
    
    public static void LoadClick()
    {
        _clicks = PlayerPrefs.GetFloat(CLICKS_KEY, 0f);
    }
    
    public static void SaveClick()
    {
        PlayerPrefs.SetFloat(CLICKS_KEY, _clicks);
    }

    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey(CLICKS_KEY);
        _clicks = 0f;
    }
}
