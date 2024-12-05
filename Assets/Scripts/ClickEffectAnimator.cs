using DG.Tweening;
using UnityEngine;

public class ClickEffectAnimator : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float animationDuration;

    [SerializeField]
    private float moveY;

    public void StartAnimation()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, animationDuration).OnComplete(SelfDestroy);
        transform.DOLocalMoveY(transform.localPosition.y + moveY, animationDuration);
    }

    private void SelfDestroy()
    {
        canvasGroup.DOKill();
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
