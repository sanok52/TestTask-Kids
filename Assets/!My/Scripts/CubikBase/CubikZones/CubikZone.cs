using System;
using System.Collections.Generic;
using UnityEngine;
public class CubikZone : MonoBehaviour
{
    public CubikZoneConitionBase conition;

    private List<IZoneInsider> insiders = new();
    public List<IZoneInsider> Insiders => insiders;

    public Action<IZoneInsider> OnAvailableEnter;
    public Action<IZoneInsider> OnUnavailableEnter;

    public Action<IZoneInsider> OnAvailableExit;
    public Action<IZoneInsider> OnUnavailableExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out IZoneInsider insider))
            return;

        OnEnter(insider);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out IZoneInsider insider))
            return;

        OnExit(insider);
    }

    public void OnEnter(IZoneInsider insider)
    {
        if (insiders.Contains(insider))
            return;

        var context = CreateContext(insider);

        if ((conition == null || conition.IsCanCubickInstide(context)) &&
            insider.IsCanPutInZone(context))
        {
            insiders.Add(insider);
            insider.OnPutInZone(context);
            OnAvailableEnter?.Invoke(insider);
        }
        else
        {
            insider.OnPutInZoneNotAvilibale(context);
            OnUnavailableEnter?.Invoke(insider);
        }
    }

    public void OnExit(IZoneInsider insider)
    {
        var context = CreateContext(insider);

        if (insiders.Remove(insider))
        {
            insider.OnOutAvilibaleZone(context);
            OnAvailableExit?.Invoke(insider);
        }
        else
        {
            insider.OnOutNotAvilibaleZone(context);
            OnUnavailableExit?.Invoke(insider);
        }
    }

    protected virtual CubikZoneConitionContext CreateContext(IZoneInsider insider)
    {
        return new CubikZoneConitionContext
        {
            CurrentCubik = insider,
            CubikZone = this
        };
    }
}