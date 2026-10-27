using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLine : DownLine
{
    protected Dictionary<ISortable, Vector3> _memoryPosition = new Dictionary<ISortable, Vector3>();

    public override void AddInList(ISortable sortable)
    {
        _memoryPosition.Add(sortable, CreateSortablePosition(sortable));
        base.AddInList(sortable);
    }

    protected virtual Vector3 CreateSortablePosition (ISortable sortable)
    {
        return sortable.gameObject.transform.position;
    }

    public override Vector3 GetPosition(int index)
    {
        Vector3 peddingVertical = new Vector3(0f, pedding.y, 0f);
        if (sortables.Count <= index)        
            return root.position + peddingVertical * index;

        Vector3 currentPos = _memoryPosition[sortables[index]];
        Vector3 zero = new Vector3(currentPos.x, root.position.y, root.position.z);
        Vector3 position = zero + peddingVertical * index;       
        _memoryPosition[sortables[index]] = position;

        return position;
    }

    public override void RemoveOutList(ISortable sortable)
    {
        base.RemoveOutList(sortable);
        _memoryPosition.Remove(sortable);
    }
}