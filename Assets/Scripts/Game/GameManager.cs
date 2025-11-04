using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FigureDataListSO _figuresDataSO;
    [SerializeField] private DaddyDataListSO _daddyDataListSO;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
    
    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
        FigureManager.OnGainFigure += PlayerSaveOnOnGainFigure;
        DaddyManager.OnUnlockDaddy += DaddyManagerOnOnUnlockDaddy;
    }

    private void OnDestroy()
    {
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
        ClickManager.SetCurrentMultiplier(ClickUtils.GetDaddyMultiplier(_daddyDataListSO));
    }

    private IEnumerator AutoClicker()
    {
        while (true)
        {
            yield return _waitForSeconds;
            ClickManager.AddClicks(ClickManager.CPS);
        }
    }
    
    //DEBUG
    [Button]
    private void ChangeCurrencyDebug(float value)
    {
        ClickManager.SetClicks(value);
    }
}
