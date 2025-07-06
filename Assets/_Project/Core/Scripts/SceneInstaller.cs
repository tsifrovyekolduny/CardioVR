using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private class QuestCouple
    {
        public BaseQuest baseQuestPrefab { get; set; }
        public NarratorPhraseScriptable narratorPhrase { get; set; }
    }

    [SerializeField] private QuestCouple[] questThings;
    [SerializeField] private Transform SpawnPoint; 
    public override void InstallBindings()
    {
        // спавним уровни
        foreach (var pair in questThings) {
            BaseQuest baseQuest = Container.InstantiatePrefabForComponent<BaseQuest>(pair.baseQuestPrefab, SpawnPoint.position, Quaternion.identity, null);
            Container.BindInterfacesAndSelfTo<BaseQuest>().FromInstance(baseQuest).AsTransient();
        }

        // спавним нарратор
        Container.Bind<Narrator>().AsSingle().NonLazy();
    }
}