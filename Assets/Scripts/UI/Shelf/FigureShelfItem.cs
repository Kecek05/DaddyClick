using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class FigureShelfItem : MonoBehaviour
{
    [SerializeField] [Required] FigureDataSO _figureDataSO;
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private TextMeshProUGUI _cpsTotalText;

    public void SetupItem()
    {
        _nameText.text = _figureDataSO.Name;
        // _cpsTotalText.text;
    }
}
