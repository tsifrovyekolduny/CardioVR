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
[RequireComponent(typeof(VisibilityAnimator))]
public class TriggerVisitor : MonoBehaviour
{
    [SerializeField] private string _visitorName;
    private XRGrabInteractable _xrGrab;

    private void Start()
    {
        gameObject.name = _visitorName;
        _xrGrab = GetComponent<XRGrabInteractable>();
        ChangeInteractiveStats(false);
    }

    public void ChangeInteractiveStats(bool status)
    {
        _xrGrab.enabled = status;
    }

}
