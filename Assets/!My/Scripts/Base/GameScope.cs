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
        RegisterBase(builder);
        RegisterFactory(builder);
        RegisterDragDrop(builder);

        builder.RegisterInstance<ScrollMouseController>(scrollMouseController).AsSelf();
        builder.RegisterInstance<CubikSpawner>(cubikSpawner);

        RegisterLocalizationAndTextView(builder);
        RegisterSaveSystem(builder);

        builder.RegisterEntryPoint<EntryPoint>();
    }

    private void RegisterBase(IContainerBuilder builder)
    {
        builder.RegisterInstance<IGameConfig>(gameConfig);
        builder.RegisterInstance<IAnimationsConfig>(animationsConfig);

        builder.RegisterInstance<Camera>(Camera.main);
    }

    private void RegisterFactory(IContainerBuilder builder)
    {
        builder.RegisterInstance<IFabricConfig>(fabricConfig);
        builder.Register<Factory>(Lifetime.Singleton);
    }

    private void RegisterDragDrop(IContainerBuilder builder)
    {
        builder.RegisterInstance(dragDropSettings);
        builder.Register<DragDropModel>(Lifetime.Singleton);
        builder.Register<DragDropController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
    }

    private void RegisterLocalizationAndTextView(IContainerBuilder builder)
    {
        builder.RegisterInstance(localizationData);
        builder.Register<LocalizationManager>(Lifetime.Singleton).AsSelf();
        builder.RegisterInstance(messageView);
        var mess = new MessageTextLineObserver();
        builder.RegisterInstance(mess);
    }

    private void RegisterSaveSystem(IContainerBuilder builder)
    {
        builder.RegisterInstance(saveManager);
    }
}