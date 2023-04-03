using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoad : MonoBehaviour
{
    private string ability_file = Application.persistentDataPath + "/abilitydata.json";

    public void SaveAbilityData(Abilities data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(ability_file, json);
    }

    public Abilities LoadAbilityData() {
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

[System.Serializable]
public class Abilities
{
    private bool time_warp;
    private bool double_jump;
    private bool slide;

    public Abilities(bool time_warp, bool double_jump, bool slide)
    {
        this.time_warp = time_warp;
        this.double_jump = double_jump;
        this.slide = slide;
    }

    public void set_time_warp(bool new_value)
    {
        this.time_warp = new_value;
    }

    public void set_double_jump(bool new_value)
    {
        this.double_jump = new_value;
    }

    public void set_slide(bool new_value)
    {
        this.slide = new_value;
    }

    public bool get_time_warp()
    {
        return this.time_warp;
    }

    public bool get_double_jump()
    {
        return this.double_jump;
    }

    public bool get_slide()
    {
        return this.slide;
    }
}