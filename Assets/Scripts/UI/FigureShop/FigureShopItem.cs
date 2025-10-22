using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureShopItem : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private TextMeshProUGUI _costText;
    [SerializeField] [Required] private TextMeshProUGUI _cPSText;
    [SerializeField] [Required] private Button _buyButton;

    private float _currentCost;
    private FigureShopSO _figureShopSO;
    
    private void Awake()
    {
        _buyButton.onClick.AddListener(() =>
        {
            if (CurrencyManager.CanSpendCurrency(_currentCost))
            {
                CurrencyManager.SpendCurrency(_currentCost);
                PlayerSave.GainFigure(_figureShopSO.FigureData.FigureType);
                UpdateCost();
            }
        });
    }

    public void SetupItem(FigureShopSO figureShopSO)
    {
        _figureShopSO = figureShopSO;
        _currentCost = _figureShopSO.Cost;
        _nameText.text = _figureShopSO.FigureData.Name;
        _costText.text = $"${_currentCost}";
        _cPSText.text = $"+{_figureShopSO.FigureData.CPS}/s";
    }

    private void UpdateCost()
    {
        _currentCost = _figureShopSO.Cost * _figureShopSO.CostMultiplierCurve.Evaluate(PlayerSave.GetFigureAmountByType(_figureShopSO.FigureData.FigureType));
        _costText.text = $"${_currentCost}";
    }
    
    
}
