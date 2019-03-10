using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchControl : MonoBehaviour {

    public GameObject welcomeMessage;
    public GameObject firstResearch;
    public GameObject finalResearch;

	// Use this for initialization
	void Start () {
        welcomeMessage.SetActive(true);
        firstResearch.SetActive(false);
        finalResearch.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("Submit"))
        //{
        //    EnterResearch();
       // }
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
}
