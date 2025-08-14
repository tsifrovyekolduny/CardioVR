using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Zenject;

public class VRPlayerInstaller : MonoInstaller
{
    [SerializeField] XROrigin _XROrigin;    
    [SerializeField] Vector3 _spawnPointPosition;
    [SerializeField] Quaternion _rotation;
    [SerializeField] private HiderUI _hiderUI;
    public override void InstallBindings()
    {
        var origin = Container.InstantiatePrefabForComponent<XROrigin>(_XROrigin);      
        origin.transform.position = _spawnPointPosition;
        origin.transform.rotation = _rotation;
        Container.InstantiatePrefabForComponent<HiderUI>(_hiderUI);
    }
}
