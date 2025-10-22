using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureShopItem : MonoBehaviour
{
    [SerializeField] [Required] private FigureShopSO _figureShopSO;
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private TextMeshProUGUI _costText;
    [SerializeField] [Required] private TextMeshProUGUI _cPSText;
    [SerializeField] [Required] private Button _buyButton;

    private void Awake()
    {
        _buyButton.onClick.AddListener(() =>
        {
            if (CurrencyManager.CanSpendCurrency(_figureShopSO.Cost))
            {
                CurrencyManager.SpendCurrency(_figureShopSO.Cost);
                PlayerSave.GainFigure(_figureShopSO.FigureData.FigureType);
            }
        });
        
        SetupItem();
    }

    private void SetupItem()
    {
        _nameText.text = _figureShopSO.FigureData.Name;
        _costText.text = $"${_figureShopSO.Cost}";
        _cPSText.text = $"+{_figureShopSO.FigureData.CPS}/s";
    }
}
