using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{    
    private ChildProfilesWrapper _profilesWrapper = new ChildProfilesWrapper();
    private string _saveLocation = Application.persistentDataPath + "/";
    private string _saveFilename = "profiles.dat";
    private string _path  { get => _saveLocation + _saveFilename; }

    ChildProfile _curentProfile;
    public void Save(int questNumber, QuestStates state)
    {
        _curentProfile.QuestNumber = questNumber;
        _curentProfile.State = state;
        SaveToFile();       
    }    

    public List<ChildProfile> GetProfiles()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(_path, FileMode.Open))
            {
                _profilesWrapper = (ChildProfilesWrapper)formatter.Deserialize(stream);                
            }            
        }
        else
        {
            File.Create(_path).Dispose();            
        }

        return _profilesWrapper.List;
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

        if(_profilesWrapper.List.Where(c => c.Patronymic == profile.Patronymic && c.Name == profile.Name && c.Surname == profile.Surname && c.Age == age).Any())
        {
            throw new Exception("Нельзя добавить одного человека два раза");
        }

        _profilesWrapper.List.Add(profile);
        SaveToFile();

        return profile;
    }

    private void SaveToFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(_path, FileMode.Create))
        {
            formatter.Serialize(stream, _profilesWrapper);
        }
    }

    public void SetProfile(ChildProfile profile)
    {
        _curentProfile = profile;
        Debug.Log("Profile set");
    }
}