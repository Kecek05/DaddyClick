using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickUIManager _clickUIManager;
    [SerializeField] private FigureDataListSO _figuresDataSO;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
    
    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
        FigureManager.OnGainFigure += PlayerSaveOnOnGainFigure;
        DaddyManager.OnUnlockDaddy += DaddyManagerOnOnUnlockDaddy;
    }

    private void Start()
    {
        _clickUIManager.OnClick += ClickUIManagerOnOnClick;
    }

    private void OnDestroy()
    {
        _clickUIManager.OnClick -= ClickUIManagerOnOnClick;
        FigureManager.OnGainFigure -= PlayerSaveOnOnGainFigure;
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
        DaddyManager.OnUnlockDaddy -= DaddyManagerOnOnUnlockDaddy;
    }
    
    private void PlayerSaveOnOnSaveLoaded()
    {
        StartCoroutine(AutoClicker());
    }

    private void PlayerSaveOnOnGainFigure(FigureType figureType, int amount)
    {
        ClickManager.SetCPS(ClickUtils.GetCPS(_figuresDataSO));
    }
    
    private void DaddyManagerOnOnUnlockDaddy()
    {
        MultiplierManager
    }

    private IEnumerator AutoClicker()
    {
        while (true)
        {
            yield return _waitForSeconds;
            DoClick(ClickManager.CPS);
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
        ClickManager.AddClicks(clickValue * MultiplierManager.CurrentMultiplier);
    }
    
    //DEBUG
    [Button]
    private void ChangeCurrencyDebug(float value)
    {
        ClickManager.SetClicks(value);
    }
}
