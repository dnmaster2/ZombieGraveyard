using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public Transform player;
    public PlayerAttributes playerAttributes;
    public float health;
    public int damage;
    public List<GameObject> loots;
    public Slider healthBar;
    public Image fillHealthBar;
    public Gradient gradient;
    bool cantWalk;
    bool attackDelayBool = true;
    bool alive;
    bool hasDroppedLoot;
    NavMeshAgent agent;
    Animation anim;
    public AnimationClip attackClip;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animation>();
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        anim.CrossFade("Z_run");
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        fillHealthBar.color = gradient.Evaluate(healthBar.normalizedValue);

        if (health <= 0)
        {
            cantWalk = true;
            alive = false;
            anim.CrossFade("Z_death_A");
            DeathLoot();
            Destroy(gameObject, 4f);
        }
        if (!cantWalk)
        {
            agent.destination = player.position;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && alive)
        {
            cantWalk = true;
            anim.CrossFade("Z_attack_A");
            if (attackDelayBool)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cantWalk = false;
            anim.CrossFade("Z_run");
            StopCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attackDelayBool = false;
        playerAttributes.playerHealth -= damage;
        yield return new WaitForSeconds(attackClip.length);
        attackDelayBool = true;
    }

    void DeathLoot()
    {
        if (!hasDroppedLoot)
        {
            int i = Random.Range(0, 101);
            if (i <= 90)
            {
                Instantiate(loots[Random.Range(0, 2)], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(loots[Random.Range(2, 5)], transform.position + Vector3.up * 2, Quaternion.identity);
            }
            hasDroppedLoot = true;
        }
        
    }
}
