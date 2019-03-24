using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using UnityEngine;



public class knnRecord : MonoBehaviour {

    public int numberOfShoots; //player
    public int numberOfHits; //enemys
    public int numberOfBoxes;   //destructibles
    public int hpLost;      //player
    public int heal;        //player
    public int seconds;     //mapConfig
    public int distance; // enemys
    public int firstSkill;
    public int mostUsedSkill;
    public List<float> distanceOfEnemys = new List<float>();
    public int distanceInRoom;
    public float distanceAux;

    public bool knnAtivar = false;
    float timer = 0;
    public bool activeTimer = false;
    public bool blockKnn = false;

    float[] knn = new float[10];

    GameObject obj;

    FileMaker file = new FileMaker();
    MySqlDb insert = new MySqlDb();

    public int totalEnergy = 0;
    public int collectedEnergy = 0;


    //string conn = "URI=file:" + Application.dataPath + "/blitzDB.s3db"; //Path to database.



    // Use this for initialization
    void Start () {

        obj = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        knn[2] = numberOfBoxes;
        knn[9] = firstSkill;
        

        //salva os valores quando a porta da sala abre
        if (knnAtivar) //ligado no warp
        {
            knn[0] = numberOfShoots;
            knn[1] = numberOfHits;
            
            knn[3] = hpLost;
            knn[4] = heal;
            knn[5] = seconds;




            if (activeTimer && obj.GetComponent<Player>().currentMap.GetComponent<MapConfig>().clear == false)
            {
                timer += Time.deltaTime;
                seconds = Mathf.RoundToInt(timer);
                knn[8] = obj.GetComponent<Player>().shootID;
            }
        }
        else
        {
            if (obj.GetComponent<Player>().recordKnn && !blockKnn)
            {
                //dataBase.InsertDB("classe", numberOfShoots, numberOfHits, numberOfBoxes, hpLost, heal, seconds);

                foreach (float d in distanceOfEnemys)
                {
                    distanceAux += d;
                }
                float aux;
                aux = (distanceAux / distanceOfEnemys.Count);              
                knn[6] = Mathf.RoundToInt(aux);

                //calcula porcentagem de energia coletada
                aux = (collectedEnergy * 100f) / totalEnergy;
                if (aux > 100) aux = 100;
                knn[7] = aux;

                //int skillAux = obj.GetComponent<Player>().arrayMostUsedSkill[0];
                //for (int i = 0 ; i< obj.GetComponent<Player>().arrayMostUsedSkill.Length ; i++)


                file.WriteFile(knn);
                insert.InsertKnnData(knn, distanceAux);
                //gravaKnnData(knn);

                //reseta os valores para nova captura
                numberOfShoots = 0;
                numberOfHits = 0;
                numberOfBoxes = 0;
                hpLost = 0;
                heal = 0;
                seconds = 0;
                timer = 0;
                distance = 0;
                distanceOfEnemys.Clear();
                distanceAux = 0;
                collectedEnergy = 0;
                totalEnergy = 0;

                obj.GetComponent<Player>().recordKnn = false;
                blockKnn = true;
            }

        }





    }


    //public void gravaKnnData(float[] knn)
    //{


    //    IDbConnection dbconn;
    //    dbconn = (IDbConnection)new SqliteConnection(conn);
    //    dbconn.Open(); //Open connection to the database.
    //    IDbCommand dbcmd = dbconn.CreateCommand();


    //    String query = "INSERT INTO player_knn_data (player_name, number_of_shoots, number_of_hits, hp_lost, heal, seconds) VALUES ($1, $2, $3, $4, $5, $6)";


    //    string sql = "insert into table1 (column2) values ($1)";
    //    dbcmd.CommandText = sql;

    //    IDbDataParameter param1 = dbcmd.CreateParameter();
    //    param1.DbType = DbType.AnsiString;
    //    param1.ParameterName = "1";
    //    param1.Value = "PlayerTest";
    //    dbcmd.Parameters.Add(param1);


    //    IDbDataParameter param2 = dbcmd.CreateParameter();
    //    param2.DbType = DbType.VarNumeric;
    //    param2.ParameterName = "2";
    //    param2.Value = knn[0];
    //    dbcmd.Parameters.Add(param2);



    //    IDbDataParameter param3 = dbcmd.CreateParameter();
    //    param3.DbType = DbType.VarNumeric;
    //    param3.ParameterName = "3";
    //    param3.Value = knn[1];
    //    dbcmd.Parameters.Add(param3);


    //    IDbDataParameter param4 = dbcmd.CreateParameter();
    //    param4.DbType = DbType.VarNumeric;
    //    param4.ParameterName = "4";
    //    param4.Value = knn[3];
    //    dbcmd.Parameters.Add(param4);


    //    IDbDataParameter param5 = dbcmd.CreateParameter();
    //    param5.DbType = DbType.VarNumeric;
    //    param5.ParameterName = "5";
    //    param5.Value = knn[4];
    //    dbcmd.Parameters.Add(param5);



    //    IDbDataParameter param6 = dbcmd.CreateParameter();
    //    param6.DbType = DbType.VarNumeric;
    //    param6.ParameterName = "6";
    //    param6.Value = knn[5];
    //    dbcmd.Parameters.Add(param6);

    //    dbcmd.ExecuteNonQuery();

    //    dbcmd.Dispose();
    //    dbcmd = null;
    //    dbconn.Close();
    //    dbconn = null;


    //}
}
