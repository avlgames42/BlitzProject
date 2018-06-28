using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knnRecord : MonoBehaviour {

    public int numberOfShoots; //player
    public int numberOfHits; //enemys
    public int numberOfBoxes;   //destructibles
    public int hpLost;      //player
    public int heal;        //player
    public int seconds;     //mapConfig
    public int distanceOfEnemys;
    public int distanceInRoom;
    

    public bool knnAtivar = false;

    int[ , ] knn = new int[10,5];
    int[] matriz = new int[10];
    int indice = 0;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //salva os valores quando a porta da sala abre
        if (knnAtivar)
        {
            knn[indice,0] = numberOfShoots;
            knn[indice,1] = numberOfHits;
            knn[indice,2] = numberOfBoxes;
            knn[indice,3] = hpLost;
            knn[indice,4] = heal;

            knnAtivar = false;
            indice++;

        }
	}
}
