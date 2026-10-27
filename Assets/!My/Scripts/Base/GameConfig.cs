using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "KidsGame/GameConfig")]
public class GameConfig : ScriptableObject, IGameConfig
{
    [Space, Header("Game Settigs")]
    [SerializeField] private int countCubiks = 20;
    [SerializeField] private CubikInfo[] cubikInfoPresets;

    [Space, Header("Load behaviour")]
    [SerializeField] private bool isTryLoadOnStart = true;

    [Space, Header("Game Rule")]

    [SerializeField, Tooltip("Применить правлиа из конфига? (только при запуске игры)")]
    private bool isUseGameRule = true;

    [SerializeField, Tooltip("Кубик возвращается в башню, если не нашёл место")] 
    private bool isCanReturnInTower = true;

    [SerializeField, Tooltip("Можно брать любой кубик, а не только первый")] 
    private bool isCanGrapAnyCubik = false;

    public int CountCubiks => countCubiks;
    public CubikInfo[] CubikInfoPresets => cubikInfoPresets;

    public bool IsUseGameRule => isUseGameRule;
    public bool IsCanReturnInTower => isCanReturnInTower;    
    public bool IsCanGrapAnyCubik => isCanGrapAnyCubik;
    public bool IsTryLoad => isTryLoadOnStart;
}

public interface IGameConfig
{
    public int CountCubiks { get; }
    public CubikInfo[] CubikInfoPresets { get; }

    public bool IsUseGameRule { get; }
    public bool IsCanReturnInTower { get; }
    public bool IsCanGrapAnyCubik { get; }
    public bool IsTryLoad { get; }
}