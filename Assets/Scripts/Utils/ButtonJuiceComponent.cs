using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJuiceComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Title("Button Juice Settings")]
    [SerializeField] [Required] private Button _button;
    [SerializeField] private float _scaleAmount = 0.9f;
    [SerializeField] private float _scaleDuration = 0.1f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _button.transform.DOKill();
            _button.transform.DOScale(_scaleAmount, _scaleDuration).SetEase(_easeType);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _button.transform.DOKill();
            _button.transform.DOScale(1f, _scaleDuration).SetEase(_easeType);
        }
    }
}
