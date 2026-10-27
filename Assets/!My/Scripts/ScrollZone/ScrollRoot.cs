using System;
using UnityEngine;

public class ScrollRoot : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private float scrollSpeed = 1f;

    [Space]
    [SerializeField] private Vector2 scrollZone;
    public bool IsInteractible { get; private set; } = true;
    public bool IsScrolling => isScrolling;

    public Vector2 minPos;
    public Vector2 maxPos;

    private bool isScrolling;
    private Vector2 lastPointerPosition;

    public void TryStartScroll(Vector2 pos)
    {
        if (!IsInteractible || isScrolling)
            return;

        Vector2 localPos = transform.InverseTransformPoint(pos);

        if (Mathf.Abs(localPos.x) > scrollZone.x * 0.5f ||
            Mathf.Abs(localPos.y) > scrollZone.y * 0.5f)
            return;

        StartScroll(pos);
    }

    private void StartScroll(Vector2 pos)
    {
        isScrolling = true;
        lastPointerPosition = pos;
    }

    public void EndScroll()
    {
        isScrolling = false;
    }

    public void SetInteractible(bool isInteractible)
    {
        IsInteractible = isInteractible;

        if (!IsInteractible)
            EndScroll();
    }

    public void UpdateScrollPosition(Vector2 pos)
    {
        if (!IsInteractible || !isScrolling)
            return;

        Vector2 delta = pos - lastPointerPosition;
        lastPointerPosition = pos;

        Scroll(delta);
    }

    public void Scroll(Vector2 delta)
    {
        if (root == null)
            return;

        Vector3 position = root.localPosition;

        position.x = Mathf.Clamp(
            position.x + delta.x * scrollSpeed,
            minPos.x,
            maxPos.x
        );

        position.y = Mathf.Clamp(
            position.y + delta.y * scrollSpeed,
            minPos.y,
            maxPos.y
        );

        root.localPosition = position;
    }

    public void UpdateZoneSize(Vector2 minPos, Vector2 maxPos)
    {
        this.minPos = minPos;
        this.maxPos = maxPos;

        Scroll(Vector2.zero);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, scrollZone);
    }
}