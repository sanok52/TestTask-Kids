using System;
using UniRx;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class MessageTextLineObserver
{
    public const string idCubikDrop = "CubikDrop";
    public const string idToHight = "ToHight";
    public const string idCubikEmptyDie = "CubikEmptyDie";
    public const string idCubikHoleDie = "CubikHoleDie";

    [Inject]
    public MessageTextLineObserver()
    {
        Init();
    }

    public void Init()
    {
        MessageBroker.Default
            .Receive<GameEventMessage>()
            .Subscribe((gameEv) => SendGameEventMessage(gameEv));
    }

    private void SendGameEventMessage(GameEventMessage gameEv)
    {
        int? priority = gameEv.OverridePriority;
        if (!string.IsNullOrEmpty(gameEv.OverrideMessage))
        {
            if (priority == null)
                priority = 0;

            SendMessage(gameEv.OverrideMessage, priority.Value);
            return;
        }

        switch (gameEv.EventType)
        {
            case GameEventType.CubikDrop:
                SendMessage(idCubikDrop);
                break;

            case GameEventType.ToHight:
                SendMessage(idToHight);
                break;

            case GameEventType.CubikEmptyDie:
                SendMessage(idCubikEmptyDie, -1);
                break;

            case GameEventType.CubikHoleDie:
                SendMessage(idCubikHoleDie);
                break;

            default:
                break;
        }
    }

    public void SendMessage(string text, int priority = 0)
    {
        MessageBroker.Default.Publish(new TextLineLocalizMessage(text, priority));
    }
}