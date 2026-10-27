using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DropTowerZone : MonoBehaviour
{
    [SerializeField] private DownLine downLine;
    public DownLine DownLine => downLine;

    private void Start()
    {
        CubikDropZone zone = GetComponent<CubikDropZone>();
        zone.onDropIn += OnDropIn;
    }

    private void OnDropIn(IZoneInsider insider)
    {
        if (insider.gameObject.TryGetComponent(out CubikMove cubikMove))        
            AddInTower(cubikMove);        
    }

    private void AddInTower(CubikMove cubikMove)
    {
        AddInTowerImmidiatly(cubikMove);
        cubikMove.CubikAnimations.PlayAnimationMove(downLine.PutMoveAnimation, downLine.GetPosition(downLine.Sortables.Count - 1), () =>
        {
            downLine.UpdatePosition(cubikMove);
            MessageBroker.Default.Publish(new GameEventMessage(GameEventType.CubikDrop, cubikMove));
        });
    }

    private void AddInTowerImmidiatly(CubikMove cubikMove)
    {
        downLine.AddInList(cubikMove);
    }
}