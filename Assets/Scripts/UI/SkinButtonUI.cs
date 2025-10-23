using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private Button _button;
    [SerializeField] [Required] private Image _image;
    [SerializeField] [Required] private TextMeshProUGUI _nameText;
    [SerializeField] [Required] private GameObject _starPrefab;
    [SerializeField] [Required] private Transform _starParent;

    private DaddyDataSO _daddyData;
    private SkinsUI _skinsUI;

    public void Setup(DaddyDataSO daddyData, SkinsUI skinsUI, bool unlocked)
    {
        _daddyData = daddyData;
        _skinsUI = skinsUI;
        _nameText.text = daddyData.Name;
        _image.sprite = _daddyData.Icon;
        HandleLocked(unlocked);
        _button.onClick.AddListener(() =>
        {
            _skinsUI.SelectSkin(_daddyData.Icon, _daddyData.DaddyType);
        });
        
        for (int i = 0; i < _daddyData.Stars; i++)
        {
            Instantiate(_starPrefab, _starParent);
        }
    }

    public void HandleLocked(bool unlocked)
    {
        _image.color = unlocked ? Color.white : Color.black;
        _button.interactable = unlocked;
    }
}
