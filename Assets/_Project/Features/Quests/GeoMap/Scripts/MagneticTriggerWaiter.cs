using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

enum MagneticEntranceState
{
    FirstEntrance, Repulse, FinalAccept
}

[RequireComponent(typeof(BoxCollider))]
public class MagneticTriggerWaiter : MonoBehaviour, ITriggerWaiter
{
    [Header("Настройки анимации")]
    [Tooltip("Время анимации притяжения")]
    public float attractDuration = 0.5f;

    [Tooltip("Смещение вперёд при полном принятии")]
    public Vector3 _finalOffset;

    [Tooltip("Время отталкивания (выплевывания)")]
    public float repulseDuration = 0.3f;
    [Tooltip("Расстояние, на которое отталкиваем вниз")]
    public float repulseDistance = 0.2f;
    [Tooltip("Задержка перед анимацией")]
    public float delayDuration = 2f;

    [Tooltip("Максимальная глубина поиска для подключения")]
    public int maxDepth = 1;

    [Header("Ожидаемый гость")]
    [SerializeField] private string _expectedVisitorGameObjectName;

    [SerializeField] private bool _isRightEntrance = false;
    public bool IsRightEntrance => _isRightEntrance;
    public string ExpectedVisitorGameObjectName => _expectedVisitorGameObjectName;
    private XRGrabInteractable _grabbedXR;
    private bool _isFull = false;

    void Awake()
    {
        gameObject.name = _expectedVisitorGameObjectName;
    }

    private bool IsXRExistOrNotSelected(Collider other, out XRGrabInteractable xRGrab, string whom)
    {
        xRGrab = other.GetComponent<XRGrabInteractable>();
        if (_isFull) { return false; }
        if (xRGrab == null) return false;
        return !xRGrab.isSelected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsXRExistOrNotSelected(other, out _grabbedXR, "enter")) { return; }

        Debug.Log($"{other.name} вошла в зону магнита");

        // Проверяем, правильная ли зона
        bool isCorrect = _grabbedXR.gameObject.name == ExpectedVisitorGameObjectName;

        StartCoroutine(DelayedLaunch(MagneticEntranceState.FirstEntrance, isCorrect));
    }

    IEnumerator DelayedLaunch(MagneticEntranceState state, bool isCorrect = false)
    {
        yield return new WaitForSeconds(delayDuration);
        if (state == MagneticEntranceState.FirstEntrance)
        {
            Debug.Log("First Entrance");
            AttractToCenter(isCorrect);
        }
        else if (state == MagneticEntranceState.Repulse)
        {
            Debug.Log("Repulse");
            Repulse();
        }
        else
        {
            Debug.Log("Accept");
            Acccept();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsXRExistOrNotSelected(other, out _grabbedXR, "exit")) { return; }

        Debug.Log($"{other.name} вышел из зоны магнита");

        // _grabbedXR = null;
    }

    private void AttractToCenter(bool isCorrect)
    {
        if (_grabbedXR == null) { return; }
        // Смещаем вперёд относительно цели        
        Vector3 targetPos = transform.localPosition;

        LeanTween.moveLocal(_grabbedXR.gameObject, targetPos, attractDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                if (isCorrect)
                {
                    StartCoroutine(DelayedLaunch(MagneticEntranceState.FinalAccept));
                }
                else
                {
                    StartCoroutine(DelayedLaunch(MagneticEntranceState.Repulse));
                }

            });
    }

    private void Acccept()
    {
        OnCorrectEntrance();
    }

    private void Repulse()
    {
        if (_grabbedXR == null) { return; }

        // Теперь толкаем вниз
        Vector3 repulseDir = transform.localPosition + (Vector3.down * repulseDistance);
        LeanTween.moveLocal(_grabbedXR.gameObject, repulseDir, repulseDuration)
            .setEase(LeanTweenType.easeInCirc)
            .setOnComplete(() =>
            {
                OnWrongEntrance();
            });

    }

    public void OnCorrectEntrance()
    {
        if (_grabbedXR == null) { return; }

        _grabbedXR.interactionLayers = LayerMask.GetMask("Interactable");
        _grabbedXR.transform.parent = transform;
        _grabbedXR.enabled = false;
        LeanTween.moveLocal(_grabbedXR.gameObject, _finalOffset, attractDuration)
            .setEase(LeanTweenType.easeInCirc);
        _isRightEntrance = true;
        _isFull = true;
        Debug.Log($"✅ Правильное попадание в зону: {name}");
    }

    public void OnWrongEntrance()
    {
        _isRightEntrance = false;
        Debug.Log($"❌ Неправильное попадание в зону: {name}");
    }
}