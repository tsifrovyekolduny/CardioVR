using UnityEngine;
using Zenject;
using Zenject.Asteroids;

public class SceneInstaller : MonoInstaller
{    

    [SerializeField] private BaseQuest[] _questThings;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _xOffset = 10f;
    private float _tempXOffset = 0f;
    public override void InstallBindings()
    {
        BindSystems();
        BindQuests();
    }

    private void BindSystems()
    {
        Container.Bind<Narrator>().AsSingle().NonLazy();
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
    }

    private void BindQuests()
    {
        foreach (var quest in _questThings)
        {
            Vector3 newSpawnPoint = new Vector3(_spawnPoint.position.x + _tempXOffset, _spawnPoint.position.y, _spawnPoint.position.z);
            BaseQuest baseQuest = Container.InstantiatePrefabForComponent<BaseQuest>(quest, newSpawnPoint, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<BaseQuest>().FromInstance(baseQuest).AsTransient();
            _tempXOffset += _xOffset;
        }
    }
}