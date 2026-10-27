using UnityEngine;

public class TowerLineAuto : TowerLine
{
    [Space]
    [SerializeField] private bool isRandomazeX = false;

    protected override Vector3 CreateSortablePosition(ISortable sortable)
    {
        if(Sortables.Count == 0)        
            return base.CreateSortablePosition(sortable);        
        else        
            return Sortables[Sortables.Count - 1].gameObject.transform.position + 
                 new Vector3(isRandomazeX ? Random.Range(-pedding.x, pedding.x) : 0f, pedding.y, 0f);        
    }
}
