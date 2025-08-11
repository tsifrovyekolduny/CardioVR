using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class ZoneTriggerComponent : MonoBehaviour
{
    [Header("Настройки")]
    public float returnDelay = 0f; // задержка перед возвратом (если нужно)
    [SerializeField] string _interactiveTag;
    private List<GameObject> activeCubes = new List<GameObject>();

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag(_interactiveTag))
        {
            // Добавляем куб в список активных
            if (!activeCubes.Contains(other.gameObject))
            {
                activeCubes.Add(other.gameObject);
                Debug.Log($"Куб {other.name} вошёл в зону.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_interactiveTag))
        {
            GameObject cube = other.gameObject;

            // Удаляем из списка
            if (activeCubes.Remove(cube))
            {
                Debug.Log($"Куб {cube.name} вышел из зоны. Возвращаем...");

                // Задержка (опционально) — например, чтобы не мигать при быстром выходе
                if (returnDelay > 0f)
                {
                    Invoke(nameof(ReturnCube), returnDelay);
                }
                else
                {
                    ReturnCube();
                }
            }
        }
    }

    void ReturnCube()
    {
        // Ищем компонент ReturnToBaseComponent на кубе
        ReturnToBaseComponent returnScript = GetComponent<ReturnToBaseComponent>();
        if (returnScript != null)
        {
            returnScript.ReturnToBase();
        }
        else
        {
            Debug.LogError("Куб не имеет скрипта ReturnToBaseComponent!");
        }
    }
}