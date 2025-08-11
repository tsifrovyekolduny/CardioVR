using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EffectService : IInitializable, IEffectService
{   
    private DiContainer _container;
    private List<EffectConfig> _effectConfigs = new();
    private readonly Dictionary<VisualEffectType, Effect> _prefabMap = new();    

    [Inject]
    private void Construct(DiContainer container, EffectsScriptable effectsConfig)
    {
        _container = container;
        _effectConfigs = effectsConfig.ListOfEffects;
    }

    public void Initialize()
    {
        foreach (var config in _effectConfigs)
        {
            if (config.prefab == null)
            {
                Debug.LogError($"Эффект {config.type} не имеет префаба!");
                continue;
            }

            _prefabMap[config.type] = config.prefab;
        }
    }

    public void PlayEffect(VisualEffectType effectType, Transform transform)
    {
        if (!_prefabMap.TryGetValue(effectType, out Effect prefab))
        {
            Debug.LogError($"Не найден префаб для эффекта: {effectType}");
            return;
        }

        Effect instance = _container.InstantiatePrefabForComponent<Effect>(prefab, transform);
        instance.transform.localPosition = Vector3.zero;
    }    
}