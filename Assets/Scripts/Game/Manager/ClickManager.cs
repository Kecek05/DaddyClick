using System;
using UnityEngine;

public static class ClickManager
{
    public static event Action<float> OnManualClick;
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

    public static void ManualClick(float amount)
    {
        if (amount == 0f)
        {
            amount = 1f;
        }
        
        float manualClickAmount = amount * CurrentMultiplier;
        _clicks += manualClickAmount;
        OnManualClick?.Invoke(manualClickAmount);
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
