using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private Button _figuresButton;
    [SerializeField] [Required] private Button _daddiesButton;
    [SerializeField] [Required] private FiguresShopUI _figuresShopUI;

    private void Awake()
    {
        _figuresButton.onClick.AddListener(() =>
        {
            _figuresShopUI.OpenShop();
        });
    }
}
