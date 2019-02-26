using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadText : MonoBehaviour {

    public TextAsset file;
    public GameObject text;
    public string[] texto;

    int fimDaLinha;
    int linhaAtual;
	// Use this for initialization
	void Start () {
		if(file != null)
        {
            texto = (file.text.Split('\n'));
        }
        if(fimDaLinha == 0)
        {
            fimDaLinha = texto.Length;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if(linhaAtual < fimDaLinha)
            {
                text.GetComponent<Text>().text = texto[linhaAtual];
            }
            linhaAtual++;
            if(linhaAtual >= fimDaLinha)
            {
                linhaAtual = 0;
            }
        }
	}
}
