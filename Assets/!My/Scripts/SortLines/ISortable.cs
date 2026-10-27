using UnityEngine;

public interface ISortable
{
    void MoveToSort(Vector3 position);
    void OnAddInLine(DownLine downLine);
    void OnRemoveOutLine(DownLine downLine);

    public GameObject gameObject { get; }
}