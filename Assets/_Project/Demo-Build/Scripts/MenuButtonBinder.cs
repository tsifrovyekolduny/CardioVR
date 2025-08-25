using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


internal class MenuButtonBinder : MonoBehaviour
{
    [SerializeField] private InputActionReference menuToggleActionReference;
    private IOperator _operator;
    private IQuestPhasable _phaser;

    [Inject]
    void Construct(IOperator @operator)
    {
        _operator = @operator;
        _operator.OnQuestStarted += _operator_OnQuestStarted;
    }

    private void _operator_OnQuestStarted(IQuest obj)
    {
        _phaser = obj.GetQuestController<QuestPhaseController>();        
    }

    private void OnEnable()
    {
        menuToggleActionReference.action.performed += NextQuestPhase;
    }

    private void OnDisable()
    {
        menuToggleActionReference.action.performed -= NextQuestPhase;
    }

    private void NextQuestPhase(InputAction.CallbackContext context)
    {
        Debug.Log("Запрос нового квеста от контроллера");
        if (_phaser != null) {
            _phaser.NextPhase();
        }        
    }
}

