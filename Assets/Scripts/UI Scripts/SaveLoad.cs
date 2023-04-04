using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoad : MonoBehaviour
{
    private static string ability_file = Application.persistentDataPath + "/abilitydata.json";

    public static void SaveAbilityData(Abilities data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(ability_file, json);
    }

    public static Abilities LoadAbilityData() {
        Abilities data;
        
        if (File.Exists(ability_file))
        {
            string json = File.ReadAllText(ability_file);
            data = JsonConvert.DeserializeObject<Abilities>(json);
        } else
        {
            data = new Abilities(false, false, false);
        }

        return data;
    }
}

