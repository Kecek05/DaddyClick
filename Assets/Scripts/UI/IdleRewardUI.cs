using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdleRewardUI : MonoBehaviour
{
    [Title("References")] [SerializeField] [Required]
    private Button _closeButton;
    
    [SerializeField] [Required] TextMeshProUGUI _clickValueText;
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

    private void CurrencyIdleReceiverManagerOnOnCurrencyIdleReceived(float clicks)
    {
        _clickValueText.text = $"Clicks: {MathK.FormatNumber(clicks)}";
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
