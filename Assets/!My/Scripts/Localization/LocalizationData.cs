using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "KidsGame/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [SerializeField] private List<LocalizationElement> localizations = new ();

    public string Get(Language language, string ID)
    {
        return localizations.First(_ => _.ID == ID).Get(language);
    }
}

[Serializable]
public struct LocalizationElement
{
    public string ID;
    public LocalizationLanguageElement[] elements;

    public string Get(Language language)
    {
        return elements.First(_ => _.Language == language).Text;
    }
}

[Serializable]
public struct LocalizationLanguageElement
{
    public string Text;
    public Language Language;
}