using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootScript : MonoBehaviour
{
    public bool ammo;
    public bool health;
    public PlayerAttributes attributes;
    public GunScript gun;
    public Rigidbody rb;

    private void Awake()
    {
        if (health)
        {
            attributes = GameObject.Find("player").GetComponent<PlayerAttributes>();
        }
        if (ammo)
        {
            gun = GameObject.Find("Hand").transform.GetChild(0).gameObject.GetComponent<GunScript>();
        }
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * 200);
    }

    public void Loot()
    {
        if (health)
        {
            if (attributes.playerHealth != 100)
            {
                attributes.playerHealth += 20;
            }
        }
        if (ammo)
        {
            gun.ammo += Random.Range(10, 51);
        }

        Destroy(gameObject);
    }
}
