using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadText : MonoBehaviour {

    public TextAsset file;
    public GameObject text;
    public GameObject btnClose;
    public GameObject panelQuestions;
    public GameObject message;
    public string[] texto;

    int cdPlayer;


    public int fimDaLinha;
    public int linhaAtual;

    bool ok = false;

    public int[] playerAnswer = new int[27];

    string urlPost = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertPlayerQuestions.php";

    // Use this for initialization
    void Start() {
        btnClose.SetActive(false);
        panelQuestions.SetActive(true);
        message.SetActive(false);
        if (file != null)
        {
            texto = (file.text.Split('\n'));
        }
        if (fimDaLinha == 0)
        {
            fimDaLinha = texto.Length;
        }


    }

    // Update is called once per frame
    void Update() {
        if(linhaAtual < playerAnswer.Length) text.GetComponent<Text>().text = texto[linhaAtual];
        if(linhaAtual >= 27)
        {
            btnClose.SetActive(true);
            panelQuestions.SetActive(false);
            message.SetActive(true);
        }
    }





    public void Answer(int resp)
    {
        //guarda a resposta no vetor
        playerAnswer[linhaAtual] = resp;

        GameObject user = GameObject.Find("GetUser");
        CdPlayer getUser = user.GetComponent<CdPlayer>();

 
        cdPlayer = getUser.cdPlayer;

        WWWForm form = new WWWForm();

        form.AddField("cdPlayerPost", cdPlayer);
        form.AddField("questionPost", linhaAtual);
        form.AddField("answerPost", resp);

        WWW www = new WWW(urlPost, form);


        linhaAtual++;
    }




    public void LoadScene()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }


        
}

