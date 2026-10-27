using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

public class Factory
{
    public CubikMove cubikMovePref;
    public CubikInfo[] CubikInfos => gameConfig.CubikInfoPresets;

    private IFabricConfig fabricConfig;
    private IGameConfig gameConfig;

    private IObjectResolver objectResolver;

    [Inject]
    public Factory(IObjectResolver objectResolver, IFabricConfig fabricConfig, IGameConfig gameConfig)
    {
        this.objectResolver = objectResolver;

        this.fabricConfig = fabricConfig;
        cubikMovePref = fabricConfig.CubikMovePrefab;
        this.gameConfig = gameConfig;
    }

    public CubikMove SpawnNextCube(Vector3 position)
    {
        var cubick = CreateCubik(position);
        cubick.SetInfo(GetNextCubikInfo());

        return cubick;
    }

    public CubikMove CreateCubik(Vector3 position)
    {
        var cubick = objectResolver.Instantiate(cubikMovePref, position, Quaternion.identity);
        objectResolver.Inject(cubick);
        return cubick;
    }

    private CubikInfo GetNextCubikInfo()
    {
        return CubikInfos[Random.Range(0, CubikInfos.Length)];
    }

    public void CreatePoofVFX(Vector3 position)
    {
        var vfx = GameObject.Instantiate(fabricConfig.PoofVFX, position, Quaternion.identity);
        GameObject.Destroy(vfx, 5f);
    }
}