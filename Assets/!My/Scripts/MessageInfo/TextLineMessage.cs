using System;
using System.Linq;

public struct TextLineMessage
{
    public string Text;
    public int Priority;

    public TextLineMessage(string text, int priority = 0)
    {
        Text = text;
        Priority = priority;
    }
}

public struct TextLineLocalizMessage
{
    public string TextID;
    public int Priority;

    public TextLineLocalizMessage(string textID, int priority = 0)
    {
        TextID = textID;
        Priority = priority;
    }
}