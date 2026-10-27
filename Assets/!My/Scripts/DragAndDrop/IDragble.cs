using UnityEngine;

public interface IDragble
{
    public void OnDragEnter(Vector3 point, DragDropModel dragDropModel);
    public void OnDragStay(Vector3 point, DragDropModel dragDropModel);
    public void OnDragExit(Vector3 point, DragDropModel dragDropModel);

    public bool IsInteractible { get; set; }
    public GameObject gameObject { get; }
}
