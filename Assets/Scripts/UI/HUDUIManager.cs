using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _currencyText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsText;

    private void Awake()
    {
        CurrencyManager.OnCurrencyChanged += CurrencyManagerOnOnCurrencyChanged;
        GameManager.OnCpsChanged += ClickManagerOnOnCpsChanged;
    }

    private void Start()
    {
        CurrencyManagerOnOnCurrencyChanged(PlayerSave.Clicks);
    }

    private void OnDestroy()
    {
        CurrencyManager.OnCurrencyChanged -= CurrencyManagerOnOnCurrencyChanged;
        GameManager.OnCpsChanged -= ClickManagerOnOnCpsChanged;
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
