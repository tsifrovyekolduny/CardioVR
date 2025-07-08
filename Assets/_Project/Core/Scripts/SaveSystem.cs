using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

class SaveSystem
{
    private List<ChildProfile> _profiles = new List<ChildProfile>();
    private string _saveLocation = Application.persistentDataPath + "/";
    private string _saveFilename = "profiles.json";
    private string _path  { get => _saveLocation + _saveFilename; }

    ChildProfile _curentProfile;
    public void Save(int questNumber, QuestStates state)
    {
        _curentProfile.QuestNumber = questNumber;
        _curentProfile.State = state;
        SaveCurrentJson();
        Debug.Log(_curentProfile);
        Debug.Log(_profiles.Where(c => c == _curentProfile).First());
    }    

    public List<ChildProfile> GetProfiles()
    {
        if (File.Exists(_path))
        {
            using (var reader = new StreamReader(_path))
            {
                string unconvertedJson = reader.ReadToEnd();
                var temp = JsonUtility.FromJson<List<ChildProfile>>(unconvertedJson);
                if(temp != null)
                {
                    _profiles = temp;
                }
            }
        }
        else
        {
            File.Create(_path).Dispose();            
        }

        return _profiles;
    }

    public ChildProfile SaveProfile(string surname, string name, string patronymic, int age)
    {
        ChildProfile profile = new ChildProfile()
        {
            Name = name,
            Patronymic = patronymic,
            Surname = surname,
            Age = age            
        };
        
        _profiles.Add(profile);
        SaveCurrentJson();

        return profile;
    }

    private void SaveCurrentJson()
    {        
        using(StreamWriter sw = (File.Exists(_path)) ? File.AppendText(_path) : File.CreateText(_path))
        {
            string json = JsonUtility.ToJson(_profiles, true);
            sw.WriteLine(json);
        }
    }

    public void SetProfile(ChildProfile profile)
    {
        _curentProfile = profile;
    }
}