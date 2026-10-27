using System;
using UniRx;
using UnityEngine;
using VContainer;

public class LocalizationMessageView : MessageView, ITextLineView
{
    private string lastMessageID;
    private string lastMessage;

    private IObjectResolver objectResolver;
    private LocalizationManager localizationManager;

    [Inject]
    public void Construct(IObjectResolver objectResolver, LocalizationManager localizationManager)
    {
        this.objectResolver = objectResolver;
        this.localizationManager = localizationManager;
    }

    public override void Start()
    {
        MessageBroker.Default
            .Receive<TextLineMessage>()
            .Subscribe((message) => ShowMessage(message));

        MessageBroker.Default
            .Receive<TextLineLocalizMessage>()
            .Subscribe((message) => ShowMessage(message));

        MessageBroker.Default
            .Receive<LanguageChangeMessage>()
            .Subscribe(langChange => OnChangeLanguage(langChange.Language));
    }

    private void ShowMessage(TextLineMessage message)
    {
        ShowMessage(message.Text, false, message.Priority);
    }

    public void ShowMessage(TextLineLocalizMessage message)
    {
        ShowMessage(message.TextID, true, message.Priority);
    }

    public override void ShowMessage(string mess, int priority = 0)
    {
        ShowMessage(mess, true, priority);
    }

    public void ShowMessage(string mess, bool isTranslate, int priority = 0)
    {
        if (IsShow && lastPriority > priority)
            return;

        lastMessageID = mess;
        lastMessage = isTranslate ? Translate(mess) : mess;
        lastPriority = priority;

        ShowText(lastMessage);
    }

    private string Translate(string textID)
    {
        return localizationManager.Get(textID);
    }

    private void OnChangeLanguage(Language language)
    {
        if (!IsShow)
            return;

        ShowMessage(lastMessageID);
    }
}