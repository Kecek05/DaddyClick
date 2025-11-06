using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShopItem : MonoBehaviour
{
    [Title("Base Shop Item References")]
    [SerializeField] protected TextMeshProUGUI _buyCountText;
    [SerializeField] [Required] protected TextMeshProUGUI _nameText;
    [SerializeField] [Required] protected TextMeshProUGUI _costText;
    [SerializeField] [Required] protected TextMeshProUGUI _valueText;
    [SerializeField] [Required] protected Image _itemImage;
    [SerializeField] [Required] protected GameObject _starPrefab;
    [SerializeField] [Required] protected Transform _starParent;
    [SerializeField] [Required] protected Button _buyButton;
    [SerializeField] protected Button _buyAllButton;

    protected float _clickCost;
    
    protected virtual void Awake()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyAllButton?.onClick.AddListener(OnButtonAllClick);
    }

    protected abstract void OnButtonClick();

    protected virtual void OnButtonAllClick() { }

    protected abstract void UpdateBought();
}
