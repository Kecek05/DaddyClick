using System;
using System.Collections;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    //Temp
    public static event Action<float> OnCpsChanged;
    
    [SerializeField] private ClickUIManager _clickUIManager;
    [SerializeField] private FigureDataListSO _figuresDataSO;
    private float _cps;
    
    public float CPS => _cps;
    
    private void Start()
    {
        _clickUIManager.OnClick += ClickUIManagerOnOnClick;
        PlayerSave.OnGainFigure += PlayerSaveOnOnGainFigure;
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    private void PlayerSaveOnOnSaveLoaded()
    {
        PlayerSaveOnOnGainFigure();
        StartCoroutine(AutoClicker());
    }

    private void OnDestroy()
    {
        _clickUIManager.OnClick -= ClickUIManagerOnOnClick;
        PlayerSave.OnGainFigure -= PlayerSaveOnOnGainFigure;
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }

    private void PlayerSaveOnOnGainFigure()
    {
        _cps = ClickUtils.GetCPS(_figuresDataSO);
        OnCpsChanged?.Invoke(_cps);
    }

    private IEnumerator AutoClicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            DoClick(_cps);
        }
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
