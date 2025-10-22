using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class LoadPlayerSave : MonoBehaviour
{
    [SerializeField] private float saveIntervalInSeconds = 60f; // Save every 60 seconds (1 minute)

    private void Start()
    {
        PlayerSave.LoadPlayerSave();
        StartCoroutine(AutoSaveCoroutine());
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            PlayerSave.ManualSave();
            yield return new WaitForSeconds(saveIntervalInSeconds);
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
