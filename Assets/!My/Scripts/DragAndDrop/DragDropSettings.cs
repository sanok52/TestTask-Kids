using UnityEngine;

[CreateAssetMenu(fileName = "DragDropSettings", menuName = "KidsGame/DragDropSettings")]
public class DragDropSettings : ScriptableObject
{
    public LayerMask LayerMask;
    public KeyCode KeyCodeHold;
    public float RadiusGrap = 0.5f;

    [Space]
    public bool PCModeInEditor = true;
}
