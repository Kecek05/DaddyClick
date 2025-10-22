using System;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private ClickUIManager _clickUIManager;
    
    private void Start()
    {
        _clickUIManager.OnClick += ClickUIManagerOnOnClick;
    }

    private void ClickUIManagerOnOnClick()
    {
        ManualClick();
    }


    private void ManualClick() 
    {
        DoClick(1);
    }
    
    private void DoClick(float clickValue) 
    {
        CurrencyManager.AddCurrency(clickValue * MultiplierManager.CurrentMultiplier);
    }
}
