using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sortingLayerName = "Door";
        }


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            anim.SetTrigger("hit");
        }
    }
}
