using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CurrencyIdleReceiverManager : MonoBehaviour
{
    /// <summary>
    /// double: amount of currency received from idle time
    /// double2: maxPossibleEarnings
    /// </summary>
    public static event Action<double, double> OnCurrencyIdleReceived;
    
    [SerializeField] [Required] private FigureDataListSO _figureDataListSO;
    [SerializeField] private double _baseMaxIdleEarnings = 1000.0;
    
    private DateTime _pauseStartTime;
    
    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
    }

    private void OnDestroy()
    {
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            _pauseStartTime = DateTime.Now;
        else
            CalculateAndGrantIdleEarnings(_pauseStartTime);
    }

    private void PlayerSaveOnOnSaveLoaded()
    {
        CalculateAndGrantIdleEarnings(PlayerSave.LastPlayedTime);
    }
    
    private void CalculateAndGrantIdleEarnings(DateTime startTime)
    {
        if (ClickUtils.GetCPS(_figureDataListSO) == 0.0)
            return;
        
        double idleTime = (DateTime.Now - startTime).TotalSeconds;
        double cps = Math.Max(1.0, ClickUtils.GetCPS(_figureDataListSO) / 10.0);
        double multiplier = Math.Max(1.0, ClickManager.CurrentMultiplier);
        double idleEarnings = cps * idleTime * multiplier;
        double maxIdleEarnings = _baseMaxIdleEarnings * cps * multiplier;
        if (idleEarnings > 0)
        {
            idleEarnings = Math.Min(idleEarnings, maxIdleEarnings);
            ClickManager.AddClicks(idleEarnings);
            OnCurrencyIdleReceived?.Invoke(idleEarnings, maxIdleEarnings);
        }
    }
}
