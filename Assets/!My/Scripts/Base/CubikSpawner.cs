using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

public class CubikSpawner : MonoBehaviour
{
    [SerializeField] private DownLine downLineSpawn;

    private IObjectResolver objectResolver;

    [Inject]
    private void Construct (IObjectResolver objectResolver)
    {
        this.objectResolver = objectResolver;
    }

    private void Start()
    {
        MessageBroker.Default
            .Receive<GameEventMessage>()
            .Where(_ => _.EventType == GameEventType.CubikAnyDie)
            .Subscribe(message => OnCubikDestroyed((CubikMove)message.Sender))
            .AddTo(this);
    }

    private void OnCubikDestroyed(CubikMove cubik)
    {
        Spawn();
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
            Spawn();
    }

    public void Spawn()
    {
        var factory = objectResolver.Resolve<Factory>();
        var cubik = factory.SpawnNextCube(downLineSpawn.GetPosition(downLineSpawn.Sortables.Count));

        downLineSpawn.AddInList(cubik);
    }
}