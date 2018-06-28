using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<GameObject> listEnemysLv1 = new List<GameObject>();
    public GameObject gameOver;
    public GameObject win;
    public string gameState = "play";

    FloorGenerator script;

    public static int enemysTotal = 0;

    public GameObject energy;
    public GameObject heal;

    // variaveis para controle de salas

    // Use this for initialization
    void Start () {

        gameOver.SetActive(false);
        win.SetActive(false);
        GameObject obj = GameObject.Find("FloorGeneratorObj");
        script = obj.GetComponent<FloorGenerator>();
        
        foreach(GameObject floor in script.floor)
        {
            enemysTotal += floor.GetComponent<MapConfig>().enemysLeft;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if(enemysTotal <= 0)
        {
            gameState = "win";
        }

        if(Input.GetButtonDown("A") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("Floor_1");
        }

        if (Input.GetButtonDown("B") && gameState.Equals("gameover"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

        if (gameState.Equals("win"))
        {
            win.SetActive(true);
        }

        //print(enemysTotal.ToString());

    }

    

    void showGameOver()
    {
        gameState = "gameover";
        gameOver.SetActive(true);
    }

 

}
