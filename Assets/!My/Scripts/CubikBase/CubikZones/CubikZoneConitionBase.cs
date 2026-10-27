using System;
using System.Collections.Generic;
using UnityEngine;

public class CubikZoneConitionBase : ScriptableObject, ICubikZoneConition
{
    public virtual bool IsCanCubickInstide(CubikZoneConitionContext context) => true;
}

public class CubikZoneDropConitionBase : CubikZoneConitionBase, ICubickDropZoneCondition
{
    public virtual bool IsCanCubickDrop(CubikZoneConitionContext context) => true;    
}