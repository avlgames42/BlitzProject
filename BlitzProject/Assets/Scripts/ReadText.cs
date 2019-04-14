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
    // public string[] texto;

    string[] texto;


    //TextAsset file = Resources.Load("pesquisa") as TextAsset;

    int cdPlayer;


    public int fimDaLinha;
    int linhaAtual = 0;

    bool ok = false;

    int[] playerAnswer = new int[28];



    string urlPost = "http://ec2-54-69-94-4.us-west-2.compute.amazonaws.com/InsertPlayerQuestions.php";

    // Use this for initialization
    void Start() {
        btnClose.SetActive(false);
        panelQuestions.SetActive(true);
        message.SetActive(false);


        texto = new string[] {
                    "Do you use very often the communication means of the games (written or voice chat, emotes, etc) ?",
                    "Your favorite games are the genre RPG, fiction or alternate reality ?",
                    "Do you usually buy or unlock expensive skin or more coveted ?",
                    "Do you play the games that are release, beta or early access ?",
                    "Do you finish the games that you like ?",
                    "Do the stories of the games can change your feelings ?",
                    "Do you stay motivated to play the same game with several consecutive defeats ?",
                    "Do you often discuss with other players or makes provocations ?",
                    "In a competitive team play, do you feel that most of the time partners more confuse than help you ?",
                    "Do you have reserved hours just to play ?",
                    "Do you consider yourself a skilled player ?",
                    "To get frustrated with a game you tend to abandon it or criticize ?",
                    "When you want to accomplish a goal in a game, do you have tendency to explore the deficiency or balance of the game ?",
                    "In team games do you try to take the leadership ?",
                    "Do you usually play with caution when begins a new phase in the games ?",
                    "Do you complete the game's  tutorial?",
                    "The arts and graphics are crucial for you to play a game ?",
                    "Do you share with others your game techniques ?",
                    "Do you feel uncomfortable when your performance is compared with other players ?",
                    "Do you prefer to play alone instead of playing in a group ?",
                    "Do you prefer strategy games than action games ?",
                    "Are you patient with bugs or crashes in games ?",
                    "Are you usually patient with critical or attacks from other players ?",
                    "Do you prefer casual games?",
                    "Do you usually leave the game inventory disorganized?",
                    "In a game, do you prioritize the fun than the competition ?",
                    "Your favorite games are the classic and / or launched a few years ago ?",
                    "Do you prefer the default skin of a character?"
        };

}

    // Update is called once per frame
    void Update() {

  
        if (linhaAtual < playerAnswer.Length)
        {

            text.GetComponent<Text>().text = texto[linhaAtual];
        }
       
        if (linhaAtual > 27)
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

