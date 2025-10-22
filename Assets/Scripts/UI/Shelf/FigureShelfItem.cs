using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class FigureShelfItem : MonoBehaviour
{
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsTotalText;
    private FigureDataSO _figureDataSO;

    public void SetupItem(FigureDataSO figureDataSO)
    {
        _figureDataSO = figureDataSO;
        _nameText.text = _figureDataSO.Name;
        _cpsTotalText.text = $"{_figureDataSO.CPS}/s";
    }

    public void UpdateAmount(int amount)
    {
        _cpsTotalText.text = $"{_figureDataSO.CPS * amount}/s";
    }
}
