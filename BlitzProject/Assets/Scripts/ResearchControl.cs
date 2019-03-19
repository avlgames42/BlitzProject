using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchControl : MonoBehaviour {

    public GameObject welcomeMessage;
    public GameObject firstResearch;
    public GameObject finalResearch;
    public string[] jogador = new string[7];

    public GameObject jogadorName;
    public GameObject jogadorIdade;
    public GameObject jogadorGenero;
    public GameObject jogadorFormacao;
    public GameObject jogadorCidade;
    public GameObject jogadorJoga;
    public GameObject jogadorTempo;
    public GameObject btnEnviar;
    public GameObject formacaoInput;
    public GameObject cidadeInput;

    public int[] aux = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] auxGenero;
    public int count = 0;


	// Use this for initialization
	void Start () {
        welcomeMessage.SetActive(true);
        firstResearch.SetActive(false);
        finalResearch.SetActive(false);
        formacaoInput.SetActive(false);
        cidadeInput.SetActive(false);
        jogadorTempo.SetActive(false);
        btnEnviar.SetActive(false);
        auxGenero = new int[jogadorGenero.transform.childCount];
	}
	
	// Update is called once per frame
	void Update () {
        if(aux[4] == 1)
        {
            for (int i = 0; i < aux.Length; i++)
            {
                count += aux[i];
            }
            if(count == 6 && jogador[5] == "NÃO") btnEnviar.SetActive(true);
            if (count == 7 && jogador[5] == "SIM") btnEnviar.SetActive(true);
            count = 0;
        }

	}

    public void EnterResearch()
    {
        welcomeMessage.SetActive(false);
        firstResearch.SetActive(true);
    }

    public void EnterFinalResearch()
    {
        firstResearch.SetActive(false);
        finalResearch.SetActive(true);
    }
    
    public void SetName()
    {
        jogador[0] = jogadorName.GetComponent<InputField>().text;
        aux[0] = 1;
    }

    public void SetAge()
    {
        jogador[1] = jogadorIdade.GetComponent<InputField>().text;
        aux[1] = 1;
    }

    public void SetGenero(int index)
    {       
        string genero = jogadorGenero.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;

        if (genero == "Feminino")
            jogador[2] = "F";
        else if (genero == "Masculino")
            jogador[2] = "M";
        else
            jogador[2] = "N";
     
        aux[2] = 1;
    }

    public void SetFormacao(int index)
    {
        if(index == 4)
        {
            formacaoInput.SetActive(true);
        }
        else
        {
            jogador[3] = jogadorFormacao.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            aux[3] = 1;
            formacaoInput.GetComponent<InputField>().text = "";
            formacaoInput.SetActive(false);
        }
        
    }

    public void SetCidade(int index)
    {
        if (index == 4)
        {
            cidadeInput.SetActive(true);
        }
        else
        {
            jogador[4] = jogadorCidade.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            aux[4] = 1;
            cidadeInput.GetComponent<InputField>().text = "";
            cidadeInput.SetActive(false);
        }
        
    }

    public void SetPlay(int index)
    {
        if(index == 0)
        {
            jogadorTempo.SetActive(true);
            jogador[5] = jogadorJoga.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            jogador[6] = jogadorTempo.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            aux[5] = 1;
            aux[6] = 0;
            btnEnviar.SetActive(false);
        }
        else
        {
            jogador[5] = jogadorJoga.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
            jogador[6] = "";
            aux[5] = 1;
            jogadorTempo.SetActive(false);
        }
        
    }

    public void SetPlayTime(int index)
    {
        jogador[6] = jogadorTempo.transform.GetChild(index).gameObject.transform.GetChild(1).GetComponent<Text>().text;
        aux[6] = 1;
    }

    public void SetOtherCidade()
    {
        jogador[4] = cidadeInput.GetComponent<InputField>().text;
        aux[4] = 1;
    }

    public void SetOtherFormacao()
    {
        jogador[3] = formacaoInput.GetComponent<InputField>().text;
        aux[3] = 1;
    }
}
