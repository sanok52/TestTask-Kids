using UnityEngine;

public class ScrollRootSizeChanger : MonoBehaviour
{
    [SerializeField] private DownLine downLine;
    [SerializeField] private ScrollRoot scrollRoot;
    [SerializeField] private Vector3 positionZero;
    [SerializeField] private Vector2 thresholdMinSize;

    private void Awake()
    {
        if(positionZero == Vector3.zero)
            positionZero = downLine.Root.localPosition;

        downLine.onAddInList += OnAddInList;
        downLine.onRemoveOutList += OnRemoveOutList;
    }

    private void OnAddInList(ISortable sortable)
    {
        UpdateSize();
    }

    private void OnRemoveOutList(ISortable sortable)
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        if (!Application.isPlaying)
            return;

        int count = downLine.Sortables.Count;
        Vector2 sizeNew = new Vector2(count * -downLine.Pedding.x, count * -downLine.Pedding.y);
        float deltaCompareW = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x;
        float deltaCompareH = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).x;

        float maxX = Mathf.Max(sizeNew.x, positionZero.x);
        float maxY = Mathf.Max(sizeNew.y, positionZero.y);
        float minX = Mathf.Min(sizeNew.x, positionZero.x) + (deltaCompareW / 2f * Mathf.Sign(maxX));
        float minY = Mathf.Min(sizeNew.y, positionZero.y) + (deltaCompareH / 2f * Mathf.Sign(maxY));

        float deltaX = Mathf.Abs(minX - maxX);
        float deltaY = Mathf.Abs(minY - maxY);

        //Debug.Log($"deltaCompareW: {deltaCompareW}; sizeNew.x => {Mathf.Abs(sizeNew.x)}");

        if (Mathf.Abs(sizeNew.x) < Mathf.Abs(deltaCompareW))
        {
            minX = positionZero.x;
            maxX = positionZero.x;
        }

        if (Mathf.Abs(sizeNew.y) < Mathf.Abs(deltaCompareH))
        {
            minY = positionZero.y;
            maxY = positionZero.y;
        }

        scrollRoot.UpdateZoneSize(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }
}