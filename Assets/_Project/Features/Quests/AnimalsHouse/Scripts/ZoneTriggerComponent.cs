using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class ZoneTriggerComponent : MonoBehaviour
{
    [Header("Настройки")]
    public float returnDelay = 0f; // задержка перед возвратом (если нужно)
    [SerializeField] string _interactiveTag;
    private List<ReturnToBaseComponent> activeCubes = new List<ReturnToBaseComponent>();

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag(_interactiveTag))
        {
            var returnBaseComponent = other.GetComponent<ReturnToBaseComponent>();
            returnBaseComponent.NeedToReturn = false;
            // Добавляем куб в список активных
            if (!activeCubes.Contains(returnBaseComponent))
            {
                activeCubes.Add(returnBaseComponent);
                Debug.Log($"Куб {returnBaseComponent.name} вошёл в зону.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_interactiveTag))
        {
            var returnBaseComponent = other.GetComponent<ReturnToBaseComponent>();
            // Удаляем из списка            

            Debug.Log($"Куб {returnBaseComponent.name} вышел из зоны. Ставим на возврат");

            // Задержка (опционально) — например, чтобы не мигать при быстром выходе
            if (returnDelay > 0f)
            {
                Invoke(nameof(ReturnCube), returnDelay);
            }
            else
            {
                ReturnCube(returnBaseComponent);
            }            
        }
    }

    private void Update()
    {
        foreach(var cub in activeCubes)
        {
            if (cub.NeedToReturn)
            {
                cub.ReturnToBase();
            }
        }
    }

    void ReturnCube(ReturnToBaseComponent returnScript)
    {
        returnScript.NeedToReturn = true;
    }
}