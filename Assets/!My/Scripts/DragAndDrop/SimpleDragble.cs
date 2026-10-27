using System;
using UnityEngine;

public class SimpleDragble : MonoBehaviour, IDragble
{
    private Vector2 offset;

    public bool IsInteractible { get; set; } = true;
    public bool IsDrag { get; private set; }
    public DragDropModel DragDropModel { get; private set; }

    public event Action<Vector3> onDragEnter;
    public event Action<Vector3> onDragStay;
    public event Action<Vector3> onDragExit;

    public void OnDragEnter(Vector3 point, DragDropModel dragDropModel)
    {
        IsDrag = true;
        this.DragDropModel = dragDropModel;

        offset = transform.position - point;
        onDragEnter?.Invoke(point);
    }

    public void OnDragStay(Vector3 point, DragDropModel dragDropModel)
    {
        if (gameObject == null)
            return;

        transform.position = new Vector3(point.x, point.y, transform.position.z) + (Vector3)offset;
        onDragStay?.Invoke(point);
    }

    public void OnDragExit(Vector3 point, DragDropModel dragDropModel)
    {
        IsDrag = false;
        this.DragDropModel = dragDropModel;

        onDragExit?.Invoke(point);
    }
}