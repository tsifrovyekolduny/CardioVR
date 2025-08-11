using UnityEngine;

public class MeshVisual : IVisualComponent
{
    private readonly MeshRenderer _renderer;
    private readonly IEffectService _effectService;           

    public MeshVisual(MeshRenderer renderer, IEffectService effectService)
    {
        _renderer = renderer;
        _effectService = effectService;
    }

    public void Show(float duration)
    {    
        _effectService.PlayEffect(VisualEffectType.ShowHideCloud, _renderer.transform);
        _renderer.enabled = true;
    }

    public void Hide(float duration)
    {     
        _effectService.PlayEffect(VisualEffectType.ShowHideCloud, _renderer.transform);
        _renderer.enabled = false;
    }
}