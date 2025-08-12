using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(XRGeneralGrabTransformer))]
[RequireComponent(typeof(ReturnToBaseComponent))]
[RequireComponent(typeof(VisibilityAnimator))]
public class TriggerVisitor : MonoBehaviour
{
    [SerializeField] protected string _visitorName;
    private XRGrabInteractable _xrGrab;

    protected virtual void Awake()
    {
        gameObject.name = _visitorName;
        _xrGrab = GetComponent<XRGrabInteractable>();
        _xrGrab.farAttachMode = UnityEngine.XR.Interaction.Toolkit.Attachment.InteractableFarAttachMode.DeferToInteractor;
        ChangeInteractiveStats(false);
    }

    public void ChangeInteractiveStats(bool status)
    {
        _xrGrab.enabled = status;
    }

}
