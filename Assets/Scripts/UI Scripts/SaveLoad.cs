using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoad : MonoBehaviour
{
    public static void SaveData<T>(T data, string path)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, json);
    }

    public static T LoadAbilityData<T>(string path) where T : new() {
        T data;
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<T>(json);
        } else
        {
            data = new T();
        }

        return data;
    }
}

