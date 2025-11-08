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
    

    private void ClickManagerOnOnCpsChanged(double newCps)
    {
        _cpsText.text = $"<CPSswing>{MathK.FormatNumberWithSuffix(newCps)}</CPSswing>/s";
    }

    private void ClickManagerOnOnClickChanged(double clicks)
    {
        _currencyText.text = $"<coinBounce>{MathK.FormatNumberWithSuffix(clicks)}</coinBounce>";
    }

    private void ClickManagerOnOnMultiplierChanged(double multiplier)
    {
        _multiplierText.text = $"<Multiplierincr>x{multiplier}</Multiplierincr>";    
    }
}
