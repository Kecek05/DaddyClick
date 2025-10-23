using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _currencyText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsText;
    [SerializeField] [Required] private TextMeshProUGUI _multiplierText;

    private void Awake()
    {
        ClickManager.OnClickChanged += ClickManagerOnOnClickChanged;
        ClickManager.OnCpsChanged += ClickManagerOnOnCpsChanged;
        ClickManager.OnMultiplierChanged += ClickManagerOnOnMultiplierChanged;
    }

    private void OnDestroy()
    {
        ClickManager.OnClickChanged -= ClickManagerOnOnClickChanged;
        ClickManager.OnCpsChanged -= ClickManagerOnOnCpsChanged;
    }
    

    private void ClickManagerOnOnCpsChanged(float newCps)
    {
        _cpsText.text = $"per second: {MathK.FormatNumber(newCps)}";
    }

    private void ClickManagerOnOnClickChanged(float clicks)
    {
        _currencyText.text = $"Clicks: {MathK.FormatNumber(clicks)}";
    }

    private void ClickManagerOnOnMultiplierChanged(float multiplier)
    {
        _multiplierText.text = $"Multiplier: x{MathK.FormatNumber(multiplier)}";    
    }
}
