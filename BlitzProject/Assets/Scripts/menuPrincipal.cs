using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPrincipal : MonoBehaviour {

    public GameObject menu;
    public GameObject controle;
    public GameObject sobre;
    public GameObject creditos;
    public GameObject idioma;
    public GameObject options;
    public GameObject selector;
    public GameObject music;
    public GameObject volume;
    public GameObject effects;
    GameObject list;
    int indice = 0;
    bool locked = false;
    bool lockedH = false;
    string state = "menu";

    public static float globalVolume = .5f;
    public static int globalMusic = 0;


	// Use this for initialization
	void Start () {
        //options.SetActive(false);
        clearScreen();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (state.Equals("menu")) list = menu;
        if (state.Equals("options")) list = options.transform.GetChild(0).gameObject;

        globalVolume = volume.GetComponent<Slider>().value;

        //navegação no menu 
        if (Input.GetAxis("Vertical") < 0 && !locked)
        {
            locked = true;
            indice++;
            if (indice > list.transform.childCount - 1) indice = 0;
            selection();
        }
        else if (Input.GetAxis("Vertical") > 0 && !locked)
        {
            locked = true;
            indice--;
            if (indice < 0) indice = list.transform.childCount - 1;
            selection();
        }
        
        //libera a trava dos eixos para seleção das opções
        if(Input.GetAxis("Vertical") == 0)
        {
            locked = false;
        }

        if (Input.GetAxis("Horizontal") == 0 && (state.Equals("music") || state.Equals("idioma")))
        {
            lockedH = false;
        }


        //navegacao no opcao musica do menu de opcoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("music") && !lockedH)
        {
            lockedH = true;
            indice++;
            if(indice > music.transform.childCount -1)
            {
                indice = music.transform.childCount - 1;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        else if (Input.GetAxis("Horizontal") < 0 && state.Equals("music") && !lockedH)
        {
            lockedH = true;
            indice--;
            if (indice < 0)
            {
                state = "options";
                indice = 0;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }

        if (state.Equals("music") && Input.GetButtonDown("A"))
        {
            globalMusic = indice;
            music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", false);
            music.transform.GetChild(indice).gameObject.GetComponent<Animator>().SetBool("select", true);
        }

        //navegacao no opcao idioma do menu de opcoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("idioma") && !lockedH)
        {
            lockedH = true;
            indice++;
            if (indice > idioma.transform.childCount - 1)
            {
                indice = idioma.transform.childCount - 1;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        else if (Input.GetAxis("Horizontal") < 0 && state.Equals("idioma") && !lockedH)
        {
            lockedH = true;
            indice--;
            if (indice < 0)
            {
                state = "options";
                list = options.transform.GetChild(0).gameObject;
                indice = 3;
            }
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }

        //seleciona as sublistas dentro do menu opçoes
        if (Input.GetAxis("Horizontal") > 0 && state.Equals("options"))
        {
            if(indice == 0)
            {
                list = music;
                indice = 0;
                selector.transform.position = list.transform.GetChild(indice).transform.position;
                state = "music";
                lockedH = true;
            }
            if(indice == 1)
            {
                volume.GetComponent<Slider>().value += 0.01f;
            }
            if(indice == 2)
            {
                effects.GetComponent<Slider>().value += 0.01f;
            }
            if (indice == 3)
            {
                list = idioma;
                indice = 0;
                selector.transform.position = list.transform.GetChild(indice).transform.position;
                state = "idioma";
                lockedH = true;
            }

        }

        //Controla o slider de volume no menu de opções
        if (Input.GetAxis("Horizontal") < 0 && state.Equals("options"))
        {
            if (indice == 1)
            {
                volume.GetComponent<Slider>().value -= 0.01f;
                
            }
            if (indice == 2)
            {
                effects.GetComponent<Slider>().value -= 0.01f;
            }
        }

        //controla a seleção das opções do menu
        if (Input.GetButtonDown("A") && state.Equals("menu"))
        {
            switch (indice)
            {
                case 0:
                    loadScene("Floor_1");
                    break;
                case 1:
                    indice = 0;
                    state = "options";
                    clearScreen();
                    options.SetActive(true);
                    if(globalMusic == 0)
                    {
                        music.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if (globalMusic == 1)
                    {
                        music.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }
                    if (globalMusic == 2)
                    {
                        music.transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("select", true);
                    }

                    break;
                case 2:
                    clearScreen();
                    creditos.SetActive(true);
                    state = "others";
                    break;
                case 3:
                    clearScreen();
                    controle.SetActive(true);
                    state = "others";
                    break;
                case 4:
                    clearScreen();
                    sobre.SetActive(true);
                    state = "others";
                    break;
                case 5:
                    Application.Quit();
                    break;

            }
        }

        //retorna do menu opções para o menu principal
        if (Input.GetButtonDown("B") && (state.Equals("options") || state.Equals("music") || state.Equals("idioma") || state.Equals("others")))
        {
            clearScreen();
            menu.SetActive(true);
            state = "menu";
            indice = 0;
        }
        
    }

    //realiza o controle de animações dos botões do menu principal
    public void selection()
    {
        if (state.Equals("menu"))
        {
            for (int i = 0; i < list.transform.childCount; i++)
            {
                list.transform.GetChild(i).GetComponent<Animator>().SetBool("hover", false);
            }

            list.transform.GetChild(indice).GetComponent<Animator>().SetBool("hover", true);
        }
        else if (state.Equals("options"))
        {
            selector.transform.position = list.transform.GetChild(indice).transform.position;
        }
        
    }

    void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void clearScreen()
    {
        menu.SetActive(false);
        options.SetActive(false);
        creditos.SetActive(false);
        sobre.SetActive(false);
        controle.SetActive(false);
    }
}
