using UnityEngine;
using Zenject;
using Zenject.Asteroids;

public class SceneInstaller : MonoInstaller
{

    [SerializeField] private QuestPrefabsObject _questPrefabsObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 10f);
    [SerializeField] private TilePrefabsObject _tilePrefabsObject;
    private Vector3 _tempOffset = Vector3.zero;
    public override void InstallBindings()
    {
        BindSettings();
        BindSystems();
    }

    public void BindSettings()
    {
        Container.BindInstance(_tilePrefabsObject).AsSingle();
        Container.BindInstance(_questPrefabsObject).AsSingle();
    }

    private void BindSystems()
    {
        Container.BindInterfacesAndSelfTo<Narrator>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Operator>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TileManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestManager>().AsSingle().NonLazy();
    }

    /***private void BindQuests()
    {
        foreach (var quest in _questThings)
        {
            Vector3 newSpawnPoint = _spawnPoint.position + _tempOffset;
            BaseQuest baseQuest = Container.InstantiatePrefabForComponent<BaseQuest>(quest, newSpawnPoint, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<BaseQuest>().FromInstance(baseQuest).AsTransient();
            _tempOffset += _offset;
        }
    }***/
    
}