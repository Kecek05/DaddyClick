using DG.Tweening;
using KeceK.Game;
using TMPro;
using UnityEngine;

public class ClickPopup : PoolableMonoBehaviour<ClickPopup>
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _tweenDuration = 1f;
    [SerializeField] private Ease _easeType = Ease.OutCubic;
    
    private Tween _tween;
    
    public void Setup(float clickValue)
    {
        _canvasGroup.alpha = 1f;
        _textMeshProUGUI.text = $"<CPSswing>{MathK.FormatNumberWithSuffix(clickValue)}</CPSswing>";
        transform.localScale = Vector3.one * 0.5f;
        Animate();
    }

    private void Animate()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 30f) * MathK.GetRandomSign());
        transform.DOScale(Vector3.one * 1.5f, _tweenDuration).SetEase(_easeType);
        DOTween.To(() => transform.localPosition, x => transform.localPosition = x,
                transform.localPosition + new Vector3(0f, 100f, 0f), _tweenDuration)
            .SetEase(_easeType);
        
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, _tweenDuration)
            .SetEase(_easeType).OnComplete(() =>
            {
                ReturnToPool();
            });
    }
    

}
