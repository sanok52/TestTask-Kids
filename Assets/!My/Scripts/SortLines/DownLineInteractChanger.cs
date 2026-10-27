using System;
using UniRx;
using UnityEngine;

public class DownLineInteractChanger : MonoBehaviour
{
    public bool IsActive = true;
    private DownLine downLine;

    public void Awake()
    {
        downLine = GetComponent<DownLine>();

        downLine.onAddInList += OnAddInList;
        downLine.onRemoveOutList += OnRemoveOutList;
    }

    private void OnAddInList(ISortable sortable)
    {
        UpdateInteractible(sortable);
    }

    private void UpdateInteractible(ISortable sortable)
    {
        if (!sortable.gameObject.TryGetComponent(out IDragble dragble))
            return;

        if (!IsActive)
        {
            dragble.IsInteractible = true;
            return;
        }

        dragble.IsInteractible = downLine.Sortables.IndexOf(sortable) == 0;
    }

    private void OnRemoveOutList(ISortable sortable)
    {
        foreach (var item in downLine.Sortables)
        {
            UpdateInteractible(item);
        }
    }
}