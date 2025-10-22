using System;

public static class CurrencyManager
{
    public static event Action<float> OnCurrencyChanged;
    
    private static float _clicksCurrency = 0;
    
    public static float ClicksCurrency => _clicksCurrency;

    public static void AddCurrency(float amount)
    {
        _clicksCurrency += amount;
        OnCurrencyChanged?.Invoke(_clicksCurrency);
    }

    public static void SpendCurrency(float amount)
    {
        _clicksCurrency -= amount;
        OnCurrencyChanged?.Invoke(_clicksCurrency);
    }
}
