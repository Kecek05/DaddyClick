using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _currencyText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsText;

    private void Awake()
    {
        ClickManager.OnClickChanged += ClickManagerOnOnClickChanged;
        ClickManager.OnCpsChanged += ClickManagerOnOnCpsChanged;
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


}
