using Sirenix.OdinInspector;
using UnityEngine;

public class ClickPopupManager : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private GameObject _popupParent;
    [SerializeField] private ClickPopupPool _clickPopupPool;

    public void Awake()
    {
        ClickManager.OnManualClick += ClickManager_OnManualClick;
    }

    private void ClickManager_OnManualClick(float clickValue)
    {
        ClickPopup newClick = _clickPopupPool.Get();
        newClick.transform.SetParent(_popupParent.transform);
        newClick.transform.position = Input.mousePosition;
        newClick.Setup(clickValue);
    }
}
