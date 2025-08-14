using System;
using System.Collections.Generic;

public interface IOperatorView
{
    event Action<string, string, string, int> ProfileSaveRequested;
    event Action<string> SearchTextChanged;
    event Action<ChildProfile> ProfileSelected;
    event Action SessionStart;
    event Action SessionEnd;
    event Action<string> AnswerGived;

    void ShowProfiles(List<ChildProfile> profiles);
    void ShowChosenProfile(ChildProfile profile);
    void UpdateTimer(float time);
    void ShowPage2();
    void ClearInputFields();
    void SetPhases(List<Phase> phases);
}