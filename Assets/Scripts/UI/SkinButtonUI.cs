using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] [Required] private Button _button;
    [SerializeField] [Required] private Image _image;


    private DaddyDataSO _daddyData;
    private SkinsUI _skinsUI;

    public void Setup(DaddyDataSO daddyData, SkinsUI skinsUI, bool unlocked)
    {
        _daddyData = daddyData;
        _skinsUI = skinsUI;
        _image.sprite = _daddyData.Icon;
        HandleLocked(unlocked);
        _button.onClick.AddListener(() =>
        {
            _skinsUI.SelectSkin(_daddyData.Icon, _daddyData.DaddyType);
        });
    }

    public void HandleLocked(bool unlocked)
    {
        _image.color = unlocked ? Color.white : Color.black;
        _button.interactable = unlocked;
    }
}
