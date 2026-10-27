using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropHoleZone : MonoBehaviour
{
    private void Start()
    {
        CubikDropZone zone = GetComponent<CubikDropZone>();
        zone.onDropIn += OnDropIn;
    }

    private void OnDropIn(IZoneInsider insider)
    {
        var cubik = insider.gameObject.GetComponent<CubikMove>();
        if (cubik == null)
            return;

        cubik.DeliteInHole();
    }
}
