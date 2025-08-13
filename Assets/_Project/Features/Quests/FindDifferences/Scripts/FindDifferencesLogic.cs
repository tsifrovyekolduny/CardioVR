using System.Linq;
using UnityEngine;

public class FindDifferencesLogic : MonoBehaviour, IQuestLogic
{
    private DifferentImagesHolder[] _imageHolders;

    private void Start()
    {
        _imageHolders = GetComponentsInChildren<DifferentImagesHolder>();
    }

    public bool IsCompleted()
    {
        return _imageHolders.All(g => g.IsCompleted);
    }

    public void StartLogic()
    {
        
    }
}
