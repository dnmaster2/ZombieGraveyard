using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GunScript gun;
    public PlayerAttributes attributes;
    public Text ammoTxt;
    public Slider ammoBar, healthBar;
    public Image fillAmmoBar, fillHealthBar;
    public Gradient gradient;

    void Start()
    {
        ammoBar.maxValue = gun.magazineCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        ammoTxt.text = gun.loadedAmmo.ToString() + " / " + gun.ammo.ToString();

        ammoBar.value = gun.loadedAmmo;
        fillAmmoBar.color = gradient.Evaluate(ammoBar.normalizedValue);

        healthBar.value = attributes.playerHealth;
        fillHealthBar.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SearchGun(GunScript newGun)
    {
        gun = newGun;
        ammoBar.maxValue = gun.magazineCapacity;
    }
}
