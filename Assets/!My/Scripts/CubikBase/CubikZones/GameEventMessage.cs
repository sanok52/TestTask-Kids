public class GameEventMessage
{
    public GameEventType EventType;
    public object Sender;

    public string OverrideMessage;
    public int? OverridePriority;

    public GameEventMessage(GameEventType eventType, object sender, string overrideMessage = null, int? overridePriority = null)
    {
        EventType = eventType;
        Sender = sender;
        OverrideMessage = overrideMessage;
        OverridePriority = overridePriority;
    }
}
