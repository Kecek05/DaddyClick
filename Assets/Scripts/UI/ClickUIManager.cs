using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ClickUIManager : MonoBehaviour
{
    [SerializeField] [Required] private Button _clickButton;
    private void Awake()
    {
        _clickButton.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        ClickManager.ManualClick(1 + ClickManager.CPS);
    }
}
