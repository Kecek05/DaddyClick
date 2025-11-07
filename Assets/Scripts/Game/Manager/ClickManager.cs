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
    
    public static void CalculateSpendMaxPossible(float baseCost, float costExponent, int currentLevel, Action<float, int> onSpend)
    {
        float availableClicks = _clicks;
        
        // Cost formula: baseCost * (level * costExponent)
        // We need to find n where sum of costs from currentLevel to (currentLevel + n - 1) <= availableClicks
        // Sum = (baseCost * costExponent) * [n * currentLevel + n*(n-1)/2]
        // This forms a quadratic equation: (a/2)*n^2 + (b - a/2)*n - availableClicks = 0
        costExponent = Mathf.Max(1f, costExponent);
        float a = baseCost * costExponent;
        float b = a * currentLevel;
        
        // Quadratic coefficients for: A*n^2 + B*n + C = 0
        float A = a / 2f;
        float B = b - a / 2f;
        float C = -availableClicks;
        
        // Quadratic formula: n = (-B + sqrt(B^2 - 4AC)) / (2A)
        float discriminant = B * B - 4 * A * C;
        if (discriminant < 0)
        {
            onSpend?.Invoke(0f, 0);
            return;
        }
        
        int levelsToBuy = Mathf.FloorToInt((-B + Mathf.Sqrt(discriminant)) / (2 * A));
        levelsToBuy = Mathf.Max(0, levelsToBuy);
        
        // Calculate actual total cost using arithmetic series sum
        float totalCost = a * levelsToBuy * (currentLevel + (levelsToBuy - 1) / 2f);
        
        onSpend?.Invoke(totalCost, levelsToBuy);
    }
    public static float CalculateCostAtLevel(float baseCost, float exponent, int level)
    {
        float levelFloat = level * exponent;
        levelFloat = Mathf.Max(1f, levelFloat);
        return baseCost * levelFloat;
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
