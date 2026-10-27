using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

public class CubikMove : MonoBehaviour, ISortable
{
    [SerializeField] private CubikAnimations cubikAnimations;

    [Space]
    [SerializeField] private float durationMoveTo = 0.5f; //Задать больше таких данных. Возможно, стоит брать их из конфига

    private SimpleDragble simpleDragble;
    private DownLine currentDownLine;
    private SimpleZoneInsider zoneInsider;

    private Tweener tweenerMove;
    private IObjectResolver objectResolver;
    private Factory factory;

    public CubikInfo CubikInfo { get; private set; }

    public bool IsMove => (tweenerMove != null && tweenerMove.IsPlaying()) || cubikAnimations.IsPlaying;
    public bool IsDrag => simpleDragble.IsDrag;
    public CubikAnimations CubikAnimations => cubikAnimations;

    [Inject]
    public void Contructor(IObjectResolver objectResolver, Factory factory)
    {
        this.objectResolver = objectResolver;
        this.factory = factory;
        objectResolver.Inject(cubikAnimations);
    }

    public void SetInfo(CubikInfo cubikInfo)
    {
        CubikInfo = cubikInfo;
        cubikAnimations.ApplyInfo(cubikInfo);
    }

    private void Awake()
    {
        simpleDragble = GetComponent<SimpleDragble>();
        simpleDragble.onDragEnter += OnDragEnter;
        simpleDragble.onDragExit += OnDragExit;

        zoneInsider = GetComponent<SimpleZoneInsider>();
    }

    private void OnDragEnter(Vector3 vector)
    {
        if (IsMove)
            transform.DOKill(false);

        //Костыль - надо переделать
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnDragExit(Vector3 vector)
    {
        if (zoneInsider.CurrentZone != null)
        {
            var zone = zoneInsider.CurrentZone as CubikDropZone;
            if (zone)
            {
                bool isDrop = zone.CanDrop(zoneInsider);
                if (isDrop)
                {
                    if (currentDownLine != null)
                        currentDownLine.RemoveOutList(this);

                    cubikAnimations.PlayAnimation(zone.GetCubikAnimationType(), () =>
                    {
                        zone.Drop(zoneInsider);
                    });

                    zoneInsider.ClearAllZone();
                    return;
                }                
            }
        }

        if (currentDownLine != null && currentDownLine.IsCanReturn)
        {
            currentDownLine.UpdateAllPosition();
            return;
        }

        DeliteInEmptySpace();
    }

    public void MoveTo(Vector3 position)
    {
        if (IsDrag)
            return;

        if(IsMove)
            transform.DOKill(false);

        tweenerMove = transform.DOMove(position, durationMoveTo).SetAutoKill(false);
    }

    public void MoveToSort(Vector3 position)
    {
        if (IsDrag)
            return;

        MoveTo(position);
    }

    public void OnAddInLine(DownLine downLine)
    {
        currentDownLine = downLine;
    }

    public void OnRemoveOutLine(DownLine downLine)
    {
        if (currentDownLine == downLine)
            currentDownLine = null;
    }

    public void DeliteInHole()
    {
        cubikAnimations.PlayAnimation(CubikAnimations.CubikAnimationType.HoleDelite, () =>
        {
            MessageBroker.Default.Publish(new GameEventMessage(GameEventType.CubikHoleDie, this));

            DeliteImmidiatly();
        });
    }

    private void DeliteInEmptySpace()
    { 
        if (currentDownLine != null)
            currentDownLine.RemoveOutList(this);

        if (simpleDragble.DragDropModel != null && simpleDragble.DragDropModel.CurrentDragble == simpleDragble as IDragble)
            simpleDragble.DragDropModel.EndDragImmidiatly(false);

        cubikAnimations.PlayAnimation(CubikAnimations.CubikAnimationType.Poof, () =>
        {
            factory.CreatePoofVFX(transform.position);

            MessageBroker.Default.Publish(new GameEventMessage(GameEventType.CubikEmptyDie, this));

            DeliteImmidiatly();
        });

        simpleDragble.IsInteractible = false;
    }

    private void DeliteImmidiatly()
    {
        MessageBroker.Default.Publish(
            new GameEventMessage(GameEventType.CubikAnyDie, this)
        );
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (simpleDragble.DragDropModel != null && simpleDragble.DragDropModel.CurrentDragble == simpleDragble as IDragble)
            simpleDragble.DragDropModel.EndDragImmidiatly();

        if (currentDownLine != null)
            currentDownLine.RemoveOutList(this);
    }
}