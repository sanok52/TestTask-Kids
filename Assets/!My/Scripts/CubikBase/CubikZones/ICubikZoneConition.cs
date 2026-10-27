public interface ICubikZoneConition
{
    bool IsCanCubickInstide(CubikZoneConitionContext context);
}

public interface ICubickDropZoneCondition : ICubikZoneConition
{
    bool IsCanCubickDrop(CubikZoneConitionContext context);
}
