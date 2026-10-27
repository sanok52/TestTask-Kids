using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownLine : MonoBehaviour
{
    [SerializeField] protected Transform root;
    [SerializeField] protected Vector3 pedding = new Vector3(0.5f, 0f, 0f);
    [SerializeField] private CubikAnimations.CubikAnimationType putMoveAnimation = CubikAnimations.CubikAnimationType.FlyAndDrop;
    protected List<ISortable> sortables = new List<ISortable>();

    public bool IsCanReturn = false;

    public List<ISortable> Sortables => sortables;
    public Vector3 Pedding => pedding;
    public Transform Root => root;
    public CubikAnimations.CubikAnimationType PutMoveAnimation => putMoveAnimation;

    public event Action<ISortable> onAddInList;
    public event Action<ISortable> onRemoveOutList;

    public virtual void AddInList(ISortable sortable)
    {
        if (sortable == null || sortables.Contains(sortable))
            return;

        sortables.Add(sortable);
        sortable.OnAddInLine(this);
        sortable.gameObject.transform.parent = root;

        UpdateAllPosition();

        onAddInList?.Invoke(sortable);
    }

    public virtual void UpdateAllPosition()
    {
        if (root == null)
            return;

        for (int i = 0; i < sortables.Count; i++)
        {
            UpdatePosition(sortables[i]);
        }
    }

    public virtual void UpdatePosition(ISortable sortable)
    {
        if (sortable == null)
            return;

        int index = Sortables.IndexOf(sortable);
        if (index < 0)
            return;

        Vector3 position = GetPosition(index);
        sortables[index].MoveToSort(position);
    }

    public virtual void RemoveOutList(ISortable sortable)
    {
        if (sortable == null)
            return;

        if (!sortables.Contains(sortable))
            return;

        sortable.OnRemoveOutLine(this);
        sortables.Remove(sortable);

        UpdateAllPosition();

        onRemoveOutList?.Invoke(sortable);
    }

    public virtual Vector3 GetPosition(int index)
    {
       return root.position + pedding * index;
    }
}

public interface ISortable
{
    void MoveToSort(Vector3 position);
    void OnAddInLine(DownLine downLine);
    void OnRemoveOutLine(DownLine downLine);

    public GameObject gameObject { get; }
}