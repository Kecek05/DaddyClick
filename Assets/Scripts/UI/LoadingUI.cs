using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;

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
        _loadingPanel.SetActive(false);
    }
}
