using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Narrator _narratorPrefab;
    [SerializeField] private QuestPrefabsObject _questPrefabsObject;
    [SerializeField] private TilePrefabsObject _tilePrefabsObject;
    [SerializeField] private EffectsScriptable _effectsConfig;
    [SerializeField] private OperatorView _operatorPanel;    
    public override void InstallBindings()
    {
        BindSettings();
        BindSystems();
    }

    public void BindSettings()
    {
        Container.BindInstance(_tilePrefabsObject).AsSingle();
        Container.BindInstance(_questPrefabsObject).AsSingle();
        Container.BindInstance(_effectsConfig).AsSingle();
    }

    private void BindSystems()
    {
        Container.BindInterfacesAndSelfTo<EffectService>().AsSingle().NonLazy();
        Container.Bind<VisualComponentFactory>().AsSingle().NonLazy();
        var nar = Container.InstantiatePrefabForComponent<INarrator>(_narratorPrefab);
        Container.BindInterfacesAndSelfTo<Narrator>().FromInstance(nar).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Operator>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TileFactory>().AsSingle().NonLazy();        
        Container.BindInterfacesAndSelfTo<TileManagmentSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestManagmentSystem>().AsSingle().NonLazy();
        var opPanel = Container.InstantiatePrefabForComponent<OperatorView>(_operatorPanel);
        opPanel.transform.position = Vector3.down * 1000f;
    } 
}