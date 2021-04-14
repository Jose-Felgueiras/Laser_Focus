using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public static class PlayerConfig
{

    private static int ID = -1;
    private static string username = "null";

    private static int selectedDeck = 0;

    private static int[] deck1 = new int[8];
    private static int[] deck2 = new int[8];
    private static int[] deck3 = new int[8];


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
        playerJSON.Add("DeckID", selectedDeck);


        JSONNode node = new JSONArray();
        for (int i = 0; i < deck1.Length; i++)
        {
            node.Add(deck1[i]);
        }
        playerJSON.Add("Deck1", node);

        JSONNode node1 = new JSONArray();
        for (int i = 0; i < deck2.Length; i++)
        {
            node1.Add(deck2[i]);
        }
        playerJSON.Add("Deck2", node1);

        JSONNode node2 = new JSONArray();
        for (int i = 0; i < deck3.Length; i++)
        {
            node2.Add(deck3[i]);
        }
        playerJSON.Add("Deck3", node2);

        File.WriteAllText(savePath, playerJSON.ToString());
    }

    public static void Load()
    {
        string JSONstring = File.ReadAllText(savePath);
        JSONObject playerJSON = (JSONObject)JSON.Parse(JSONstring);

        ID = playerJSON["ID"];
        username = playerJSON["Username"];
        selectedDeck = playerJSON["DeckID"];

        JSONNode node = new JSONArray();
        node = playerJSON["Deck1"];
        for (int i = 0; i < deck1.Length; i++)
        {
            deck1[i] = node[i];
        }

        JSONNode node1 = new JSONArray();
        node1 = playerJSON["Deck2"];
        for (int i = 0; i < deck2.Length; i++)
        {
            deck2[i] = node1[i];
        }

        JSONNode node2 = new JSONArray();
        node2 = playerJSON["Deck3"];
        for (int i = 0; i < deck3.Length; i++)
        {
            deck3[i] = node2[i];
        }
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
    public static void SetDeck(int deckID, int[] deckTowers)
    {
        Load();
        switch (deckID)
        {
            case 0:
                deck1 = deckTowers;
                break;
            case 1:
                deck2 = deckTowers;
                break;
            case 2:
                deck3 = deckTowers;
                break;
            default:
                break;
        }
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
    public static int[] GetDeck(int deckID)
    {
        string JSONstring = File.ReadAllText(savePath);
        JSONObject playerJSON = (JSONObject)JSON.Parse(JSONstring);

        switch (deckID)
        {
            case 0:
                if (playerJSON["Deck1"] != null)
                {
                    JSONNode node = new JSONArray();
                    node = playerJSON["Deck1"];

                    for (int i = 0; i < deck1.Length; i++)
                    {
                        deck1[i] = node[i];
                    }
                }
                return deck1;
            case 1:
                if (playerJSON["Deck2"] != null)
                {
                    JSONNode node = new JSONArray();
                    node = playerJSON["Deck2"];

                    for (int i = 0; i < deck2.Length; i++)
                    {
                        deck2[i] = node[i];
                    }
                }
                return deck2;
            case 2:
                if (playerJSON["Deck3"] != null)
                {
                    JSONNode node = new JSONArray();
                    node = playerJSON["Deck3"];

                    for (int i = 0; i < deck3.Length; i++)
                    {
                        deck3[i] = node[i];
                    }
                }
                return deck3;
            default:
                return null;
        }

        
        
    }
    public static int GetSelectedDeck()
    {
        return selectedDeck;
    }
    public static void SetSelectedDeck(int deckID)
    {
        selectedDeck = deckID;
        Save();
    }
}
