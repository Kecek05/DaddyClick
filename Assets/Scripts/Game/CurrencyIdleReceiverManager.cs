using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CurrencyIdleReceiverManager : MonoBehaviour
{
    /// <summary>
    /// float: amount of currency received from idle time
    /// float2: maxPossibleEarnings
    /// </summary>
    public static event Action<float, float> OnCurrencyIdleReceived;
    
    [SerializeField] [Required] private FigureDataListSO _figureDataListSO;
    [SerializeField] private float maxIdleEarnings = 10000f;
    
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
        float idleTime = (float)(DateTime.Now - startTime).TotalSeconds;
        float cps = ClickUtils.GetCPS(_figureDataListSO);
        float multiplier = ClickManager.CurrentMultiplier;
        float idleEarnings = cps * idleTime * multiplier;
        
        if (idleEarnings > 0)
        {
            idleEarnings = Mathf.Min(idleEarnings, maxIdleEarnings * cps * ClickManager.CurrentMultiplier);
            ClickManager.AddClicks(idleEarnings);
            OnCurrencyIdleReceived?.Invoke(idleEarnings, maxIdleEarnings * cps * ClickManager.CurrentMultiplier);
        }
    }
}
