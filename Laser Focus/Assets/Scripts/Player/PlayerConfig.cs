using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class PlayerConfig
{

    private static int ID = -1;
    private static string username = "null";


    private static string savePath = Application.persistentDataPath + "/PlayerSave.json";

    public static void CreateJSON()
    {
        if (!File.Exists(savePath))
        {
            JSONObject playerJSON = new JSONObject();
            playerJSON.Add("ID", ID);
            playerJSON.Add("Username", username);

            File.WriteAllText(savePath, playerJSON.ToString());
        }
    }

    public static void Save()
    {
        JSONObject playerJSON = new JSONObject();
        playerJSON.Add("ID", ID);
        playerJSON.Add("Username", username);

        File.WriteAllText(savePath, playerJSON.ToString());

        
    }

    public static void Load()
    {
        string JSONstring = File.ReadAllText(savePath);
        JSONObject playerJSON = (JSONObject)JSON.Parse(JSONstring);

        ID = playerJSON["ID"];
        username = playerJSON["Username"];
    }

    public static void SetID(int id)
    {
        Load();
        ID = id;
        Save();
    }

    public static void SetUsername(string name)
    {
        Load();
        username = name;
        Save();
    }

    public static int GetPlayerID()
    {
        string JSONstring = File.ReadAllText(savePath);
        JSONObject playerJSON = (JSONObject)JSON.Parse(JSONstring);
        if (playerJSON["ID"] != null)
        {
            ID = playerJSON["ID"];
        }
        return ID;
    }
    public static string GetPlayerUsername()
    {
        string JSONstring = File.ReadAllText(savePath);
        JSONObject playerJSON = (JSONObject)JSON.Parse(JSONstring);

        if (playerJSON["Username"] != null)
        {
            username = playerJSON["Username"];
        }
        return username;
    }
}
