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
            PlayerSave.ManualSave();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerSave.ManualSave();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerSave.ManualSave();
        }
    }

    [Button]
    private void ResetSave()
    {
        PlayerSave.ResetSaveData();
    }
}
