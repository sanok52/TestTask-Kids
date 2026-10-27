using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CubikAnimations : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private SpriteRenderer spriteRenderer;    

    private Coroutine coroutine;
    private Sequence sequence;
    private IAnimationsConfig config;

    public bool IsPlaying => coroutine != null || (sequence != null && sequence.IsActive());

    public enum CubikAnimationType
    {
        None,
        Jigle,
        Poof,
        HoleDelite,
        FlyAndDrop
    }

    [Inject]
    public void Construct (IObjectResolver objectResolver, IAnimationsConfig config)
    {
        this.config = config;
    }

    private void Start()
    {
        if (root == null)
            root = transform.parent ? transform.parent : transform;
    }

    public void PlayAnimation(CubikAnimationType animationType, Action onComplite = null)
    {
        if (!AnimationPrepare(animationType))
            onComplite?.Invoke();

        var routine = GetAnimationRoutine(animationType);
        if(routine == null)
        {
            Debug.LogWarning("routine == null");
            return;
        }

        coroutine = StartCoroutine(PlayRoutine(routine, onComplite));
    }

    public void PlayAnimationMove (CubikAnimationType animationType, Vector3 target, Action onComplite = null)
    {
        if (!AnimationPrepare(animationType))
            onComplite?.Invoke();

        var routine = GetAnimationMoveRoutine(animationType, target);
        if (routine == null)
        {
            Debug.LogWarning("routine == null");
            return;
        }

        coroutine = StartCoroutine(PlayRoutine(routine, onComplite));
    }

    private bool AnimationPrepare(CubikAnimationType animationType)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if (sequence != null)
        {
            sequence.Kill();
            sequence = null;
        }

        root.DOKill(false);

        if (animationType == CubikAnimationType.None)
            return false;
        
        return true;
    }

    private IEnumerator GetAnimationRoutine(CubikAnimationType animationType)
    {
        switch (animationType)
        {
            case CubikAnimationType.Jigle:
                return JigleAnimationsRoutine();
            case CubikAnimationType.Poof:
                return PoofAnimationsRoutine();
            case CubikAnimationType.HoleDelite:
                return HoleDeliteAnimationsRoutine();
            case CubikAnimationType.FlyAndDrop:
                return null;
            default:
                return null;
        }
    }

    private IEnumerator GetAnimationMoveRoutine(CubikAnimationType animationType, Vector3 targetPosition)
    {
        switch (animationType)
        {            
            case CubikAnimationType.FlyAndDrop:
                return FlyAndDropAnimationsRoutine(targetPosition);
            default:
                return null;
        }
    }

    private IEnumerator PlayRoutine(IEnumerator animationRoutine, Action onComplite)
    {
        yield return StartCoroutine(animationRoutine);

        if(transform.parent != null)
            transform.localPosition = Vector3.zero;

        onComplite?.Invoke();
    }

    private IEnumerator JigleAnimationsRoutine()
    {
        yield return transform
            .DOShakePosition(config.JigleShakeDuration, config.JigleShakeStrength)
            .WaitForCompletion();
    }

    private IEnumerator PoofAnimationsRoutine()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(root.DOScale(
            transform.localScale.x * config.PoofScaleMultiplier,
            config.PoofScaleDuration));

        yield return sequence.WaitForCompletion();
    }

    private IEnumerator HoleDeliteAnimationsRoutine()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(root.DOMove(
            root.position + Vector3.up * config.HoleDeleteUpDistance,
            config.HoleDeleteUpDuration));

        sequence.AppendInterval(config.HoleDeleteInterval);

        sequence.Append(root.DOMove(
            root.position + Vector3.down * config.HoleDeleteDownDistance,
            config.HoleDeleteDownDuration));

        sequence.OnUpdate(() =>
            transform.Rotate(Vector3.forward, config.HoleDeleteRotationSpeed * Time.deltaTime));

        yield return sequence.WaitForCompletion();
    }

    private IEnumerator FlyAndDropAnimationsRoutine(Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(root.DOMove(
            targetPosition + Vector3.up * config.FlyUpDistance,
            config.FlyUpDuration));

        sequence.Append(root.DOMove(
            targetPosition,
            config.FlyDownDuration));

        yield return sequence.WaitForCompletion();
    }

    public void ApplyInfo(CubikInfo cubikInfo)
    {
        spriteRenderer.sprite = cubikInfo.Sprite;
    }
}