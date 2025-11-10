using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

public static class ClickManager
{
    public static event Action<double, Vector2> OnManualClick;
    public static event Action<double> OnClickChanged;
    public static event Action<double> OnCpsChanged;
    public static event Action<double> OnMultiplierChanged;
    
    private const string CLICKS_KEY = "PlayerClicks";
    
    
    private static double _clicks = 0.0;
    private static double _cps;
    private static double _currentMultiplier = 1.0;
    
    public static double CurrentMultiplier => _currentMultiplier;
    public static double CPS => _cps;
    public static double Clicks => _clicks;
    
    public static void SetCurrentMultiplier(double multiplier)
    {
        _currentMultiplier = multiplier;
        OnMultiplierChanged?.Invoke(_currentMultiplier);
    }

    public static void ManualClick(double amount, Vector2 position)
    {
        double manualClickAmount = amount * CurrentMultiplier;
        _clicks += manualClickAmount;
        OnManualClick?.Invoke(manualClickAmount, position);
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void AddClicks(double amount)
    {
        _clicks += amount * CurrentMultiplier;
        OnClickChanged?.Invoke(_clicks);
    }

    public static void SpendClicks(double amount)
    {
        _clicks -= amount;
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void SetClicks(double amount)
    {
        _clicks = amount;
        OnClickChanged?.Invoke(_clicks);
    }
    
    public static void SetCPS(double amount)
    {
        _cps = amount;
        OnCpsChanged?.Invoke(_cps);
    }
    
    public static bool CanSpendClicks(double amount)
    {
        return _clicks >= amount;
    }
    
    public static void CalculateSpendMaxPossible(double baseCost, double costExponent, int currentLevel, Action<double, int> onSpend)
    {
        double availableClicks = _clicks;
        
        // Cost formula: baseCost * (level * costExponent)
        // Check if we can afford at least 1 level
        double firstLevelCost = CalculateCostAtLevel(baseCost, costExponent, currentLevel);
        if (firstLevelCost > availableClicks)
        {
            onSpend?.Invoke(0.0, 0);
            return;
        }
        
        // We need to find n where sum of costs from currentLevel to (currentLevel + n - 1) <= availableClicks
        // Sum = (baseCost * costExponent) * [n * currentLevel + n*(n-1)/2]
        // This forms a quadratic equation: (a/2)*n^2 + (b - a/2)*n - availableClicks = 0
        double a = baseCost * costExponent;
        double b = a * currentLevel;
        
        // Quadratic coefficients for: A*n^2 + B*n + C = 0
        double A = a / 2.0;
        double B = b - a / 2.0;
        double C = -availableClicks;
        
        // Quadratic formula: n = (-B + sqrt(B^2 - 4AC)) / (2A)
        double discriminant = B * B - 4 * A * C;
        if (discriminant < 0)
        {
            onSpend?.Invoke(0.0, 0);
            return;
        }
        
        int levelsToBuy = (int)Math.Floor((-B + Math.Sqrt(discriminant)) / (2 * A));
        levelsToBuy = Math.Max(0, levelsToBuy);
        
        // Calculate actual total cost using arithmetic series sum
        // Sum = a * [n * currentLevel + n*(n-1)/2]
        double totalCost = a * (levelsToBuy * currentLevel + levelsToBuy * (levelsToBuy - 1) / 2.0);
        
        // Ensure we don't exceed available clicks due to floating point precision
        if (totalCost > availableClicks)
        {
            levelsToBuy--;
            totalCost = a * (levelsToBuy * currentLevel + levelsToBuy * (levelsToBuy - 1) / 2.0);
        }
        
        onSpend?.Invoke(totalCost, levelsToBuy);
    }
    public static double CalculateCostAtLevel(double baseCost, double exponent, int level)
    {
        double levelDouble = level * exponent;
        levelDouble = Math.Max(1.0, levelDouble);
        return baseCost * levelDouble;
    }
    
    public static async Task LoadClick()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>{CLICKS_KEY});
        if (playerData.TryGetValue(CLICKS_KEY, out var keyName))
        {
            _clicks = keyName.Value.GetAs<double>();
        }
    }
    
    public static async Task SaveClick()
    {
        var data = new Dictionary<string, object> { {CLICKS_KEY, _clicks } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    public static async Task ResetSave()
    {
        await CloudSaveService.Instance.Data.Player.DeleteAsync(CLICKS_KEY);
        _clicks = 0.0;
    }
}
