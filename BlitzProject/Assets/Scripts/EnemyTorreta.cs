﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTorreta : MonoBehaviour {

    Vector3 target;
    Vector3 direction;
    GameObject player;
    GameObject gm;
    Animator anim;
    Vector3 initialPosition;
    public GameObject shoot;
    public bool horizontal;
    public float AttackRadius;
    public float delay;
    float distance;

    float hp = 0;
    public float hpMax;
    float attackDamage = 1;

    bool isAttack = false;
    bool isActive = false;
    bool dead = false;
    bool ready = false;

    RaycastHit2D hit;

    public int xp;
    public GameObject energy;
    public GameObject lifeBar;

    public AudioClip hitSound;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("Manager");
        anim = GetComponent<Animator>();
        hp = hpMax;
    }
	
	// Update is called once per frame
	void Update () {

        if (gm.GetComponent<GameManager>().gameState.Equals("play"))
        {
            if (isActive && ready)
            {
                lifeBar.GetComponent<Image>().fillAmount = hp / hpMax;

                if (hp > hpMax) hp = hpMax;
                if (hp <= 0 && !dead)
                {
                    dead = true;
                    die();
                }


                    //define target com a posicao inicial 
                    target = initialPosition;

                //raycast em direcao ao jogador, para verificar se ele se encontra no raio de visao do inimigo
                hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, AttackRadius, 1 << LayerMask.NameToLayer("Default"));

                //forward = transform.TransformDirection(player.transform.position - transform.position);
                Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.red);

                //verifica se ocorreu colisao do raycast, se o collider tiver tag player muda o target para o player
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (distance < AttackRadius)
                        {
                            target = player.transform.position;
                        }
                    }
                }


                //calcula a distancia entre os objetos para determinar o comportamento
                distance = Vector3.Distance(target, transform.position);

                // se a distancia for menor que a de ataque , ataca o target
                if (target != initialPosition && distance < AttackRadius && !anim.GetCurrentAnimatorStateInfo(0).IsTag("dead"))
                {
                    anim.SetBool("active", true);
                    if (!isAttack)
                    {
                        StartCoroutine(attackDelay());
                        Instantiate(shoot, transform.position, transform.rotation);



                    }
                }
                else
                {
                    anim.SetBool("active", false);
                }

               // distance = Vector3.Distance(target, transform.position);
                
            }
                
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    IEnumerator attackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(delay);
        isAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("hit"))
            {

            }
            else
            {
                collision.SendMessage("takeDamage", attackDamage);
                collision.GetComponent<Animator>().SetTrigger("hit");
            }

        }
        else if (collision.tag == "Attack")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hit") || anim.GetCurrentAnimatorStateInfo(0).IsTag("dead"))
            {

            }
            else
            {
                anim.SetTrigger("hit");
                collision.GetComponent<Shoot>().direction = new Vector3(0, 0, 0);
                takeDamage(collision.GetComponent<Shoot>().damage);
                collision.GetComponent<Animator>().SetTrigger("collision");
            }

        }
    }

    public void takeDamage(float damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(hitSound, musicControl.soundVolume);
    }

    void die()
    {
        anim.SetTrigger("die");
    }

    private void OnBecameVisible()
    {
        isActive = true;
        StartCoroutine(isReady());
    }

    private void OnBecameInvisible()
    {
        //isActive = false;
    }

    void destroyEnemy()
    {
        player.GetComponent<Player>().currentMap.GetComponent<MapConfig>().defeatEnemy();
        GameManager.enemysTotal--;
        for (int i = 0; i < xp; i++)
        {
            Instantiate(energy, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    public IEnumerator isReady()
    {
        yield return new WaitForSeconds(1f);
        ready = true;
    }
}
