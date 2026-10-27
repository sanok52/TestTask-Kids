using UnityEngine;

[CreateAssetMenu(fileName = "FabricConfig", menuName = "KidsGame/FabricConfig")]
public class FabricConfig : ScriptableObject, IFabricConfig
{
    [SerializeField] private CubikMove cubikMovePrefab;
    [SerializeField] private GameObject poofVFX;

    public CubikMove CubikMovePrefab => cubikMovePrefab;
    public GameObject PoofVFX => poofVFX;
}

public interface IFabricConfig
{
    public CubikMove CubikMovePrefab { get; }
    public GameObject PoofVFX { get; }
}
