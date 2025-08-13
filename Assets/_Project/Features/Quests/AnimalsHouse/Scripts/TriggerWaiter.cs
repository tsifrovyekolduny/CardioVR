using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TriggerWaiter : MonoBehaviour, ITriggerWaiter
{
    public bool IsRightEntrance { get => _isRightEntrance; }

    public string ExpectedVisitorGameObjectName => _expectedVisitorGameObjectName;

    private bool _isRightEntrance = false;
    [SerializeField] string _expectedVisitorGameObjectName;
    [SerializeField] Color _rightEntranceColor = Color.green;
    [SerializeField] Color _wrongEntranceColor = Color.red;
    private MeshRenderer _triggerModel;
    private BoxCollider _boxCollider;
    
    void Start()
    {
        _triggerModel = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        ChangeTriggerMode(false);
    }

    void ChangeTriggerMode(bool rightEntrance)
    {
        _isRightEntrance = rightEntrance;
        _triggerModel.material.color = rightEntrance ? _rightEntranceColor : _wrongEntranceColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == _expectedVisitorGameObjectName)
        {
            ChangeTriggerMode(true);
        }
        else
        {
            ChangeTriggerMode(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == _expectedVisitorGameObjectName)
        {
            ChangeTriggerMode(false);
        }        
    }
}
