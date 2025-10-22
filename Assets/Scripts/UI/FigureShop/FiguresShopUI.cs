using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class FiguresShopUI : MonoBehaviour
{
    [SerializeField] [Required] private Button _closeButton;
    [SerializeField] [Required] private GameObject _figureShopPanel;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseShop);
        CloseShop();
    }

    private void CloseShop()
    {
        _figureShopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        _figureShopPanel.SetActive(true);
    }
}
