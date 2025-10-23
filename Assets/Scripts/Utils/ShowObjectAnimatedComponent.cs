using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShowObjectAnimatedComponent : MonoBehaviour
{
    [Title("Animation Settings")]
    [SerializeField] private float _yOffset = -50f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private Ease _easeType = Ease.OutBack;
    [SerializeField] private bool _animateOnStart = true;
    
    private Vector3 _originalPosition;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        if (_animateOnStart)
        {
            PlayAnimation();
        }
    }

    [Button("Play Animation")]
    public void PlayAnimation()
    {
        _rectTransform.DOKill();
        Vector2 startPos = new Vector2(_originalPosition.x, _originalPosition.y + _yOffset);
        _rectTransform.anchoredPosition = startPos;
        
        // Tween to original position
        _rectTransform.DOAnchorPos(_originalPosition, _duration).SetEase(_easeType);
    }

    private void OnDisable()
    {
        _rectTransform.DOKill();
        _rectTransform.anchoredPosition = _originalPosition;
    }
}
