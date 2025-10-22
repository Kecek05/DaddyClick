using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class FiguresShopUI : MonoBehaviour
{
    [SerializeField] [Required] private Button _closeButton;
    [SerializeField] [Required] private GameObject _figureShopPanel;
    [SerializeField] [Required] FigureDataListSO _figureDataListSO;
    [SerializeField] [Required] private Transform _figuresShopParent;
    [SerializeField] [Required] private GameObject _figureShopItemPrefab;
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseShop);
        CloseShop();
        SetupUI();
    }

    private void SetupUI()
    {
        foreach (FigureShopSO figureDataSO in _figureDataListSO.FiguresShop)
        {
            FigureShopItem figureShopItem = Instantiate(_figureShopItemPrefab, _figuresShopParent).GetComponent<FigureShopItem>();
            figureShopItem.SetupItem(figureDataSO);
        }
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
