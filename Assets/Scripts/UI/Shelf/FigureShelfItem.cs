using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureShelfItem : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsTotalText;
    [SerializeField] [Required] private TextMeshProUGUI _countText;
    [SerializeField] [Required] private GameObject _starPrefab;
    [SerializeField] [Required] private Transform _starParent;
    [SerializeField] [Required] private Image _figureImage;
    
    private FigureDataSO _figureDataSO;

    public void SetupItem(FigureDataSO figureDataSO)
    {
        _figureDataSO = figureDataSO;
        _nameText.text = _figureDataSO.Name;
        _cpsTotalText.text = $"{MathK.FormatNumberWithSuffix(_figureDataSO.CPS)}/s";
        _figureImage.sprite = _figureDataSO.Icon;
        for (int i = 0; i < _figureDataSO.Stars; i++)
        {
            Instantiate(_starPrefab, _starParent);
        }
    }

    public void UpdateAmount(int amount)
    {
        _cpsTotalText.text = $"{MathK.FormatNumberWithSuffix(_figureDataSO.CPS * amount)}/s";
        _countText.text = $"{MathK.FormatNumberWithSuffix(amount)}";
    }
}
