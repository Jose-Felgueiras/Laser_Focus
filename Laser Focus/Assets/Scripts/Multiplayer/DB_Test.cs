using UnityEngine;
//using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DB_Test : MonoBehaviour
{


    //void ReadDatabase()
    //{
    //    //Search for database in assets folder
    //    string conn = "URI=file:" + Application.dataPath + "/SQL_Lite_Database.db";

    //    //Open Connection to the database
    //    IDbConnection dbconn;
    //    //dbconn = (IDbConnection)new SqliteConnection(conn);
    //    //dbconn.Open();

    //    //Create a command 
    //    //IDbCommand dbcmd = dbconn.CreateCommand();

    //    //Commanda using SQL 
    //    string sqlQuery = "SELECT id, username " + "FROM user";
    //    dbcmd.CommandText = sqlQuery;

    //    //Execute Command
    //    IDataReader reader = dbcmd.ExecuteReader();
    //    while (reader.Read())
    //    {
    //        int id = reader.GetInt32(0);
    //        string username = reader.GetString(1);


    //        Debug.LogFormat("ID = {0}; UserName = {1}", id, username);
    //        //DO SOMETHING WITH DATA
    //    }

    //    //Close everything
    //    reader.Close();
    //    reader = null;
    //    dbcmd.Dispose();
    //    dbcmd = null;
    //    dbconn.Close();
    //    dbconn = null;
    //}
    //void AddUser(string username)
    //{
    //    string conn = "URI=file:" + Application.dataPath + "/SQL_Lite_Database.db";
    //    //Open Connection to the database
    //    IDbConnection dbconn;
    //    //dbconn = (IDbConnection)new SqliteConnection(conn);
    //    //dbconn.Open();

    //    //Create a command 
    //    IDbCommand dbcmd = dbconn.CreateCommand();

    //    //Commanda using SQL 
    //    string sqlQuery = "INSERT INTO user (username) VALUES ('" + username + "')";
    //    dbcmd.CommandText = sqlQuery;

    //    //Execute Command
    //    IDataReader reader = dbcmd.ExecuteReader();

    //    //Close everything 
    //    reader.Close();
    //    reader = null;
    //    dbcmd.Dispose();
    //    dbcmd = null;
    //    dbconn.Close();
    //    dbconn = null;
    //}



}
