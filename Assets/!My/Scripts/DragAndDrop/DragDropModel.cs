using System;
using UnityEngine;
using VContainer;

public class DragDropModel
{
    private IDragble currentDragble;
    private Vector3 worldPosition;

    private DragDropSettings settings;

    public bool IsDrag => currentDragble != null;
    public IDragble CurrentDragble => currentDragble;

    [Inject]
    public DragDropModel(DragDropSettings settings)
    {
        this.settings = settings;
    }

    public void OnButtonDown()
    {
        if (currentDragble != null)
            return;

        currentDragble = TryGrap(worldPosition);
        if (currentDragble != null)
            currentDragble.OnDragEnter(worldPosition, this);
    }

    private IDragble TryGrap(Vector3 worldPosition)
    {
        var results = Physics2D.OverlapCircleAll(worldPosition, settings.RadiusGrap, settings.LayerMask);
        foreach (var item in results)
        {
            if(item.TryGetComponent(out IDragble dragble) && dragble.IsInteractible)
            {
                return dragble;
            }
        }

        return null;
    }

    public void OnTick (Vector2 worldPosition)
    {
        this.worldPosition = worldPosition;

        if (currentDragble != null)
        {
            if (currentDragble.gameObject == null || !currentDragble.IsInteractible)
            {
                currentDragble = null;
                return;
            }
            
            currentDragble.OnDragStay(this.worldPosition, this);
        }
    }

    public void OnButtonUp()
    {
        if (currentDragble == null)
            return;

        EndDragImmidiatly();
    }

    public void EndDragImmidiatly(bool isInvokeEvent = true)
    {
        if (isInvokeEvent)
            currentDragble.OnDragExit(worldPosition, this);
        currentDragble = null;
    }
}