using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    Transform target;
    Vector3 direction;
    public float speed;

    public float damage = 1;
    public float delay = .3f;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Aim").transform;

        direction = (target.position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(direction * speed * Time.deltaTime);
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.SendMessage("takeDamage", damage);
        }

        if(collision.tag != "Player") Destroy(this.gameObject);
    }
}
