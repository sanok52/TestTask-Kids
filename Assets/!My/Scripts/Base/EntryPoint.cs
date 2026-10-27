using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EntryPoint : IStartable
{
    private IObjectResolver resolver;
    private IGameConfig gameConfig;

    [Inject]
    public EntryPoint(IObjectResolver resolver, IGameConfig gameConfig)
    {
        this.resolver = resolver;
        this.gameConfig = gameConfig;
    }

    public void Start()
    {
        if (gameConfig.IsTryLoad)
        {
            bool isLoad = resolver.Resolve<SaveManager>().Load();
            if (isLoad)
                return;
        }

        NewGame();
    }

    private void NewGame()
    {
        if (gameConfig.IsUseGameRule)
        {
            var downLineInteractChanger = GameObject.FindFirstObjectByType<DownLineInteractChanger>();
            var towerLineAuto = GameObject.FindFirstObjectByType<TowerLineAuto>();

            if (downLineInteractChanger)
                downLineInteractChanger.IsActive = !gameConfig.IsCanGrapAnyCubik;
            if (towerLineAuto)
                towerLineAuto.IsCanReturn = gameConfig.IsCanReturnInTower;
        }

        int countStartSpawn = resolver.Resolve<IGameConfig>().CountCubiks;
        resolver.Resolve<CubikSpawner>().Spawn(countStartSpawn);

        resolver.Resolve<LocalizationManager>().SetLanguage(Language.RU);
    }
}
