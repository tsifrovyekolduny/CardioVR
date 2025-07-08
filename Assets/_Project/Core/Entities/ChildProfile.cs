
[System.Serializable]
public class ChildProfile
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }    
    public int? QuestNumber { get; set; }
    public QuestStates? State { get; set; }
}

