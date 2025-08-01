using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{

    [SerializeField] private BaseQuest[] _questThings;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 10f);
    [SerializeField] private Narrator _narratorPrefab;

    private Vector3 _tempOffset = Vector3.zero;

    public override void InstallBindings()
    {
        BindSystems();
        BindQuests();
    }

    private void BindSystems()
    {
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Operator>().AsSingle().NonLazy();

        Narrator narratorInstance = Container.InstantiatePrefabForComponent<Narrator>(
            _narratorPrefab,
            Vector3.zero,
            Quaternion.identity,
            null);
        Container.Bind<INarrator>().FromInstance(narratorInstance).AsSingle().NonLazy();
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