using UnityEngine;

public class ReturnToBaseComponent : MonoBehaviour
{
    [Header("Настройки")]
    public bool NeedToReturn;
    public Vector3 initialPosition; // Сохраняем начальную позицию
    [SerializeField] string _interactiveTag;

    void Start()
    {        
        initialPosition = transform.position;
        gameObject.tag = _interactiveTag;
    }

    // Метод, который будет вызываться из триггера
    public void ReturnToBase()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
    }
}