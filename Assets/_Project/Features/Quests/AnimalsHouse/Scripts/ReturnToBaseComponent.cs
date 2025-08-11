using UnityEngine;

public class ReturnToBaseComponent : MonoBehaviour
{
    [Header("Настройки")]
    public Vector3 initialPosition; // Сохраняем начальную позицию
    [SerializeField] string _interactiveTag;
    private bool hasBeenReturned = false;

    void Start()
    {
        // Сохраняем исходную позицию при старте
        initialPosition = transform.position;
        gameObject.tag = _interactiveTag;
    }

    // Метод, который будет вызываться из триггера
    public void ReturnToBase()
    {
        if (hasBeenReturned) return;

        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;

        hasBeenReturned = true;
        Debug.Log($"Куб {name} возвращён на место.");

        // Можно добавить звук, эффект, сброс физики и т.д.
    }
}