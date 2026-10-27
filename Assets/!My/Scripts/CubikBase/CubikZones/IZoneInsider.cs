using UnityEngine;

public interface IZoneInsider
{
    bool IsCanPutInZone(CubikZoneConitionContext context);
    bool IsCanDropInZone(CubikZoneConitionContext context);
    void OnPutInZone(CubikZoneConitionContext context);
    void OnPutInZoneNotAvilibale(CubikZoneConitionContext context);
    void OnOutAvilibaleZone(CubikZoneConitionContext context);
    void OnOutNotAvilibaleZone(CubikZoneConitionContext context);

    GameObject gameObject { get; }
}