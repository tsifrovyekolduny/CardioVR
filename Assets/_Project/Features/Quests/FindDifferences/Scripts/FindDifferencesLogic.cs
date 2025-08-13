using System.Linq;
using UnityEngine;

public class FindDifferencesLogic : MonoBehaviour, IQuestLogic
{
    private DifferentImagesHolder[] _imageHolders;

    public bool IsCompleted()
    {
        return _imageHolders.All(g => g.IsCompleted);
    }

    public void StartLogic()
    {
        
    }
}
