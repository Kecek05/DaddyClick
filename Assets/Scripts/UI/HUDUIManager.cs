using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _currencyText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsText;

    private void Start()
    {
        CurrencyManager.OnCurrencyChanged += CurrencyManagerOnOnCurrencyChanged;
        ClickManager.OnCpsChanged += ClickManagerOnOnCpsChanged;
    }

    private void OnDestroy()
    {
        CurrencyManager.OnCurrencyChanged -= CurrencyManagerOnOnCurrencyChanged;
        ClickManager.OnCpsChanged -= ClickManagerOnOnCpsChanged;
    }
    
    private void CurrencyManagerOnOnCurrencyChanged(float totalCurrency)
    {
        _currencyText.text = $"Clicks: {totalCurrency}";
    }
    
    private void ClickManagerOnOnCpsChanged(float newCps)
    {
        _cpsText.text = $"per second: {newCps}";
    }
}
