using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig : MonoBehaviour {

    public bool peek = false;
    public bool clear = false;
    public bool active = false;

    public List<GameObject> listDoors = new List<GameObject>();
    public int enemysLeft;

    public GameObject doors;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //trava as portas quando o mapa for ativo
        if (active && !clear && enemysLeft > 0)
        {
            doors.gameObject.SetActive(true);
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

    private void OnBecameVisible()
    {
        if(!active) active = true;
        print("ativo");
    }

    public void defeatEnemy()
    {
        enemysLeft--;

    }

}
