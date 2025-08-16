using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISaveSystem
{
    public void WriteQuestToCurrentProfile(QuestEntity quest);
    public List<ChildProfile> GetProfiles();
    public ChildProfile SaveProfile(string surname, string name, string patronymic, int age);
    public void SetProfile(ChildProfile profile);
}