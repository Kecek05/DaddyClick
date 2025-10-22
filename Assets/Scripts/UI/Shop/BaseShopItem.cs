using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShopItem : MonoBehaviour
{
    [Title("Base Shop Item References")]
    [SerializeField] [Required] protected TextMeshProUGUI _nameText;
    [SerializeField] [Required] protected TextMeshProUGUI _costText;
    [SerializeField] [Required] protected TextMeshProUGUI _valueText;
    [SerializeField] [Required] protected Button _buyButton;

    protected float _currentCost;
    
    protected virtual void Awake()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();

    protected abstract void UpdateBought();
}
