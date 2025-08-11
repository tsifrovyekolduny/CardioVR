using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(XRGeneralGrabTransformer))]
[RequireComponent(typeof(ReturnToBaseComponent))]
public class TriggerVisitor : MonoBehaviour
{
    [SerializeField] private string _visitorName;

    private void Start()
    {
        gameObject.name = _visitorName;
    }
}
