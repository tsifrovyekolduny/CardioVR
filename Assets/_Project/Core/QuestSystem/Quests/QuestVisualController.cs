using System.Collections.Generic;
using UnityEngine;


public class QuestVisualController : MonoBehaviour, IQuestVisualController
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector3 _localPosition;
    [SerializeField] private Quaternion _localRotation;
    [SerializeField] private List<VisibilityAnimator> _animators;
    [TagMaskField]
    [SerializeField] private string[] _hidedObjectsOnStart;

    private void Awake()
    {
        var anims = gameObject.GetComponentsInChildren<VisibilityAnimator>();
        for (int childIndex = 0; childIndex < gameObject.transform.childCount; ++childIndex)
        {
            var child = gameObject.transform.GetChild(childIndex);
            var childAnims = child.GetComponentsInChildren<VisibilityAnimator>();
            if (childAnims.Length > 0)
            {
                _animators.AddRange(childAnims);
            }
        }
    }

    public void SetParent(Transform parent) => transform.SetParent(parent);
    public void SetLocalPosition(Vector3 pos) => transform.localPosition = pos;
    public void SetLocalRotation(Quaternion rot) => transform.localRotation = rot;

    public void Show(bool instant = false, bool firstTime = false) => EnableChildren(true, instant, firstTime);
    public void Hide(bool instant = false, bool firstTime = false) => EnableChildren(false, instant, firstTime);

    private void EnableChildren(bool show, bool instant, bool firstTime)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Decor") continue;
            var animators = child.GetComponentsInChildren<VisibilityAnimator>();
            foreach (var a in _animators)
            {
                string[] tags = show && firstTime ? _hidedObjectsOnStart : null;
                if (show)
                {
                    a.Show(instant, tags);
                }
                else
                {
                    a.Hide(instant, tags);
                }
            }
        }
    }
}