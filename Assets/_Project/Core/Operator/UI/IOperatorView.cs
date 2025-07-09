using System;
using System.Collections.Generic;

public interface IOperatorView
{
    event Action<string, string, string, int> ProfileSaveRequested;
    event Action<string> SearchTextChanged;
    event Action<ChildProfile> ProfileSelected;
    event Action SessionStart;
    event Action<string> HintGived;
    event Action SessionEnd;    

    // todo передавать как объект
    event Action<int> AnswerGived;

    void ShowProfiles(List<ChildProfile> profiles);
    void ShowChosenProfile(ChildProfile profile);
    void UpdateTimer(float time);
    void ShowPage2();
    void ClearInputFields();
    void ShowHints(string[] obj);
}