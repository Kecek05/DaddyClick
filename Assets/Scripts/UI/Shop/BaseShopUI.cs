using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShopUI : MonoBehaviour
{
    [Title("Base Shop UI References")]
    [SerializeField] [Required] protected Button _closeButton;
    [SerializeField] [Required] protected GameObject _shopPanel;
    [SerializeField] [Required] protected Transform _shopContentParent;
    [SerializeField] [Required] protected GameObject _itemPrefab;
    
    protected virtual void Awake()
    {
        _closeButton.onClick.AddListener(CloseShop);
        CloseShop();
        
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    protected void OnDestroy()
    {
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }

    private void PlayerSaveOnOnSaveLoaded()
    {
        SetupUI();
    }

    protected abstract void SetupUI();

    protected virtual void CloseShop()
    {
        _shopPanel.SetActive(false);
    }

    public virtual void OpenShop()
    {
        _shopPanel.SetActive(true);
    }
}
