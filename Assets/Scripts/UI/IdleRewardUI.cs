using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdleRewardUI : MonoBehaviour
{
    [Title("References")] [SerializeField] [Required]
    private Button _closeButton;
    
    [SerializeField] [Required] TextMeshProUGUI _clickValueText;
    [SerializeField] [Required] TextMeshProUGUI _maxIdleEarningText;
    [SerializeField] [Required] Slider _clickValueSlider;
    [SerializeField] [Required] GameObject _idleRewardPanel;
    
    private void Awake()
    {
        CloseUI();
        CurrencyIdleReceiverManager.OnCurrencyIdleReceived += CurrencyIdleReceiverManagerOnOnCurrencyIdleReceived;
        _closeButton.onClick.AddListener(CloseUI);
    }

    private void OnDestroy()
    {
        CurrencyIdleReceiverManager.OnCurrencyIdleReceived -= CurrencyIdleReceiverManagerOnOnCurrencyIdleReceived;
    }

    private void CurrencyIdleReceiverManagerOnOnCurrencyIdleReceived(float currency, float maxPossibleEarnings)
    {
        _clickValueText.text = $"{MathK.FormatNumberWithSuffix(currency)}";
        _maxIdleEarningText.text = $"{MathK.FormatNumberWithSuffix(maxPossibleEarnings)}";
        _clickValueSlider.value = currency / maxPossibleEarnings;
        OpenUI();
    }

    private void CloseUI()
    {
        _idleRewardPanel.SetActive(false);
    }
    
    private void OpenUI()
    {
        _idleRewardPanel.SetActive(true);
    }
}
