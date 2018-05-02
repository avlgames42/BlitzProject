using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy: MonoBehaviour
{

    Vector3 target;
    Vector3 initialPosition;
    Vector3 direction;

    RaycastHit2D hit;

    GameObject player;

    public float speed;
    float hp = 0;
    float hpMax = 4;
    float attackDamage = 1;
    public float visionRadius;
    public float AttackRadius;

    float distance;

    Animator anim;

    public CircleCollider2D attackCollider;

    public float attackDelay;
    bool isAttack = false;
    bool isActive = false;
    bool playerHasSpoted = false;

    int aux;

    public GameObject lifeBar;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        initialPosition = transform.position;
        attackCollider.enabled = false;

        target = initialPosition;
        aux = Mathf.RoundToInt((Random.Range(1, 4) * 100) / 100);

        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            lifeBar.GetComponent<Image>().fillAmount = hp / hpMax;

            if (hp > hpMax) hp = hpMax;
            if (hp < 0)
            {
                hp = 0;
                die();
            }

            raycastMaker();
            chooseAction();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    void raycastMaker()
    {
        //define target com a posicao inicial 
        //target = initialPosition;

        //raycast em direcao ao jogador, para verificar se ele se encontra no raio de visao do inimigo
        hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRadius, 1 << LayerMask.NameToLayer("Default"));

        //forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.red);

        //verifica se ocorreu colisao do raycast, se o collider tiver tag player muda o target para o player
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                if(distance < visionRadius)
                {
                    target = player.transform.position;
                }
            }
        }

        //calcula a distancia entre os objetos para determinar o comportamento
        distance = Vector3.Distance(target, transform.position);

        //calcula a direcao para tratar animacao 
        direction = (target - transform.position).normalized;
    }

    void attack()
    {
        //flip o sprite para esquerda para ajusta a animaçao
        if (direction.x < 0)
        {
           transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
           attackCollider.offset = new Vector2((direction.x*-1), direction.y);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            attackCollider.offset = new Vector2(direction.x, direction.y);
        }

        //posiciona o collider de ataque

        anim.SetFloat("movX", direction.x);
        anim.SetFloat("movY", direction.y);
        anim.SetTrigger("attack");
        StartCoroutine(delay());

    }

    void chooseAction()
    {
        // se a distancia for menor que a de ataque , ataca o target
        if (target != initialPosition && distance < AttackRadius)
        {
            if (!isAttack) attack();
        }
        else
        {
            if (!isAttack) walk();
        }
    }

    void walk()
    { 
        //flip o sprite para esquerda para ajusta a animaçao
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }


        //caso contrario movimenta em direcao ao alvo
        transform.Translate((direction) * speed * Time.deltaTime);

        // trata a animacao doinimigo
        anim.SetFloat("movX", direction.x);
        anim.SetFloat("movY", direction.y);
        anim.SetBool("walking", true);

        if (target == initialPosition && distance < 0.02f)
        {
            transform.position = initialPosition;
            anim.SetBool("walking", false);

        }

    }

    public void takeDamage(float damage)
    {
        hp -= damage;
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    public void activeColliderAttack()
    {
        attackCollider.enabled = true;

    }
    public void desactiveColliderAttack()
    {
        attackCollider.enabled = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.SendMessage("takeDamage", attackDamage);
        }
    }


    IEnumerator delay()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelay);
        if(isAttack) isAttack = false;

    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        //isActive = false;
    }
}
