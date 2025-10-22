using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    //Temp
    public static event Action<float> OnCpsChanged;
    
    [SerializeField] private ClickUIManager _clickUIManager;
    [SerializeField] private FigureDataListSO _figuresDataSO;
    private float _cps;
    
    public float CPS => _cps;

    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    private void Start()
    {
        _clickUIManager.OnClick += ClickUIManagerOnOnClick;
        FigureManager.OnGainFigure += PlayerSaveOnOnGainFigure;
    }

    private void PlayerSaveOnOnSaveLoaded()
    {
        PlayerSaveOnOnGainFigure();
        StartCoroutine(AutoClicker());
    }

    private void OnDestroy()
    {
        _clickUIManager.OnClick -= ClickUIManagerOnOnClick;
        FigureManager.OnGainFigure -= PlayerSaveOnOnGainFigure;
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
    
    [Button]
    private void DoClick(float clickValue) 
    {
        CurrencyManager.AddCurrency(clickValue * MultiplierManager.CurrentMultiplier);
    }
    
    //DEBUG
    [Button]
    private void ChangeCurrencyDebug(float value)
    {
        CurrencyManager.SetCurrency(value);
    }
}
