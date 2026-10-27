using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleZoneInsider : MonoBehaviour, IZoneInsider
{
    private List<CubikZone> zones = new List<CubikZone>();
    public CubikZone CurrentZone => zones.Count == 0 ? null : zones[0];

    public bool IsCanPutInZone(CubikZoneConitionContext context) => true;
    public bool IsCanDropInZone(CubikZoneConitionContext context) => true;

    public void OnOutAvilibaleZone(CubikZoneConitionContext context)
    {
        zones.Remove(context.CubikZone);
    }

    public void OnOutNotAvilibaleZone(CubikZoneConitionContext context)
    {
    }

    public void OnPutInZone(CubikZoneConitionContext context)
    {
        zones.Add(context.CubikZone);
    }

    public void OnPutInZoneNotAvilibale(CubikZoneConitionContext context)
    {
    }

    public void ClearAllZone(bool isExitOutZone = true)
    {
        if (isExitOutZone)
        {
            foreach (var zone in zones.ToArray())
            {
                zone.OnExit(this);
            }
        }
        zones.Clear();
    }
}