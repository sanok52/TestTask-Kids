using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[DefaultExecutionOrder(-1000)]
public class GameScope : LifetimeScope
{
    [Space]
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private FabricConfig fabricConfig;
    [SerializeField] private AnimationsConfig animationsConfig;
    [SerializeField] private DragDropSettings dragDropSettings;

    [Space]
    [SerializeField] private ScrollMouseController scrollMouseController;
    [SerializeField] private CubikSpawner cubikSpawner;
    [SerializeField] private LocalizationData localizationData;
    [SerializeField] private LocalizationMessageView messageView;
    [SerializeField] private SaveManager saveManager;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance<IGameConfig>(gameConfig);
        builder.RegisterInstance<IFabricConfig>(fabricConfig);
        builder.RegisterInstance<IAnimationsConfig>(animationsConfig);

        builder.Register<Factory>(Lifetime.Singleton);

        builder.RegisterInstance<Camera>(Camera.main);

        builder.RegisterInstance(dragDropSettings);
        builder.Register<DragDropModel>(Lifetime.Singleton);
        builder.Register<DragDropController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

        builder.RegisterInstance<ScrollMouseController>(scrollMouseController).AsSelf();
        builder.RegisterInstance<CubikSpawner>(cubikSpawner);

        builder.RegisterInstance(localizationData);
        builder.Register<LocalizationManager>(Lifetime.Singleton).AsSelf();
        builder.RegisterInstance(messageView);

        var mess = new MessageTextLineObserver();
        builder.RegisterInstance(mess);

        builder.RegisterInstance(saveManager);

        builder.RegisterEntryPoint<EntryPoint>();
    }
}