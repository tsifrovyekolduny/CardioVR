using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class OperatorPresenter
{
    private readonly SaveSystem _saveSystem;
    private readonly IOperator _operator;
    private readonly IOperatorView _view;

    private List<ChildProfile> _profiles;

    public OperatorPresenter(SaveSystem saveSystem, IOperator @operator, IOperatorView view)
    {
        _saveSystem = saveSystem;
        _operator = @operator;
        _view = view;

        // Подписываемся на события
        _view.ProfileSaveRequested += HandleSaveProfile;
        _view.SearchTextChanged += HandleSearchTextChanged;
        _view.ProfileSelected += HandleProfileSelected;
        _view.SessionStart += HandleSessionStart;
        _view.SessionEnd += HandleSessionEnd;        
        _view.AnswerGived += HandleGivedAnswer;
        _operator.OnQuestStarted += HandleQuestStart;
    }

    private void HandleGivedAnswer(string obj)
    {
        // todo работать с SaveSystem для записи ответа        
    }    

    private void HandleSessionEnd()
    {
        _operator.EndSession();
    }

    public void Initialize()
    {
        _profiles = _saveSystem.GetProfiles();
        Debug.Log($"Profiles set: {_profiles.Count}");
        _view.ShowProfiles(_profiles);
    }

    private void HandleQuestStart(IQuest quest)
    {
        var phases = quest.GetQuestController<IQuestPhasable>().Phases;
        _view.SetPhases(phases);
    }

    private void HandleSaveProfile(string surname, string name, string patronymic, int age)
    {
        var profile = _saveSystem.SaveProfile(surname, name, patronymic, age);
        _profiles.Add(profile);
        _view.ShowProfiles(_profiles);
        _view.ClearInputFields();        
        HandleProfileSelected(profile);
    }

    private void HandleSearchTextChanged(string text)
    {
        var filtered = _profiles.Where(p =>
            p.Surname.Contains(text) ||
            p.Name.Contains(text) ||
            p.Patronymic.Contains(text)).ToList();
        _view.ShowProfiles(filtered);
    }

    private void HandleProfileSelected(ChildProfile profile)
    {
        _saveSystem.SetProfile(profile);
        _view.ShowChosenProfile(profile);        
    }

    private void HandleSessionStart()
    {
        _operator.StartSession();
        _view.ShowPage2();        
    }

    public void Update()
    {
        _view.UpdateTimer(_operator.LostTime);
    }
}