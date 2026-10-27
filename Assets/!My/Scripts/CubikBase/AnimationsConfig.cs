using UnityEngine;

[CreateAssetMenu(fileName = "AnimationsConfig", menuName = "KidsGame/AnimationsConfig")]
public class AnimationsConfig : ScriptableObject, IAnimationsConfig
{
    [Header("Jigle")]
    [SerializeField] private float jigleShakeDuration = 0.5f;
    [SerializeField] private float jigleShakeStrength = 0.2f;

    [Header("Poof")]
    [SerializeField] private float poofScaleMultiplier = 1.5f;
    [SerializeField] private float poofScaleDuration = 0.25f;

    [Header("Hole Delete")]
    [SerializeField] private float holeDeleteUpDistance = 1.1f;
    [SerializeField] private float holeDeleteUpDuration = 0.2f;
    [SerializeField] private float holeDeleteInterval = 0.15f;
    [SerializeField] private float holeDeleteDownDistance = 4f;
    [SerializeField] private float holeDeleteDownDuration = 0.35f;
    [SerializeField] private float holeDeleteRotationSpeed = 500f;

    [Header("Fly And Drop")]
    [SerializeField] private float flyUpDistance = 3f;
    [SerializeField] private float flyUpDuration = 0.3f;
    [SerializeField] private float flyDownDuration = 0.25f;

    public float JigleShakeDuration => jigleShakeDuration;
    public float JigleShakeStrength => jigleShakeStrength;

    public float PoofScaleMultiplier => poofScaleMultiplier;
    public float PoofScaleDuration => poofScaleDuration;

    public float HoleDeleteUpDistance => holeDeleteUpDistance;
    public float HoleDeleteUpDuration => holeDeleteUpDuration;
    public float HoleDeleteInterval => holeDeleteInterval;
    public float HoleDeleteDownDistance => holeDeleteDownDistance;
    public float HoleDeleteDownDuration => holeDeleteDownDuration;
    public float HoleDeleteRotationSpeed => holeDeleteRotationSpeed;

    public float FlyUpDistance => flyUpDistance;
    public float FlyUpDuration => flyUpDuration;
    public float FlyDownDuration => flyDownDuration;
}

public interface IAnimationsConfig
{
    float JigleShakeDuration { get; }
    float JigleShakeStrength { get; }

    float PoofScaleMultiplier { get; }
    float PoofScaleDuration { get; }

    float HoleDeleteUpDistance { get; }
    float HoleDeleteUpDuration { get; }
    float HoleDeleteInterval { get; }
    float HoleDeleteDownDistance { get; }
    float HoleDeleteDownDuration { get; }
    float HoleDeleteRotationSpeed { get; }

    float FlyUpDistance { get; }
    float FlyUpDuration { get; }
    float FlyDownDuration { get; }
}