using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(60f);

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
        StartCoroutine(AutoSaveCoroutine());
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            yield return _waitForSeconds;
            
            var saveTask = PlayerSave.ManualSave();
            yield return new WaitUntil(() => saveTask.IsCompleted);
            
            if (saveTask.Exception != null)
            {
                Debug.LogError($"Auto-save failed: {saveTask.Exception.GetBaseException().Message}");
            }
        }
    }

    private async void OnApplicationQuit()
    {
        await PlayerSave.ManualSave();
    }

    private async void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            await PlayerSave.ManualSave();
        }
    }

    [Button]
    private void ResetSave()
    {
        PlayerSave.ResetSaveData();
    }
}
