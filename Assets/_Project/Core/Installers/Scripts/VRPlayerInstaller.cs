using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Zenject;

public class VRPlayerInstaller : MonoInstaller
{
    [SerializeField] XROrigin _XROrigin;
    [SerializeField] XRUIInputModule _XREventSystem;
    [SerializeField] XRInteractionManager _XRInterManager;
    [SerializeField] Vector3 _spawnPointPosition;
    [SerializeField] Quaternion _rotation;

    public override void InstallBindings()
    {
        var origin = Container.InstantiatePrefabForComponent<XROrigin>(_XROrigin);
        // var eventSystem = Container.InstantiatePrefabForComponent<XRUIInputModule>(_XREventSystem);
        // var interManager = Container.InstantiatePrefabForComponent<XRInteractionManager>(_XRInterManager);

        origin.transform.position = _spawnPointPosition;
        origin.transform.rotation = _rotation;
    }
}
