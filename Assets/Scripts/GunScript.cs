using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Camera main;
    public GunProfile profile;
    public float damage;
    public int loadedAmmo = 0;
    public int magazineCapacity;
    public int ammo;
    public float cadency;
    public bool canShoot = true;

    public GameObject gunBurst;
    public GameObject bloodSplatter;

    void Awake()
    {
        main = Camera.main;
        if(profile != null)
        {
            damage = profile.damage;
            magazineCapacity = profile.magazineSize;
            ammo = profile.ammo;
            cadency = profile.cadency;
        }

        GameObject.Find("Canvas").GetComponent<CanvasController>().SearchGun(this);
        bloodSplatter = Resources.Load("BloodSplatter") as GameObject;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            int reload = magazineCapacity - loadedAmmo;
            if(ammo == 0)
            {
                return;
            }

            if (reload > ammo)
            {
                loadedAmmo = ammo;
                ammo = 0;
                return;
            }

            ammo -= reload;
            loadedAmmo += reload;
        }

        if (Input.GetMouseButton(0) && loadedAmmo > 0)
        {
            if (canShoot)
            {
                StartCoroutine(ShotDelay());             
            }
        }
    }

    IEnumerator ShotDelay()
    {
        canShoot = false;
        gunBurst.SetActive(true);
        loadedAmmo--;

        Ray ray = main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("Zombie"))
            {
                hitInfo.collider.gameObject.GetComponent<ZombieScript>().health -= damage;
                Vector3 rotationParticle = transform.eulerAngles;
                rotationParticle.y -= 90;
                Instantiate(bloodSplatter, hitInfo.point, Quaternion.Euler(rotationParticle));
            }
        }

        yield return null;
        gunBurst.SetActive(false);
        yield return new WaitForSeconds(cadency);
        canShoot = true;
    }
}
