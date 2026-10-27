using DG.Tweening.Core;
using System.Linq;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "ConitionTower", menuName = "KidsGame/ZoneConditions/Tower")]
public class CubikZoneConitionTower : CubikZoneDropConitionBase
{
    [SerializeField] private float yMinOffset = 0.5f;
    [SerializeField] private float xMaxOffset = 0.5f;

    [Space]
    [SerializeField] private bool isTestX = true;
    [SerializeField] private bool isTestY = true;

    [Space]
    [SerializeField] private bool isTestMaxHeightDevice = true;

    public override bool IsCanCubickDrop(CubikZoneConitionContext context)
    {
        var dropTowerZone = context.CubikZone.gameObject.GetComponent<DropTowerZone>();
        if (dropTowerZone == null)
            return false;

        var dropLine = dropTowerZone.DownLine;
        if (dropLine == null)
            return false;

        var list = dropLine.Sortables.Select(_ => _.gameObject.GetComponent<CubikMove>()).ToArray();
        if (list.Length == 0)
            return true;

        var last = list[list.Length - 1];
        var current = context.CurrentCubik.gameObject.GetComponent<CubikMove>();

        if (isTestY)
        {
            if (last.transform.position.y + yMinOffset > current.transform.position.y)
                return false;

            if (isTestMaxHeightDevice)
            {
                Vector3 nextPos = dropLine.GetPosition(dropLine.Sortables.Count);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(nextPos);
                //Vector3 screenPos = Camera.main.WorldToScreenPoint(last.transform.position);
                if (screenPos.y + yMinOffset >= Screen.height)
                {
                    MessageBroker.Default.Publish(new GameEventMessage(GameEventType.ToHight, this));
                    return false;
                }
            }
        }        

        if (isTestX && Mathf.Abs(last.transform.position.x - current.transform.position.x) > xMaxOffset)        
            return false;        

        return AdditionCondition(context, current, list);
    }

    //Для добавления дополнительных условий в будущем через наследников
    protected virtual bool AdditionCondition(CubikZoneConitionContext context, CubikMove current, CubikMove[] list)
    {
        return true;
    }
}