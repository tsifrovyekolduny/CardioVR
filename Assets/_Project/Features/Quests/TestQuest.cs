using UnityEngine;

public class TestQuest : BaseQuest
{
    private bool _isExitedFromArea = false;

    public override bool IsFinished()
    {
        return _isExitedFromArea;
    }

    public void OnTriggerExit(Collider other)
    {
        _isExitedFromArea = true;
    }
}
