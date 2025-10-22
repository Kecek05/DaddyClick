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
        float cps = ClickUtils.GetCPS(_figureDataListSO);
        float idleEarnings = cps * idleTime;
        if (idleEarnings > 0)
        {
            CurrencyManager.AddCurrency(idleEarnings);
            Debug.Log($"You earned {idleEarnings} clicks while idle for {idleTime} seconds.");
        }
    }
}
