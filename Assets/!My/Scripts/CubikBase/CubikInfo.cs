using System;
using UnityEngine;

[Serializable]
public class CubikInfo
{
    [SerializeField] private CubikColor color;
    [SerializeField] private Sprite sprite;

    public CubikColor Color { get => color; set => color = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
}

public enum CubikColor
{
    Red,
    BrightRed,
    Orange,
    Yellow,
    Green,
    BrightGreen,
    LightGreen,
    Blue,
    BrightBlue,
    LightBlue,
    Purple,
    DarkPurple,
    LightPurple,
    Gray,
    Brown,
    DarkBrown,
    Black,
    Violet,
    BlueViolet,
    White,
};