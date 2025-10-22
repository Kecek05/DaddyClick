using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CurrencyIdleReceiverManager : MonoBehaviour
{
    [SerializeField] [Required] private FigureDataListSO _figureDataListSO;
    
    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    private void OnDestroy()
    {
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }

    
    private void PlayerSaveOnOnSaveLoaded()
    {
        float idleTime = (float)(DateTime.Now - PlayerSave.LastPlayedTime).TotalSeconds;
        float cps = 0;
        foreach (var figureData in PlayerSave.Figures)
        {
            int figureCount = 0;
            PlayerSave.Figures.TryGetValue(figureData.Key, out figureCount);
            cps += _figureDataListSO.GetFigureDataSOByType(figureData.Key).CPS * figureCount;
        }
        
        float idleEarnings = cps * idleTime;
        if (idleEarnings > 0)
        {
            CurrencyManager.AddCurrency(idleEarnings);
            Debug.Log($"You earned {idleEarnings} clicks while idle for {idleTime} seconds.");
        }
    }
}
