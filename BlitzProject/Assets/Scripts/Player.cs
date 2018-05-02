using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

    Animator anim;
    Vector2 movement;
    public float speed;

    public GameObject initialMap;
    public GameObject currentMap;

    public GameObject aim;
    Vector2 aimDirection;

    public GameObject shoot;


    private void Awake()
    {
        //Assert.IsNotNull(initialMap);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        //passa o mapa inicial para a referencia da camera
        Camera.main.GetComponent<MainCamera>().setBounds(initialMap);
	}
	
	// Update is called once per frame
	void Update () {

        aimDirection = new Vector3(Input.GetAxisRaw("HorizontalDireito"), Input.GetAxisRaw("VerticalDireito"));
        aim.transform.localPosition = aimDirection;

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        walk(movement);

        if (Input.GetAxisRaw("HorizontalDireito") != 0 || Input.GetAxisRaw("VerticalDireito") != 0)
        {
            fire();
        }


    }

    void walk(Vector2 movement)
    {
        transform.Translate(movement * speed * Time.deltaTime);

        if (Input.GetAxisRaw("HorizontalDireito") != 0 || Input.GetAxisRaw("VerticalDireito") != 0)
        {
            anim.SetFloat("movX", aimDirection.x);
            anim.SetFloat("movY", aimDirection.y);
            anim.SetBool("walking", true);
        }
        else if (movement != Vector2.zero)
        {
            anim.SetFloat("movX", movement.x);
            anim.SetFloat("movY", movement.y);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    void fire()
    {
        Instantiate(shoot, transform.position, transform.rotation);
    }
}
