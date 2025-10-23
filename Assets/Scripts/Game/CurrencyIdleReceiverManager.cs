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
            idleEarnings = Mathf.Min(idleEarnings, maxIdleEarnings);
            ClickManager.AddClicks(idleEarnings);
            OnCurrencyIdleReceived?.Invoke(idleEarnings, maxIdleEarnings);
        }
    }
}
