using System.Collections;
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
            Debug.Log("Auto-save completed at: " + System.DateTime.Now);
            yield return new WaitForSeconds(saveIntervalInSeconds);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerSave.ManualSave();
        Debug.Log("Save on quit completed");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerSave.ManualSave();
            Debug.Log("Save on pause completed");
        }
    }
}
