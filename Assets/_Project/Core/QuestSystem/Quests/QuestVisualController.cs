using UnityEngine;

public class QuestVisualController : MonoBehaviour, IQuestVisualController
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector3 _localPosition;
    [SerializeField] private Quaternion _localRotation;

    public void SetParent(Transform parent) => transform.SetParent(parent);
    public void SetLocalPosition(Vector3 pos) => transform.localPosition = pos;
    public void SetLocalRotation(Quaternion rot) => transform.localRotation = rot;

    public void Show(bool instant = false) => EnableChildren(true, instant);
    public void Hide(bool instant = false) => EnableChildren(false, instant);

    private void EnableChildren(bool show, bool instant)
    {
        foreach (Transform child in transform)
        {
            if (child.tag != "Decor") continue;
            var animators = child.GetComponentsInChildren<VisibilityAnimator>();
            foreach (var a in animators)
            {
                if (show)
                {
                    a.Show(instant);
                }
                else
                {
                    a.Hide(instant);
                }
            }
        }
    }
}