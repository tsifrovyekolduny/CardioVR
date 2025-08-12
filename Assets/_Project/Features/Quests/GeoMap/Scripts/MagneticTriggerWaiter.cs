using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MagneticTriggerWaiter : MonoBehaviour, ITriggerWaiter
{
    [Header("Настройки магнетизма")]
    [Tooltip("Сила притяжения к центру зоны")]
    public float magneticForce = 10f;
    [Tooltip("Расстояние, на котором начинается притяжение")]
    public float attractionRadius = 2f;
    [Tooltip("Расстояние, на котором начинается отталкивание (если неправильно)")]
    public float repulsionRadius = 0.8f;
    [Tooltip("Сила отталкивания (если неправильно)")]
    public float repulsionForce = 5f;
    [Tooltip("Насколько сильно объект будет отодвинут (расстояние от центра)")]
    public float repulsionDistance = 0.5f;
    [Tooltip("Скорость, с которой объект возвращается к центру после отталкивания")]
    public float returnSpeed = 2f;

    [Header("Ожидаемый гость")]
    [SerializeField] private string _expectedVisitorGameObjectName;

    private bool _isRightEntrance = false;
    public bool IsRightEntrance => _isRightEntrance;
    public string ExpectedVisitorGameObjectName => _expectedVisitorGameObjectName;

    private BoxCollider _collider;
    private Vector3 _center;
    private bool _isGrabbed = false;
    private Rigidbody _grabbedRigidbody;
    private Vector3 _initialPosition;
    private Vector3 _repulseDirection;
    

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _center = transform.position + _collider.center;
    }

    void Update()
    {
        if (!_isGrabbed || _grabbedRigidbody == null) return;

        Vector3 toCenter = _center - _grabbedRigidbody.position;
        float distance = toCenter.magnitude;

        // Если слишком далеко — возвращаемся
        if (distance > attractionRadius)
        {
            _grabbedRigidbody.linearVelocity = Vector3.zero;
            return;
        }

        // Притягиваем к центру
        if (distance < repulsionRadius)
        {
            Vector3 force = toCenter.normalized * magneticForce * Time.deltaTime;
            _grabbedRigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        // Если неправильная зона — отталкиваем
        if (!IsCorrectZone(_grabbedRigidbody.gameObject.name)
            && distance < repulsionRadius)
        {
            Vector3 repulseDir = (transform.position - _grabbedRigidbody.position).normalized;
            _repulseDirection = repulseDir;
            _grabbedRigidbody.AddForce(repulseDir * repulsionForce, ForceMode.Impulse);

            // Запоминаем направление, чтобы потом вернуть
            _initialPosition = _grabbedRigidbody.position;
        }

        // После отталкивания — медленно возвращаем обратно
        if (!IsCorrectZone(_grabbedRigidbody.gameObject.name)
            && distance > repulsionRadius)
        {
            Vector3 returnDir = (_center - _grabbedRigidbody.position).normalized;
            _grabbedRigidbody.linearVelocity = returnDir * returnSpeed;
        }
    }

    public bool IsCorrectZone(string visitorName)
    {
        return visitorName == ExpectedVisitorGameObjectName;
    }

    private void OnTriggerEnter(Collider other)
    {
        var triggerVisitor = other.GetComponent<TriggerVisitor>();
        OnEnter(triggerVisitor);
    }

    private void OnTriggerExit(Collider other)
    {
        var triggerVisitor = other.GetComponent<TriggerVisitor>();
        OnExit(triggerVisitor);
    }

    public void OnEnter(TriggerVisitor visitor)
    {
        if (visitor == null) return;

        var rigidbody = visitor.GetComponent<Rigidbody>();
        if (rigidbody == null) return;

        _grabbedRigidbody = rigidbody;
        _isGrabbed = true;

        // Сохраняем начальную позицию
        _initialPosition = rigidbody.position;
    }

    public void OnExit(TriggerVisitor visitor)
    {
        if (visitor == null) return;

        _isGrabbed = false;
        _grabbedRigidbody = null;
    }

    public void OnCorrectEntrance()
    {
        _isRightEntrance = true;
        Debug.Log($"Правильное попадание в зону: {name}");
    }

    public void OnWrongEntrance()
    {
        _isRightEntrance = false;
        Debug.Log($"Неправильное попадание в зону: {name}");
    }
}