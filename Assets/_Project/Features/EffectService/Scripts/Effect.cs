using UnityEngine;

public class Effect : MonoBehaviour
{
    public float Duration = 0f;    
    public void Start()
    {
        // TODO временная мера. В будущем можно сделать через пул
        Destroy(gameObject, Duration);
    }
}

