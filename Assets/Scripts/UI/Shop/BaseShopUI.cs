using System;
using System.Collections;
using System.Collections.Generic;
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
    
    protected List<GameObject> _items = new();
    
    protected Coroutine _itemsToggleCoroutine;
    
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.1f);
    
    protected virtual void Awake()
    {
        _closeButton.onClick.AddListener(CloseShop);
        CloseShop();
        
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    protected virtual void OnDestroy()
    {
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }

    protected virtual void PlayerSaveOnOnSaveLoaded()
    {
        SetupUI();
    }

    protected abstract void SetupUI();

    protected virtual void CloseShop()
    {
        _shopPanel.SetActive(false);
        if (_itemsToggleCoroutine != null)
        {
            StopCoroutine(_itemsToggleCoroutine);
        }
        if (_items == null) return;
        foreach (var item in _items)
        {
            item.SetActive(false);
        }
    }

    public virtual void OpenShop()
    {
        _shopPanel.SetActive(true);
        
        if (_items == null) return;
        if (_itemsToggleCoroutine != null)
        {
            StopCoroutine(_itemsToggleCoroutine);
        }
        _itemsToggleCoroutine = StartCoroutine(EnableItemsCoroutine());
    }
    
    private IEnumerator EnableItemsCoroutine()
    {
        yield return null;
        foreach (var item in _items)
        {
            item.SetActive(true);
            yield return _waitForSeconds;
        }
    }
}
