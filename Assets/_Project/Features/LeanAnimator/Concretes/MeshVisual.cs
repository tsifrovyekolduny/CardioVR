using System;
using UnityEngine;

public class MeshVisual : AbstractVisualWithColor
{
    private readonly MeshRenderer _renderer;

    // ID свойства цвета — можно заменить на другое имя, если нужно
    private static readonly int _сolorPropertyId = Shader.PropertyToID("_Color");

    public MeshVisual(MeshRenderer renderer) => _renderer = renderer;

    protected override GameObject GetGameObject()
    {
        return _renderer.gameObject;
    }

    protected override Color GetColor()
    {
        var material = _renderer.materials[0];
        if (material == null)
        {
            Debug.LogWarning($"Материал пустой у объекта '{_renderer.gameObject.name}'");
            return Color.white;
        }

        // Проверяем, существует ли свойство _Color
        if (material.HasProperty(_сolorPropertyId))
        {
            try
            {
                return material.GetColor(_сolorPropertyId);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Не удалось прочитать цвет из материала '{material.name}': {e.Message}");
                return Color.white;
            }
        }

        // Если _Color нет — пробуем другие возможные названия
        return TryFindColorInAlternativeProperties(material);
    }

    protected override void UpdateColor(Color c)
    {
        var material = _renderer.materials[0];
        if (material == null)
        {
            Debug.LogWarning($"Материал пустой у объекта '{_renderer.gameObject.name}'");
            return;
        }

        if (material.HasProperty(_сolorPropertyId))
        {
            try
            {
                material.SetColor(_сolorPropertyId, c);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Не удалось установить цвет в материал '{material.name}': {e.Message}");
            }
        }
        else
        {
            // Попробуем изменить другой цветовой параметр
            TryUpdateColorInAlternativeProperties(material, c);
        }
    }

    // Попытка найти цвет в других возможных имённых полях
    private Color TryFindColorInAlternativeProperties(Material material)
    {
        string[] possibleNames = {
            "_BaseColor",      // У Material UI, Lit, PBR
            "_Color",          // Старый стандарт
            "BaseColor",       // Некоторые графы
            "EmissionColor",
            "TintColor"
        };

        foreach (string propName in possibleNames)
        {
            int propId = Shader.PropertyToID(propName);
            if (material.HasProperty(propId))
            {
                try
                {
                    return material.GetColor(propId);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Ошибка при чтении свойства '{propName}': {e.Message}");
                }
            }
        }

        Debug.Log($"Цвет не найден ни в одном из известных полей для материала '{material.name}'. Используем белый.");
        return Color.white;
    }

    private void TryUpdateColorInAlternativeProperties(Material material, Color c)
    {
        string[] possibleNames = {
            "_BaseColor",
            "_Color",
            "BaseColor",
            "TintColor"
        };

        foreach (string propName in possibleNames)
        {
            int propId = Shader.PropertyToID(propName);
            if (material.HasProperty(propId))
            {
                try
                {
                    material.SetColor(propId, c);
                    return; // Успешно обновили
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Ошибка при записи в '{propName}': {e.Message}");
                }
            }
        }

        Debug.Log($"Не удалось обновить цвет в материале '{material.name}' — нет подходящего свойства.");
    }
}