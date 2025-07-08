using UnityEngine;
using Zenject;
using Zenject.Asteroids;

public class SceneInstaller : MonoInstaller
{

    [SerializeField] private BaseQuest[] _questThings;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 10f);
    private Vector3 _tempOffset = Vector3.zero;
    public override void InstallBindings()
    {
        BindSystems();
        BindQuests();
    }

    private void BindSystems()
    {
        Container.Bind<Narrator>().AsSingle().NonLazy();
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Operator>().AsSingle().NonLazy();
    }

    private void BindQuests()
    {
        foreach (var quest in _questThings)
        {
            Vector3 newSpawnPoint = _spawnPoint.position + _tempOffset;
            BaseQuest baseQuest = Container.InstantiatePrefabForComponent<BaseQuest>(quest, newSpawnPoint, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<BaseQuest>().FromInstance(baseQuest).AsTransient();
            _tempOffset += _offset;
        }
    }
}