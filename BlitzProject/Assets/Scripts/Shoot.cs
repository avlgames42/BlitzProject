using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    Transform target;
    Vector3 direction;
    public float speed;

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
}
