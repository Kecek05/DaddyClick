using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _currencyText;

    private void Start()
    {
        CurrencyManager.OnCurrencyChanged += CurrencyManagerOnOnCurrencyChanged;
    }

    private void OnDestroy()
    {
        CurrencyManager.OnCurrencyChanged -= CurrencyManagerOnOnCurrencyChanged;
    }
    
    private void CurrencyManagerOnOnCurrencyChanged(float totalCurrency)
    {
        _currencyText.text = $"Clicks: {totalCurrency}";
    }
}
