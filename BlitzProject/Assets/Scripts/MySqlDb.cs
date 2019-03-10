using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDb : MonoBehaviour {


    string source;
    MySqlConnection conn;

    // Use this for initialization
    void Start () {
        source = "Server=localhost;Database=blitzproject;User Id=root;Password=Ogpcae123";
        //DbConnection(source);
        //DbInsert(conn);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DbConnection(string _source) 
    {
        conn = new MySqlConnection(_source);
        conn.Open ();
    }

    void DbInsert(MySqlConnection _conn)
    {
        MySqlCommand command = _conn.CreateCommand();
        command.CommandText = "INSERT INTO jogador (nome) VALUES ('Luis')";
        command.ExecuteNonQuery();
        _conn.Close();
    }
}
