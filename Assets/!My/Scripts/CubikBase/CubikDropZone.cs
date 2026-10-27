using System;
using UnityEngine;

public class CubikDropZone : CubikZone
{
    [SerializeField] private CubikAnimations.CubikAnimationType animationType = CubikAnimations.CubikAnimationType.None;

    public event Action<IZoneInsider> onDropIn;

    public bool TryDrop(IZoneInsider insider)
    {
        if (!CanDrop(insider))
            return false;

        Drop(insider);
        return true;
    }

    public void Drop(IZoneInsider insider)
    {
        onDropIn?.Invoke(insider);
    }

    public bool CanDrop(IZoneInsider insider)
    {
        var context = CreateContext(insider);
        var conditionDrop = conition != null ? (conition as CubikZoneDropConitionBase) : null;

        if ((conditionDrop == null || conditionDrop.IsCanCubickDrop(context)) &&
            insider.IsCanDropInZone(context))
        {
            return true;
        }

        return false;
    }

    public CubikAnimations.CubikAnimationType GetCubikAnimationType()
    {
        return animationType;
    }
}