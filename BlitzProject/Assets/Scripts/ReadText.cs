using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadText : MonoBehaviour {

    public TextAsset file;
    public GameObject text;
    public string[] texto;

    public int fimDaLinha;
    public int linhaAtual;
    // Use this for initialization
    void Start() {
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
        text.GetComponent<Text>().text = texto[linhaAtual];
    }



    public void Answer(int resp)
    {
        linhaAtual++;
        if (linhaAtual >= fimDaLinha)
        {
            //configurar para agradecimento e ir para o menu principal
            linhaAtual = 0;
        }
    }

}

