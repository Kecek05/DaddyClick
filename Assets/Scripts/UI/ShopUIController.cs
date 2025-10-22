using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private Button _figuresButton;
    [SerializeField] [Required] private Button _daddiesButton;
    [SerializeField] [Required] private BaseShopUI _figuresShopUI;
    [SerializeField] [Required] private BaseShopUI _daddiesShopUI;
    
    private void Awake()
    {
        _figuresButton.onClick.AddListener(() =>
        {
            _figuresShopUI.OpenShop();
        });
        
        _daddiesButton.onClick.AddListener(() =>
        {
            _daddiesShopUI.OpenShop();
        });
    }
}
