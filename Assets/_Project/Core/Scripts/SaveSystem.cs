using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

class SaveSystem
{
    public void Save(int questNumber)
    {
        // todo
        Debug.Log(questNumber);
    }

    public List<ChildProfile> GetProfiles()
    {
        return new List<ChildProfile>();
    }

    internal void SaveProfile(string surname, string name, string patronymic, string age)
    {
        throw new NotImplementedException();
    }
}