using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ClickUIManager : MonoBehaviour
{
    public event Action OnClick;
    
    [SerializeField] [Required] private Button _clickButton;

    private void Awake()
    {
        _clickButton.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        OnClick?.Invoke();
    }
}
