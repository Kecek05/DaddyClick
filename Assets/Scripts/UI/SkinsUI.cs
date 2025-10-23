using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SkinsUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private GameObject _uiPanel;
    [SerializeField] [Required] private Transform _uiContentParent;
    [SerializeField] [Required] private Button _closeButton;
    [SerializeField] [Required] private Button _openButton;
    [SerializeField] [Required] private GameObject _skinButtonPrefab;
    [SerializeField] [Required] private DaddyDataListSO _daddyDataList;
    [SerializeField] [Required] private Image _clickImage;
    
    private Dictionary<DaddyType, SkinButtonUI> _skinButtons = new Dictionary<DaddyType, SkinButtonUI>();
    
    private void Awake()
    {
        PlayerSave.OnSaveLoaded += PlayerSaveOnOnSaveLoaded;
        DaddyManager.OnUnlockDaddy += DaddyManagerOnOnUnlockDaddy;
        _closeButton.onClick.AddListener(CloseUI);
        _openButton.onClick.AddListener(OpenUI);
        CloseUI();
    }

    private void OnDestroy()
    {
        PlayerSave.OnSaveLoaded -= PlayerSaveOnOnSaveLoaded;
        DaddyManager.OnUnlockDaddy -= DaddyManagerOnOnUnlockDaddy;
    }
    
    private void DaddyManagerOnOnUnlockDaddy()
    {
        foreach (var skinButtonData in _skinButtons)
        {
            if (DaddyManager.BoughtDaddies.TryGetValue(skinButtonData.Key, out bool unlocked))
            {
                skinButtonData.Value.HandleLocked(unlocked);
            }
        }
    }

    private void PlayerSaveOnOnSaveLoaded()
    {
        _clickImage.sprite = _daddyDataList.GetDaddyDataSOByType(PlayerSave.SelectedSkinType).Icon;
        SetupButtons();
    }

    private void SetupButtons()
    {
        var sortedDaddies = DaddyManager.BoughtDaddies
            .OrderBy(d => _daddyDataList.GetDaddyDataSOByType(d.Key).Stars)
            .ThenBy(d => d.Key);
        
        foreach (var daddyData in sortedDaddies)
        {
            SkinButtonUI daddyButton = Instantiate(_skinButtonPrefab, _uiContentParent).GetComponent<SkinButtonUI>();
            daddyButton.Setup(_daddyDataList.GetDaddyDataSOByType(daddyData.Key), this, daddyData.Value);
            _skinButtons.Add(daddyData.Key, daddyButton);
        }
    }

    public void SelectSkin(Sprite skin, DaddyType daddyType)
    {
        _clickImage.sprite = skin;
        PlayerSave.SetSelectedSkin(daddyType);
        CloseUI();
    }

    private void OpenUI()
    {
        _uiPanel.SetActive(true);
    }
    
    private void CloseUI()
    {
        _uiPanel.SetActive(false);
    }
}
