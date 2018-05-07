using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig : MonoBehaviour {

    public bool peek = false;
    public bool clear = false;
    public bool active = false;
    public bool populated = false;

    //public List<GameObject> listDoors = new List<GameObject>();
    public int enemysLeft;

    public GameObject doors;

    public List<GameObject> enemySpot = new List<GameObject>();
    GameObject gm;

    int aux;

    int[] check = new int[10] {0,0,0,0,0,0,0,0,0,0};

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("Manager");

	}
	
	// Update is called once per frame
	void Update () {



        //trava as portas quando o mapa for ativo
        if (active && !clear)
        {
            doors.gameObject.SetActive(true);
            if(!populated && enemysLeft > 0) populateRoom();
            //Instantiate(gm.GetComponent<GameManager>().listEnemysLv1[0], enemySpot[1].transform.position, enemySpot[1].transform.rotation);
        }

        //liberaas portas quando todos inimigos forem derrotados
        if (clear)
        {
            doors.gameObject.SetActive(false);
        }

        if (enemysLeft <= 0)
        {
            enemysLeft = 0;
            clear = true;
        }
    }

    public void defeatEnemy()
    {
        enemysLeft--;

    }

    void populateRoom()
    {
        //sorteia onde o inimgo sera instanciado
        for(int j = 0;j < enemysLeft; j++)
        {
            sorteio:
            aux = Mathf.RoundToInt((Random.Range(0, enemySpot.Count) * 100) / 100);

            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] == aux)
                {
                    goto sorteio;
                }
            }
            //inseri valor no vetor
            check[j] = aux;
            print(aux.ToString());
            //sorteia inimigo
            aux = Mathf.RoundToInt((Random.Range(0, gm.GetComponent<GameManager>().listEnemysLv1.Count) * 100) / 100);

            Instantiate(gm.GetComponent<GameManager>().listEnemysLv1[aux], enemySpot[check[j]].transform.position, enemySpot[check[j]].transform.rotation);

        }
        populated = true;
        

    }


}
