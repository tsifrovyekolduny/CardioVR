// QuestPhaseController.cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestPhaseController : MonoBehaviour, IQuestPhasable
{
    [SerializeField] private List<Phase> _phases = new List<Phase>();
    private int _currentPhaseIndex = -1;

    public List<Phase> Phases => _phases;

    public void Initialize(List<Phase> phases)
    {
        _phases = phases;
        if (_phases.Count > 0)
        {
            _currentPhaseIndex = 0;
        }
    }

    public bool IsPhasesGone()
    {
        return _phases.All(p => p.IsGone);
    }

    public void NextPhase()
    {
        // Старая фаза завершается тогда, когда начинается новая
        if (_currentPhaseIndex > -1)
        {
            _phases[_currentPhaseIndex].Complete();
        }

        if (_currentPhaseIndex < _phases.Count - 1)
        {
            _currentPhaseIndex++;
            ExecuteCurrentPhase();
        }
    }
    private void ExecuteCurrentPhase()
    {
        if (_currentPhaseIndex >= 0 && _currentPhaseIndex < _phases.Count)
        {
            var currentPhase = _phases[_currentPhaseIndex];
            Debug.Log($"Executing phase: {currentPhase.Name}");
            currentPhase.SomeAction?.Invoke();
        }
    }

    [ExecuteInEditMode]
    private void Start()
    {
        if (_phases.Count == 0)
        {
            Debug.LogWarning($"Не объявлены фазы для квеста {gameObject.name}");
        }
    }
}