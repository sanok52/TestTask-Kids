using UniRx;
using VContainer;

public class LocalizationManager
{
    private LocalizationData data;
    private Language language;

    [Inject]
    public LocalizationManager(LocalizationData data)
    {
        this.data = data;
    }

    public void SetLanguage(Language language)
    {
        this.language = language;
        MessageBroker.Default.Publish(new LanguageChangeMessage(language));
    }

    public string Get (string textID)
    {
        return data.Get(language, textID);
    }
}

public struct LanguageChangeMessage
{
    public Language Language;

    public LanguageChangeMessage(Language language)
    {
        Language = language;
    }
}