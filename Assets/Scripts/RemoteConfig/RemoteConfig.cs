using System;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class RemoteConfig : MonoBehaviour
{
    public event Action OnConfigLoaded;
    
    [SerializeField] private DaddyDataListSO _daddyDataListSO;
    
    private string DEV_ENV_ID = "db022ebe-f3ca-4a9f-a806-8af9c72a0ba9";
    private string PROD_ENV_ID = "b76600ab-97c8-4a8f-93d7-0fc582d708ee";
    public struct userAttributes {}
    public struct appAttributes {}

    private void Start()
    {
        Debug.Log(AuthenticationService.Instance.IsSignedIn);
        Debug.Log(UnityServices.State);

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        #if UNITY_EDITOR
            Debug.Log("Using DEV environment for Remote Config");
            RemoteConfigService.Instance.SetEnvironmentID(DEV_ENV_ID);
        #else
            RemoteConfigService.Instance.SetEnvironmentID(PROD_ENV_ID);
        #endif
        
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        // Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
        
        foreach (var daddyData in _daddyDataListSO.DaddiesShop)
        {
            string daddyKey = daddyData.name.Replace("ShopSO", "") + "Cost";
            string daddyPriceString = RemoteConfigService.Instance.appConfig.GetString(daddyKey);
            
            if (double.TryParse(daddyPriceString, out double daddyPriceNumber))
            {
                daddyData.Cost = daddyPriceNumber;
            }
            else
            {
                Debug.LogWarning($"Failed to parse cost for {daddyKey}: {daddyPriceString}");
            }
        }
        PlayerSave.LoadPlayerSave();
    }
}

