using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class MessageView : MonoBehaviour, ITextLineView
{
    [SerializeField] private TMP_Text messageTMP;
    [SerializeField] private float delayShow = 5f;
    [SerializeField] private bool autoSubscrube = true;
    protected int lastPriority;

    public bool IsShow { get; private set; }

    private SerialDisposable hideMessageDisposable = new();

    public virtual void Start()
    {
        if (autoSubscrube)
            MessageBroker.Default
                .Receive<TextLineMessage>()
                .Subscribe(OnMessage)
                .AddTo(this);
    }

    private void OnMessage(TextLineMessage message)
    {
        ShowMessage(message.Text);
    }

    public virtual void ShowMessage(string mess, int priority = 0)
    {
        if (IsShow && lastPriority > priority)
            return;

        lastPriority = priority;
        ShowText(mess);
    }

    protected virtual void ShowText (string text)
    {
        messageTMP.text = text;
        IsShow = true;

        hideMessageDisposable.Disposable = Observable
            .Timer(TimeSpan.FromSeconds(delayShow))
            .Subscribe(_ =>
            {
                messageTMP.text = "";
                IsShow = false;
            });
    }

    private void OnDestroy()
    {
        hideMessageDisposable.Dispose();
    }
}