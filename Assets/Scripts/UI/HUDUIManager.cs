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
        _cpsText.text = $"<CPSswing>{MathK.FormatNumber(newCps)}</CPSswing>/s";
    }

    private void ClickManagerOnOnClickChanged(float clicks)
    {
        _currencyText.text = $"<coinBounce>{MathK.FormatNumber(clicks)}</coinBounce>";
    }

    private void ClickManagerOnOnMultiplierChanged(float multiplier)
    {
        _multiplierText.text = $"<Multiplierincr>x{MathK.FormatNumber(multiplier)}</Multiplierincr>";    
    }
}
