
using System;
using System.Collections.Generic;

[Serializable]
public class ChildProfile
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public int? QuestNumber { get; set; }
    public QuestStates? State { get; set; }
}

[Serializable]
public class ChildProfilesWrapper
{
    public List<ChildProfile> List { get; set; }

    public ChildProfilesWrapper()
    {
        List = new List<ChildProfile>();
    }
}