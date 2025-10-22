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
    
    
    private void Start()
    {
        _clickUIManager.OnClick += ClickUIManagerOnOnClick;
        PlayerSave.OnGainFigure += PlayerSaveOnOnGainFigure;
        
        StartCoroutine(AutoClicker());
    }

    private void OnDestroy()
    {
        _clickUIManager.OnClick -= ClickUIManagerOnOnClick;
        PlayerSave.OnGainFigure -= PlayerSaveOnOnGainFigure;
    }

    private void PlayerSaveOnOnGainFigure()
    {
        _cps = 0;
        foreach (var figureData in _figuresDataSO.Figures)
        {
            int figureCount = 0;
            PlayerSave.Figures.TryGetValue(figureData.FigureType, out figureCount);
            _cps += figureData.CPS * figureCount;
        }
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
