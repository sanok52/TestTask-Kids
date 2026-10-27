using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorder : MonoBehaviour
{
    [SerializeField] private float offsetXMin = 1f;
    [SerializeField] private float offsetXMax = Mathf.Infinity;

    private void Awake()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {        
        Vector3 pointZero = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f));

        Vector2 pointMin = pointZero + new Vector3(offsetXMin, 0f);
        Vector2 pointMax = pointZero + new Vector3(offsetXMax, 0f);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, pointMin.x, pointMax.x),
             transform.position.y);
    }
}
