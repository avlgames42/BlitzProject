using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDb : MonoBehaviour

{
    string urlPost = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertPlayerInfo.php";

     public void Start()
    {
    }


    public void Insert()
    {
        GameObject research = GameObject.Find("Canvas");
        ResearchControl researchScript = research.GetComponent<ResearchControl>();

        InsertPlayerInfo(researchScript.jogador);
    }


    public void InsertPlayerInfo(string[] jogador)
    {
        print("InsertPlayerInfo");
        int playGames;



        if (jogador[5] == "SIM")
        {
            playGames = 1;
        }
        else
        {
            playGames = 0;
        }


        WWWForm form = new WWWForm();
        form.AddField("playerNamePost", jogador[0]);
        form.AddField("genderPost", jogador[2]);
        form.AddField("agePost", jogador[1]);
        form.AddField("educationPost", jogador[3]);
        form.AddField("cityPost", jogador[4]);
        form.AddField("playGamesPost", playGames);

        WWW www = new WWW(urlPost, form );

    }
}
