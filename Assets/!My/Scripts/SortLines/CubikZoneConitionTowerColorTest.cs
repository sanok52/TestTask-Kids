using UnityEngine;

[CreateAssetMenu(fileName = "ConitionTowerColor", menuName = "KidsGame/ZoneConditions/Tower (Color Test)")]
public class CubikZoneConitionTowerColorTest : CubikZoneConitionTower
{
    protected override bool AdditionCondition(CubikZoneConitionContext context, CubikMove current, CubikMove[] list)
    {
        var last = list[list.Length - 1];
        return current.CubikInfo.Color == last.CubikInfo.Color;
    }
}